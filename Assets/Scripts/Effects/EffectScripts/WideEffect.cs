using UnityEngine;

public class WideEffect : CustomEffectScript
{
    private const uint effectId = 1;

    public override void activate(Transform transform)
    {
        transform.localScale *= new Vector2(2, 1);
    }

    public override void deactivate(Transform transform)
    {
        transform.localScale /= new Vector2(2, 1);
    }

    public override uint getId()
    {
        return effectId;
    }
}
