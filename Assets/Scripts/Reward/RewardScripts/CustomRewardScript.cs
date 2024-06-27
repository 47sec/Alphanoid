using UnityEngine;

public class CustomRewardScript : MonoBehaviour
{
    // Для создания своего скрипта награды нужно наследоваться от этого класса. Переопределение Use(Collider2D collision) для своего поведения
    // collision всегда явлется объектом с тегом Player
    public virtual void Use(Collider2D collision) { Debug.Log("You stupit"); }
}
