using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockManager blockManager;

    [HideInInspector]
    public float relativeChance;

    [Tooltip("Модификатор шанса спавна блока")]
    public float chanceMod;

    [Tooltip("Количество ударов по блоку для разрушения")]
    public int blockHp;

    [Tooltip("Количество очков за разрушение блока")]
    public uint blockPoints;

    [Tooltip("Ускорение мяча при столкновении с блоком (1 = 100%)")]
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
