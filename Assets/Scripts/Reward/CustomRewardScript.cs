using UnityEngine;

public class CustomRewardScript : MonoBehaviour
{
    // ��� �������� ������ ������� ������� ����� ������������� �� ����� ������. ��������������� Use(Collider2D collision) ��� ������ ���������
    // collision ������ ������� �������� � ����� Player
    public virtual void Use(Collider2D collision) { Debug.Log("You stupit"); }
}
