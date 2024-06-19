using UnityEngine;

public class Reward : MonoBehaviour
{
    // Кастомный скрипт надо подвязать к объекту
    private CustomRewardScript script;

    [Tooltip("Модификатор шанса появления при рандомном выборе")]
    public float chanceMod;

    [Tooltip("Скорость падения")]
    public float speed;

    [HideInInspector]
    public float relativeChance;

    private void Start()
    {
        script = GetComponent<CustomRewardScript>();
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // Спасибо господь за то, что оно вызывает Use как метод у наследника
            script.Use(collision);
            Destroy(transform.gameObject);
        }
    }
    public void setRelativeChance(float sum)
    {
        relativeChance = chanceMod / sum;
    }
}
