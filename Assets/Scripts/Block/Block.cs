using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockManager blockManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockManager = GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            blockManager.DestroyBlock(this);
            collision.gameObject.SendMessage("Scored", 1u);
        }
    }
}
