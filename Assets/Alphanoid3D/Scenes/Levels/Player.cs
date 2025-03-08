using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthPoints health;
    public Score score;
    private void Awake()
    {
        health = GetComponent<HealthPoints>();
        score = GetComponent<Score>();
    }
}