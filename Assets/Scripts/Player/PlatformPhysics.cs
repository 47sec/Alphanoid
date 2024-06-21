using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlatformPhysics : MonoBehaviour
{
    [Tooltip("Больше число, больше угол отскока на краях")]
    public float angleMod = 8f;

    [Tooltip("Меньше число, больше зона центра")]
    public float centerSize = 2.2f;

    private bool sticky = false;

    public void setSticky(bool sticky)
    {
        this.sticky = sticky;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Hit":
                {
                    Rigidbody2D ball_rb = other.attachedRigidbody;

                    if (sticky)
                    {
                        other.GetComponent<MoveBall>().disable();
                        ball_rb.velocity = Vector3.zero;
                        break;
                    }

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
                    break;
                }

        }
    }

}
