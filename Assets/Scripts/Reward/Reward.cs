using UnityEngine;

public class Reward : MonoBehaviour
{
    // ��������� ������ ���� ��������� � �������
    private CustomRewardScript script;

    [Tooltip("����������� ����� ��������� ��� ��������� ������")]
    public float chanceMod;

    [Tooltip("�������� �������")]
    public float speed;

    [HideInInspector]
    public float relativeChance;

    private void Start()
    {
        script = GetComponent<CustomRewardScript>();
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // ������� ������� �� ��, ��� ��� �������� Use ��� ����� � ����������
            script.Use(collision);
            Destroy(transform.gameObject);
        }
    }
    public void setRelativeChance(float sum)
    {
        relativeChance = chanceMod / sum;
    }
}
