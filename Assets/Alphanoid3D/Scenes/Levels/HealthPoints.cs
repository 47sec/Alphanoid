using System;
using UnityEngine;
public class HealthPoints : MonoBehaviour
{
    [field : SerializeField] public int MaxHealth {  get; private set; }
    public int Health { get; private set; }

    public Action<int> OnHealthChange;
    //public Action<int> OnDamage;
    //public Action<int> OnHeal;
    public Action OnDead;
    public void Damage(int damageAmount)
    {
        Health -= damageAmount;

        if(Health < 0) 
            Health = 0;

        OnHealthChange?.Invoke(-damageAmount);

        if (IsDead())
            OnDead?.Invoke();
    }
    public void Heal(int healAmount)
    {
        Health += healAmount;

        OnHealthChange?.Invoke(healAmount);

        if (Health > MaxHealth)
            Health = MaxHealth;
    }
    public bool IsDead()
    {
        return Health <= 0;
    }

    private void Awake()
    {
        Health = MaxHealth;
    }
}