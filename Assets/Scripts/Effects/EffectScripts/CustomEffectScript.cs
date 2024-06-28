using System;
using UnityEngine;

public class CustomEffectScript : ScriptableObject
{
    // Id класса-наследника должен быть уникальным, я хз как сделать это автоматически
    private const uint effectId = 0;

    public float timer = 0;

    // 1 - увеличение прозрачности, -1 - уменьшение
    protected static float shadeDir = 1f;

    // Если в эффекте используется моргание перед выключением, не забудьте отнять от timer'а shadeTimer, чтобы длительность эффекта не увеличилась
    // Также внутри public override void activate(Transform transform) можно изменить длительность моргания, если хочется
    protected float shadeTimer = 2f;

    public virtual void activate(Transform transform) { Debug.Log("You stupid"); }
    public virtual void deactivate(Transform transform) { Debug.Log("You stupid"); }

    // Тоже надо переписать
    public virtual uint getId() { return effectId; }

    // Метод для моргания спрайта
    protected static void setShade(SpriteRenderer sprite)
    {
        sprite.color -= (Color.black * shadeDir) * 0.025f;
        if (sprite.color.a >= 0.99f || sprite.color.a <= 0.5f)
            shadeDir *= -1f;
    }

    // Принимает вторым параметром статический метод, который будет выполнен после окончания мигания
    protected void shadeAndDeactivate(Transform transform, Action<Transform> afterShade)
    {
        EffectHandler handler = transform.GetComponent<EffectHandler>();
        SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();

        handler.addLastingEffect(() =>
        {
            setShade(sprite);
            shadeTimer -= Time.deltaTime;

            if (shadeTimer <= 0)
            {
                sprite.color = sprite.color * new Color(1, 1, 1, 0) + Color.black;
                shadeDir = 1f;
                afterShade(transform);
                return true;
            }

            return false;
        });
    }
}
