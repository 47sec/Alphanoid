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
