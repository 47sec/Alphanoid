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

    public void hit(int damage, GameObject hitBy)
    { 
        var customScript = transform.GetComponent<CustomBlockScript>();

        if (customScript != null)
        {
            customScript.CollisionUpdate(hitBy);
        }

        hitBy.SendMessage("ChangeSpeed", blockAcceleration);
        blockHp -= damage;

        if (blockHp <= 0)
        {
            blockManager.DestroyBlock(this);
            hitBy.SendMessage("Scored", blockPoints);
        }
    }
}
