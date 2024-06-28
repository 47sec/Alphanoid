using System.Runtime.CompilerServices;
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
        base.timer -= shadeTimer;
    }

    public override void deactivate(Transform transform)
    {
        shadeAndDeactivate(transform, reset);
    }

    private static void reset(Transform transform)
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
