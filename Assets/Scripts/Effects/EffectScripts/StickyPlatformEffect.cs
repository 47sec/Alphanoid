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
        transform.GetComponent<PlatformInput>().sendAllBalls();

    }

}
