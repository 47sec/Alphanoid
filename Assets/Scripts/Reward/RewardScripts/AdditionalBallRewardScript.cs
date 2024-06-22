using UnityEngine;

public class AdditionalBallRewardScript : CustomRewardScript
{
    public override void Use(Collider2D collision)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var ball = Instantiate(GameObject.FindGameObjectWithTag("Hit")).GetComponent<BallScript>();
        ball.setDynamicSpeed();
        player.GetComponent<PlatformInput>().activateBall(ball.GetComponent<Rigidbody2D>());
        ball.transform.position = player.transform.position + (Vector3.up * player.GetComponent<BoxCollider2D>().size.y * 0.5f);
    }
}
