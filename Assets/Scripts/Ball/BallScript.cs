using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("—корость м€ча")]
    private float speed;

    [SerializeField]
    [Tooltip("ћаксимальна€ скорость м€ча")]
    private float maxSpeed;

    [SerializeField]
    [Tooltip("—охран€ть скорость м€ча после смерти м€ча")]
    private bool saveMomentum;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.transform.GetComponent<PlatformPhysics>().setReflection(transform);
                break;

            case "Hit":
                break;

            case "Block":
                collision.transform.GetComponent<Block>().hit(1, gameObject);
                break;

            case "Lose":
                lose();
                break;

            default:
                {
                    Rigidbody2D rb = GetComponent<Rigidbody2D>();

                    foreach (var contact in collision.contacts)
                        rb.velocity = Vector2.Reflect(rb.velocity, contact.normal);

                    break;
                }
        }
    }

    private void lose()
    {
        if (GameObject.FindGameObjectsWithTag("Hit").Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (saveMomentum)
            speed = rb.velocity.magnitude;

        rb.velocity = Vector3.zero;
        transform.position = player.transform.position + new Vector3(0, player.GetComponent<BoxCollider2D>().size.y / 2);

        player.SendMessage("Damaged", 1);
        player.GetComponent<PlatformInput>().attachBall(rb);
    }

    public float getSpeed()
    {
        return speed;
    }

    private void Scored(uint points)
    {
        GameObject.FindGameObjectWithTag("Player").SendMessage("AddScore", points);
    }

    public void ChangeSpeed(float percent)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // ”скор€ем м€ч в пределах максимальной скорости
        rb.velocity = rb.velocity.normalized * Mathf.Clamp((rb.velocity + rb.velocity * percent).magnitude, 0, maxSpeed);
    }

}
