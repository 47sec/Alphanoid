using UnityEngine;

public class CustomEffectScript : ScriptableObject
{
    // Id ������-���������� ������ ���� ����������, � �� ��� ������� ��� �������������
    private const uint effectId = 0;
    public virtual void activate(Transform transform) { Debug.Log("You stupid"); }
    public virtual void deactivate(Transform transform) { Debug.Log("You stupid"); }

    // ���� ���� ����������
    public virtual uint getId() { return effectId; }
}
