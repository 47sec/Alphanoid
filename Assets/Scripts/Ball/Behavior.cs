using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBall : MonoBehaviour
{
    [SerializeField]

    Rigidbody2D rb;
    private bool ballIsActivated;

    [Tooltip("ѕлатформа")]
    public Transform platform;

    [Tooltip("—корость м€ча")]
    public float speed;

    private void Start()
    {
        ballIsActivated = false;
    }

    void Update()
    {
        if (!ballIsActivated && Input.GetKeyDown(KeyCode.Space))
        {
            BallActivate();
            ballIsActivated = true;
        }
        if (!ballIsActivated) { transform.position = new Vector2(platform.position.x, transform.position.y); }
    }
    
    private void BallActivate()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-5f, 5f), speed); ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lose"))
        {
            Debug.Log("You lose :(");
            ballIsActivated = false;
            rb.velocity = Vector2.zero;
            transform.position = new Vector2(0, -3);
            platform.position = new Vector2(0, -4);
        }
    }
}
