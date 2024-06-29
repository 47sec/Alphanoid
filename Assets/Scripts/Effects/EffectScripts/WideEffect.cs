using UnityEngine;

public class WideEffect : CustomEffectScript
{
    private const uint effectId = 1;

    public override void activate(Transform transform)
    {
        EffectHandler handler = transform.GetComponent<EffectHandler>();

        Vector3 twiceScaleX = transform.localScale * new Vector2(2, 1); 

        // Плавное расширение объекта
        handler.addLastingEffect(() =>
        {
            transform.localScale += (Vector3)(transform.localScale * new Vector2(0.01f, 0f));

            // Чтоб был ровно в 2 раза больше
            if (transform.localScale.x >= twiceScaleX.x)
                transform.localScale = twiceScaleX;

            // Условие окончания эффекта (объект стал в 2 раза шире)
            return transform.localScale == twiceScaleX;
        });
    }

    public override void deactivate(Transform transform)
    {
        EffectHandler effectHandler = transform.GetComponent<EffectHandler>();

        Vector3 halfScaleX = transform.localScale * new Vector2(0.5f, 1);

        // Плавное сужение объекта
        effectHandler.addLastingEffect(() =>
        {
            transform.localScale -= (Vector3)(transform.localScale * new Vector2(0.05f, 0f));

            // Чтоб был ровно в два раза уже
            if (transform.localScale.x <= halfScaleX.x)
                transform.localScale = halfScaleX;

            // Условие окончания эффекта (объект стал в 2 раза уже)
            return transform.localScale == halfScaleX;
        });
    }

    public override uint getId()
    {
        return effectId;
    }
}
