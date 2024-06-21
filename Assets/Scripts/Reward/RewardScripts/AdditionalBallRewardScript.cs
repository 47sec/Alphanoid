using UnityEngine;

public class AdditionalBallRewardScript : CustomRewardScript
{
    public override void Use(Collider2D collision)
    {
        MoveBall ball = Instantiate(GameObject.FindGameObjectWithTag("Hit")).GetComponent<MoveBall>();
        ball.transform.position = new Vector3(0, 1, 0) + GameObject.FindGameObjectWithTag("Player").transform.position;
        ball.setAdditional(true);
        ball.BallActivate();
    }
}
