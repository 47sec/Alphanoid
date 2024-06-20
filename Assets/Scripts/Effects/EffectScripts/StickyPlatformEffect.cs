using UnityEngine;

public class StickyPlatformEffect : CustomEffectScript
{
    private const uint effectId = 2;

    public override uint getId()
    {
        return effectId;
    }

    public override void activate(Transform transform)
    {
        transform.GetComponent<PlatformPhysics>().setSticky(true);
    }

    public override void deactivate(Transform transform)
    {
        transform.GetComponent<PlatformPhysics>().setSticky(false);

        foreach (var ball in GameObject.FindGameObjectsWithTag("Hit"))
        {
            var ballObj = ball.GetComponent<MoveBall>();
            if (!ballObj.getActive())
                ballObj.BallActivate();
        }
    }
}
