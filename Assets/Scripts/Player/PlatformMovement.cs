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

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), transform.position.y); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hit"))
        {
            Rigidbody2D ball_rb = other.attachedRigidbody;

            float distance = ball_rb.transform.position.x - transform.position.x;

            float direction_x = new Vector2(distance, 0).normalized.x; // Сторона, в которую будет отскок

            float center_size = 2.2f; // Меньше число, больше центр

            float max_angle = 8f; // Больше число, больше угол отскока на краях

            float center_clamp = Mathf.Floor(Mathf.Clamp(Mathf.Abs(distance) * center_size, 0, 1)) * Mathf.Abs(distance) + 0.05f;
            Debug.Log(center_clamp);

            ball_rb.velocity =
                (
                    ball_rb.velocity *
                    (new Vector2(0f, -1f)) +
                    (new Vector2(center_clamp * direction_x * max_angle, 0f))
                ).normalized
                * (ball_rb.velocity / ball_rb.velocity.normalized);
        }
    }
}
