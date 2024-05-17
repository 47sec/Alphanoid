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
        switch (collision.gameObject.tag)
        {
            case "Lose":
                platform.SendMessage("Damaged", 1);
                ballIsActivated = false;
                rb.velocity = Vector2.zero;
                platform.position = new Vector2(0, -4);
                transform.position = new Vector2(0, -3);
                break;
            default:
                break;
        }
    }

    private void Scored(uint points)
    {
        platform.SendMessage("AddScore", points);
    }
}
