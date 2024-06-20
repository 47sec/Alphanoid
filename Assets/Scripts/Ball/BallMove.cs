using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBall : MonoBehaviour
{
    Rigidbody2D rb;
    private bool ballIsActivated;

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
        ballIsActivated = false;
    }

    void Update()
    {
        if (!ballIsActivated && Input.GetKeyDown(KeyCode.Space))
        {
            BallActivate();
            ballIsActivated = true;
        }
        if (!ballIsActivated) { transform.position = new Vector2(platform.position.x, transform.position.y); }
    }

    private void BallActivate()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.forward) * (new Vector2(0, 1) * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Lose":
                platform.SendMessage("Damaged", 1);
                ballIsActivated = false;
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
