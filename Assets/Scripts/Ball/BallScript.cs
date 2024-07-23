using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("—корость м€ча")]
    private float speed;

    private float dynamicSpeed;

    [SerializeField]
    [Tooltip("ћаксимальна€ скорость м€ча")]
    private float maxSpeed;

    [SerializeField]
    [Tooltip("—охран€ть скорость м€ча после смерти м€ча")]
    private bool saveMomentum;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.transform.GetComponent<PlatformPhysics>().setReflection(transform);
                break;

            case "Lose":
                lose();
                break;

            case "Block":
                collision.transform.GetComponent<Block>().hit(1, gameObject);
                reflect(collision);
                break;

            default:
                reflect(collision);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edges"))
        {
            foreach (var point in collision.contacts)
            {
                transform.position += (Vector3)(new Vector2(Mathf.Abs(transform.position.x - point.point.x),
                        Mathf.Abs(transform.position.y - point.point.y)) * point.normal);

            }
        }
    }

    private void Start()
    {
        ballInit();
    }

    public void ballInit()
    {
        dynamicSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    private void reflect(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            Vector2 refl = Vector2.Reflect(direction, contact.normal) * dynamicSpeed;
            setDirection(refl.normalized);
            rb.velocity = refl;
        }
        float angle = Vector2.Angle(rb.velocity, Vector2.right);
        float correctedAngle = Mathf.Clamp(angle, 45f, 135f);

        // Ћибо -1, либо 1
        float dir = (rb.velocity * Vector2.up).normalized.y;

        // ѕоворачиваем м€ч, если угол относительно X больше 135 или меньше 45
        rb.velocity = Quaternion.AngleAxis(dir * (correctedAngle - angle), Vector3.forward) * rb.velocity;
    }

    private void lose()
    {
        if (GameObject.FindGameObjectsWithTag("Hit").Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (saveMomentum)
            speed = dynamicSpeed;

        rb.velocity = Vector3.zero;
        transform.position = player.transform.position + new Vector3(0, player.GetComponent<BoxCollider2D>().size.y / 2);

        player.SendMessage("Damaged", 1);
        player.GetComponent<PlatformInput>().attachBall(rb);
    }

    public float getSpeed()
    {
        return dynamicSpeed;
    }

    private void Scored(uint points)
    {
        GameObject.FindGameObjectWithTag("Player").SendMessage("AddScore", points);
    }

    public void ChangeSpeed(float percent)
    {
        dynamicSpeed = Mathf.Clamp(dynamicSpeed + dynamicSpeed * percent, 0, maxSpeed);
    }

}
