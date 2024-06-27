using UnityEngine;

public class CustomEffectScript : ScriptableObject
{
    // Id класса-наследника должен быть уникальным, € хз как сделать это автоматически
    private const uint effectId = 0;
    public virtual void activate(Transform transform) { Debug.Log("You stupid"); }
    public virtual void deactivate(Transform transform) { Debug.Log("You stupid"); }

    // “оже надо переписать
    public virtual uint getId() { return effectId; }
}
