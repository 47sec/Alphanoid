using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlatformMovement : MonoBehaviour
{
    [Tooltip("����� ������")]
    public Transform leftWall;

    [Tooltip("������ ������")]
    public Transform rightWall;

    [Tooltip("�������� ���������")]
    public float speed;

    private float spriteWidth; //  ������ ���������

    float GetSpriteWidth(GameObject spriteObject)
    {
        //  ���������� ������ �� x ����� "SpriteRenderer"
        return spriteObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Start() { spriteWidth = GetSpriteWidth(GameObject.Find("Player")); }

    void FixedUpdate()
    {
        float platformPositionX = transform.position.x; //  ������� ��������� �� X 

        if (Input.GetKey(KeyCode.A) &&
            //   ������� ��� ������� ����� ����� ��������� �� ������ ������� ������� �������
            (leftWall.transform.position.x < (platformPositionX - spriteWidth * 0.5 - 0.2))) // ����� ��� �� �� �������� �� �������
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.D) &&
            //   ������� ��� ������� ������ ����� ��������� �� ������ ������� ������ �������
            (rightWall.transform.position.x > (platformPositionX + spriteWidth * 0.5 + 0.2)))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }
}
