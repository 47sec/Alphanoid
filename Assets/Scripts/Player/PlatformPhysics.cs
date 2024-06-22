using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlatformPhysics : MonoBehaviour
{
    [Tooltip("Больше число, больше угол отскока на краях")]
    public float angleMod = 8f;

    [Tooltip("Меньше число, больше зона центра")]
    public float centerSize = 1.7f;

    private bool sticky = false;

    public void setSticky(bool sticky)
    {
        this.sticky = sticky;
    }
    public void setReflection(Transform other)
    {
        other.position = new Vector2(other.position.x, transform.position.y + (transform.GetComponent<BoxCollider2D>().size.y / 2));
        Rigidbody2D ball_rb = other.GetComponent<Rigidbody2D>();

        if (sticky)
        {
            ball_rb.velocity = Vector2.zero;
            GetComponent<PlatformInput>().attachBall(ball_rb);
            return;
        }

        Vector2 posDifference = (Vector2)other.position - ((Vector2)transform.position + GetComponent<BoxCollider2D>().offset);

        float bounceAngle = Mathf.Lerp(45, -45, posDifference.normalized.x);

        ball_rb.velocity = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * (new Vector2(0, 1) * ball_rb.GetComponent<BallScript>().getSpeed());
        ball_rb.GetComponent<BallScript>().setDirection(ball_rb.velocity.normalized);


        //float distance = ball_rb.transform.position.x - transform.position.x;

        //float direction_x = new Vector2(distance, 0).normalized.x; // Сторона, в которую будет отскок

        //float center_clamp = Mathf.Floor(Mathf.Clamp(Mathf.Abs(distance) * centerSize, 0, 1)) * Mathf.Abs(distance) + 0.05f;

        //ball_rb.velocity =
        //    (
        //        ball_rb.velocity *
        //        (new Vector2(0f, -1f)) +
        //        (new Vector2(center_clamp * direction_x * angleMod, 0f))
        //    ).normalized
        //    * (ball_rb.velocity / ball_rb.velocity.normalized);
    }

}
