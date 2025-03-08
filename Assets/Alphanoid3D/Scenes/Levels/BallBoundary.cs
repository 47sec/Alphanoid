using UnityEngine;

/// <summary>
/// Логика при столкновения Ball с объектом
/// </summary>
public class BallBoundary : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // use collision groups?
        ArcanoidBall ball = collision.collider.GetComponent<ArcanoidBall>();

        if(ball != null)
        {
            Debug.Log("Ball collided");
            ball.ResetToPlatform();
        }
    }
}