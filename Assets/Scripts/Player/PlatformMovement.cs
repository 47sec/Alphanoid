using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Tooltip("Скорость платформы")]
    public float speed;


    private bool canMoveToLeft;
    private bool canMoveToRight;

    void Start()
    {
        canMoveToLeft = true;
        canMoveToRight = true;
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) && canMoveToRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }

        if (Input.GetKey(KeyCode.A) && canMoveToLeft)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("This Left Wall"))
        {
            canMoveToLeft = false;
            Debug.Log("This Wall");
        }
        if (other.gameObject.CompareTag("This Right Wall"))
        {
            canMoveToRight = false;
            Debug.Log("This Wall");
        }

    }

  private void OnTriggerExit2D(Collider2D other)
    {
        canMoveToLeft = true;

        canMoveToRight = true;
    }
}
