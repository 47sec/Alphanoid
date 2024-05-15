using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        speed = 8;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D)) { transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); }
        if (Input.GetKey(KeyCode.A)) { transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); }
    }
}
