using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject blockManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            //transform.position = new Vector2(10, 10);
            Destroy(transform.gameObject);
            collision.gameObject.SendMessage("Scored", 1u);

        }
    }
}
