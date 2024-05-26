using UnityEditor; // ����������� ���������� UnityEditor ��� ������������� ��������� Unity
using UnityEngine; // ����������� ���������� UnityEngine ��� ������������� ����������� Unity
using UnityEngine.UIElements; // ����������� ���������� UnityEngine.UIElements ��� ������������� UI ���������

public class MoveBall : MonoBehaviour
{
    // ���� ��� �������� ������ �� ��������� Rigidbody2D, ������� �������� �� ������ �������
    [SerializeField]
    Rigidbody2D rb;

    // ���������� ���������� ��� ������������, ����������� �� ���
    private bool ballIsActivated;

    // ���� ��� �������� ������ �� ���������, �� ������� ���������� ���
    [Tooltip("���������")]
    public Transform platform;

    // ���� ��� �������� �������� ����
    [Tooltip("�������� ����")]
    public float speed;

    // �����, ������� ���������� ��� ������ ����
    private void Start()
    {
        // ���������� ��� �� �����������
        ballIsActivated = false;
    }

    // �����, ������� ���������� ������ ����
    void Update()
    {
        // ���� ��� �� ����������� � ���� ������ ������� ������
        if (!ballIsActivated && Input.GetKeyDown(KeyCode.Space))
        {
            // ���������� ��� � ������������� ���� ���������
            BallActivate();
            ballIsActivated = true;
        }

        // ���� ��� �� �����������, ���������� ��� ������ � ����������
        if (!ballIsActivated)
        {
            transform.position = new Vector2(platform.position.x, transform.position.y);
        }

        // �������� �������� ���� � � �������������
        if (ballIsActivated && rb.velocity.magnitude < speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    // ����� ��� ��������� ����
    private void BallActivate()
    {
        // �������� ��������� Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // ������������� ��������� �������� ���� � ��������� �������������� ������������
        rb.velocity = new Vector2(Random.Range(-5f, 5f), speed);
    }

    // ����� ��� ������������� ���� ��������� ����
    private void AdjustBallAngle()
    {
        // ��������� ��������� ��������� ���� (1-2 �������) � �������� ����������� �������� ����
        float angleAdjustment = Random.Range(-2f, 2f);
        Vector2 velocity = rb.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        angle += angleAdjustment;
        rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
    }

    // �����, ������� ���������� ��� ������������ � ������ ��������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������� ��� �������, � ������� ��������� ������������
        switch (collision.gameObject.tag)
        {
            // ���� ����������� � ��������, ������� ��� "Lose"
            case "Lose":
                // ���������� ��������� ��������� � ��������� �����
                platform.SendMessage("Damaged", 1);

                // ���������� ��������� ���� � ��� ��������
                ballIsActivated = false;
                rb.velocity = Vector2.zero;

                // ���������� ��������� � ��� � ��������� ���������
                platform.position = new Vector2(0, -4);
                transform.position = new Vector2(0, -3);
                break;

            // � ��������� ������� ������ �� ������
            default:
                break;
        }

        // ������������ ���� ��������� ������ ���� ��� ����������� � ���������� � ����������
        if (ballIsActivated && collision.gameObject.tag == "Scored")
        {
            AdjustBallAngle();
        }
    }

    // ����� ��� ���������� �����
    private void Scored(uint points)
    {
        // ���������� ��������� ��������� � ���������� �����
        platform.SendMessage("AddScore", points);
    }
}
