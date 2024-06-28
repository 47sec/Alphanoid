using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Tooltip("Скорость платформы")]
    public float speed;


    private bool canMoveToLeft;
    private bool canMoveToRight;

    private float sendingTimer = 0.1f;

    void Start()
    {
        canMoveToLeft = true;
        canMoveToRight = true;
    }
    void FixedUpdate()
    {
        // Без этого бреда засчитывает миллион нажатий пробела вместо одного
        sendingTimer = Mathf.Clamp(sendingTimer - Time.deltaTime, 0, 0.1f);

        if (Input.GetKey(KeyCode.D) && canMoveToRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }

        if (Input.GetKey(KeyCode.A) && canMoveToLeft)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            var balls = GameObject.FindGameObjectsWithTag("Hit");  // Сорри


            foreach (var ball in balls)
            {
                // Запускает только 1 мяч

                MoveBall ballObj = ball.GetComponent<MoveBall>();
                if (!ballObj.getActive() && sendingTimer <= 0)
                {
                    sendingTimer = 0.1f;
                    ballObj.BallActivate();
                    break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "This Left Wall":
                {
                    canMoveToLeft = false;
                    Debug.Log("This Wall");
                    break;
                }
            case "This Right Wall":
                {
                    canMoveToRight = false;
                    Debug.Log("This Wall");
                    break;
                }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Hit"))
        {

            canMoveToLeft = true;

            canMoveToRight = true;
        }
    }
}
