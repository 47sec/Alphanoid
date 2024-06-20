using UnityEngine;

public class StickyRewardScript : CustomRewardScript
{
    public override void Use(Collider2D collision)
    {
        collision.GetComponent<EffectHandler>().addEffect(ScriptableObject.CreateInstance<StickyPlatformEffect>(), 10);
    }

}
