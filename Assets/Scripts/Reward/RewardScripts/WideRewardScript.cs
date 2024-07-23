using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WideRewardScript : CustomRewardScript
{
    public override void Use(Collider2D collision)
    {
        collision.GetComponent<EffectHandler>().addEffect(ScriptableObject.CreateInstance<WideEffect>(), 10);
    }
}
