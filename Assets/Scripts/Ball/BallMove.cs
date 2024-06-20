using UnityEditor;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D collider2;
    private bool ballIsActivated = false;
    private bool isAdditional = false;

    [Tooltip("ѕлатформа")]
    public Transform platform;

    [Tooltip("—корость м€ча")]
    public float speed;

    [Tooltip("ћаксимальна€ скорость м€ча")]
    public float maxSpeed;

    [Tooltip("—охран€ть скорость м€ча после смерти м€ча")]
    public bool saveMomentum;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2 = GetComponent<Collider2D>();
    }

    public bool getActive()
    {
        return ballIsActivated;
    }

    void Update()
    {
        if (!ballIsActivated)
        {
            transform.position = new Vector2(platform.position.x, transform.position.y);
            collider2.enabled = false;
        }
    }
    public void BallActivate()
    {
        transform.position = new Vector3(0, platform.GetComponent<BoxCollider2D>().size.y + platform.GetComponent<BoxCollider2D>().size.y * 0.05f) + platform.position;
        rb = GetComponent<Rigidbody2D>();
        collider2 = GetComponent<Collider2D>();
        ballIsActivated = true;
        rb.velocity = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.forward) * (new Vector2(0, 1) * speed);
        collider2.enabled = true;
    }

    public void disable()
    {
        ballIsActivated = false;
    }

    public void setAdditional(bool additional)
    {
        this.isAdditional = additional;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Lose":
                if (isAdditional)
                {
                    Destroy(transform.gameObject);
                    break;
                }

                platform.SendMessage("Damaged", 1);
                ballIsActivated = false;
                collider2.enabled = false;
                if (saveMomentum)
                    speed = rb.velocity.magnitude;
                rb.velocity = Vector2.zero;
                platform.position = new Vector2(0, -4);
                transform.position = new Vector2(0, -3);
                break;
            default:
                break;
        }
    }

    private void Scored(uint points)
    {
        platform.SendMessage("AddScore", points);
    }

    public void ChangeSpeed(float percent)
    {
        // ”скор€ем м€ч в пределах максимальной скорости
        rb.velocity = rb.velocity.normalized * Mathf.Clamp((rb.velocity + rb.velocity * percent).magnitude, 0, maxSpeed);
    }
}
