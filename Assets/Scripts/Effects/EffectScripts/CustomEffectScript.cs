using System;
using UnityEngine;

public class CustomEffectScript : ScriptableObject
{
    // Id ������-���������� ������ ���� ����������, � �� ��� ������� ��� �������������
    private const uint effectId = 0;

    public float timer = 0;

    // 1 - ���������� ������������, -1 - ����������
    protected static float shadeDir = 1f;

    // ���� � ������� ������������ �������� ����� �����������, �� �������� ������ �� timer'� shadeTimer, ����� ������������ ������� �� �����������
    // ����� ������ public override void activate(Transform transform) ����� �������� ������������ ��������, ���� �������
    protected float shadeTimer = 2f;

    public virtual void activate(Transform transform) { Debug.Log("You stupid"); }
    public virtual void deactivate(Transform transform) { Debug.Log("You stupid"); }

    // ���� ���� ����������
    public virtual uint getId() { return effectId; }

    // ����� ��� �������� �������
    protected static void setShade(SpriteRenderer sprite)
    {
        sprite.color -= (Color.black * shadeDir) * 0.025f;
        if (sprite.color.a >= 0.99f || sprite.color.a <= 0.5f)
            shadeDir *= -1f;
    }

    // ��������� ������ ���������� ����������� �����, ������� ����� �������� ����� ��������� �������
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
