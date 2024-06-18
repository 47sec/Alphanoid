using UnityEngine;

public class CustomRewardScript : MonoBehaviour
{
    [Tooltip("Модификатор шанса появления при рандомном выборе")]
    public float chanceMod;

    [Tooltip("Скорость падения")]
    public float speed;

    [HideInInspector]
    public float relativeChance;

    private void Start()
    {
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    public virtual void Use(Collider2D collision) {}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Use(collision);

            Destroy(transform.gameObject);
        }
    }
    public void setRelativeChance(float sum)
    {
        relativeChance = chanceMod / sum;
    }
}
