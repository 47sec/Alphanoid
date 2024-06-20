using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBall : MonoBehaviour
{
    Rigidbody2D rb;
    private bool ballIsActivated;

    [Tooltip("���������")]
    public Transform platform;

    [Tooltip("�������� ����")]
    public float speed;

    [Tooltip("������������ �������� ����")]
    public float maxSpeed;

    [Tooltip("��������� �������� ���� ����� ������ ����")]
    public bool saveMomentum;

    private void Start()
    {
        ballIsActivated = false;
    }

    public bool getActive()
    {
        return ballIsActivated;
    }

    void Update()
    {
        if (!ballIsActivated && Input.GetKeyDown(KeyCode.Space))
        {
            BallActivate();
        }
        if (!ballIsActivated) { transform.position = new Vector2(platform.position.x, transform.position.y); }
    }
    public void BallActivate()
    {
        ballIsActivated = true;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.forward) * (new Vector2(0, 1) * speed);
    }

    public void disable()
    {
        ballIsActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Lose":
                platform.SendMessage("Damaged", 1);
                ballIsActivated = false;
                if (saveMomentum)
                    speed = rb.velocity.magnitude;
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

    public void ChangeSpeed(float percent)
    {
        // �������� ��� � �������� ������������ ��������
        rb.velocity = rb.velocity.normalized * Mathf.Clamp((rb.velocity + rb.velocity * percent).magnitude, 0, maxSpeed);
    }
}
