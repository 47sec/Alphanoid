using UnityEngine;

public class WideEffect : CustomEffectScript
{
    private const uint effectId = 1;

    public override void activate(Transform transform)
    {
        EffectHandler handler = transform.GetComponent<EffectHandler>();

        Vector3 twiceScaleX = transform.localScale * new Vector2(2, 1); 

        // ������� ���������� �������
        handler.addLastingEffect(() =>
        {
            transform.localScale += (Vector3)(transform.localScale * new Vector2(0.01f, 0f));

            // ���� ��� ����� � 2 ���� ������
            if (transform.localScale.x >= twiceScaleX.x)
                transform.localScale = twiceScaleX;

            // ������� ��������� ������� (������ ���� � 2 ���� ����)
            return transform.localScale == twiceScaleX;
        });
    }

    public override void deactivate(Transform transform)
    {
        EffectHandler effectHandler = transform.GetComponent<EffectHandler>();

        Vector3 halfScaleX = transform.localScale * new Vector2(0.5f, 1);

        // ������� ������� �������
        effectHandler.addLastingEffect(() =>
        {
            transform.localScale -= (Vector3)(transform.localScale * new Vector2(0.05f, 0f));

            // ���� ��� ����� � ��� ���� ���
            if (transform.localScale.x <= halfScaleX.x)
                transform.localScale = halfScaleX;

            // ������� ��������� ������� (������ ���� � 2 ���� ���)
            return transform.localScale == halfScaleX;
        });
    }

    public override uint getId()
    {
        return effectId;
    }
}
