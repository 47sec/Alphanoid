using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class ArcanoidPlatform : MonoBehaviour
{
    public Player owner;

    [SerializeField] private float speed = 1f;
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed *= rb.mass;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForceAtPosition(Vector3.right * speed * Time.deltaTime, gameObject.transform.position);
            //rb.MovePosition(gameObject.transform.position + Vector3.right * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForceAtPosition(Vector3.left * speed * Time.deltaTime, gameObject.transform.position);

            //rb.MovePosition(gameObject.transform.position + Vector3.left * speed * Time.deltaTime);
        }
    }
}
