using UnityEngine;

public class Behaviour : MonoBehaviour
{
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
            Debug.Log("Block [" + this.name + "] destroyed!");
            transform.position = new Vector2(10, 10);
        }
    }
}
