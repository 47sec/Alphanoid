using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlatformPhysics : MonoBehaviour
{
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

        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        float maxDifference = collider.size.x * 0.5f;
        float posDifference = other.position.x - transform.position.x;
        posDifference += maxDifference;
        posDifference /= collider.size.x;

        float bounceAngle = Mathf.Lerp(60, -60, posDifference);

        ball_rb.velocity = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * (new Vector2(0, 1) * ball_rb.GetComponent<BallScript>().getSpeed());
        ball_rb.GetComponent<BallScript>().setDirection(ball_rb.velocity.normalized);
    }

}
