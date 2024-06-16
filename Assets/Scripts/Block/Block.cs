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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        relativeChance = chanceMod;
        blockManager = GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setRelativeChance(float sum)
    {
        relativeChance = chanceMod / sum;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            var customScript = transform.GetComponent<CustomBlockScript>();

            if (customScript != null)
            {
                customScript.CollisionUpdate(collision);
            }

            blockHp--;

            collision.gameObject.GetComponent<Rigidbody2D>().velocity += collision.gameObject.GetComponent<Rigidbody2D>().velocity * blockAcceleration;

            if (blockHp <= 0)
            {
                blockManager.DestroyBlock(this);
                collision.gameObject.SendMessage("Scored", blockPoints);
            }
        }
    }
}
