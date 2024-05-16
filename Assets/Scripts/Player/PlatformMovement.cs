using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        speed = 8;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D)) { transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); }
        if (Input.GetKey(KeyCode.A)) { transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hit"))
        {
            Rigidbody2D ball_rb = other.attachedRigidbody;

            float direction_x = new Vector2(ball_rb.transform.position.x - transform.position.x, 0).normalized.x;

            float center_clamp = Mathf.Floor(Mathf.Abs(ball_rb.transform.position.x - transform.position.x) * 2.5f) + 0.1f;

            //Debug.Log(direction_x);

            ball_rb.velocity =
                (
                    ball_rb.velocity *
                    (new Vector2(0f, -1f)) +
                    (new Vector2(center_clamp * direction_x * 8, 0f))
                ).normalized
                * speed;
        }
    }
}
