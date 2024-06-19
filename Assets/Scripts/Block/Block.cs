using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockManager blockManager;

    [HideInInspector]
    public float relativeChance;

    [Tooltip("����������� ����� ������ �����")]
    public float chanceMod;

    [Tooltip("���������� ������ �� ����� ��� ����������")]
    public int blockHp;

    [Tooltip("���������� ����� �� ���������� �����")]
    public uint blockPoints;

    [Tooltip("��������� ���� ��� ������������ � ������ (1 = 100%)")]
    public float blockAcceleration;

    void Start()
    {
        relativeChance = chanceMod;
        blockManager = GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>();
    }

    public void setRelativeChance(float sum)
    {
        relativeChance = chanceMod / sum;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            MoveBall ball = collision.gameObject.GetComponent<MoveBall>();
            Rigidbody2D ballRb = ball.gameObject.GetComponent<Rigidbody2D>();

            var customScript = transform.GetComponent<CustomBlockScript>();

            if (customScript != null)
            {
                customScript.CollisionUpdate(collision);
            }

            blockHp--;

            ball.ChangeSpeed(blockAcceleration);

            if (blockHp <= 0)
            {
                blockManager.DestroyBlock(this);
                ball.gameObject.SendMessage("Scored", blockPoints);
            }
        }
    }
}
