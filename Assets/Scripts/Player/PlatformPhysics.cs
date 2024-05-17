using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Tooltip("Скорость платформы")]
    public float speed;
    private bool canMoveToLeft;
    private bool canMoveToRight;

    void Start()
    {
        canMoveToLeft = true;
        canMoveToRight = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) && canMoveToRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }

        if (Input.GetKey(KeyCode.A) && canMoveToLeft)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }

        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), transform.position.y);
    }

    [Tooltip("Больше число, больше угол отскока на краях")]
    public float angleMod = 8f;

    [Tooltip("Меньше число, больше зона центра")]
    public float centerSize = 2.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hit"))
        {
            Rigidbody2D ball_rb = other.attachedRigidbody;

            float distance = ball_rb.transform.position.x - transform.position.x;

            float direction_x = new Vector2(distance, 0).normalized.x; // Сторона, в которую будет отскок

            float center_clamp = Mathf.Floor(Mathf.Clamp(Mathf.Abs(distance) * centerSize, 0, 1)) * Mathf.Abs(distance) + 0.05f;

            ball_rb.velocity =
                (
                    ball_rb.velocity *
                    (new Vector2(0f, -1f)) +
                    (new Vector2(center_clamp * direction_x * angleMod, 0f))
                ).normalized
                * (ball_rb.velocity / ball_rb.velocity.normalized);
        }

        if (other.gameObject.CompareTag("This Left Wall"))
        {
            canMoveToLeft = false;
            Debug.Log("This Wall");
        }
        if (other.gameObject.CompareTag("This Right Wall"))
        {
            canMoveToRight = false;
            Debug.Log("This Wall");
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canMoveToLeft = true;

        canMoveToRight = true;
    }
}
