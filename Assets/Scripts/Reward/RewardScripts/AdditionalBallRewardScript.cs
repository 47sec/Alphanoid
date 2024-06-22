using UnityEngine;

public class AdditionalBallRewardScript : CustomRewardScript
{
    public override void Use(Collider2D collision)
    {
        var ball = Instantiate(GameObject.FindGameObjectWithTag("Hit")).GetComponent<BallScript>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformInput>().activateBall(ball.GetComponent<Rigidbody2D>());
    }
}
