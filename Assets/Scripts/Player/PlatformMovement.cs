using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlatformMovement : MonoBehaviour
{
    [Tooltip("Левый барьер")]
    public Transform leftWall;

    [Tooltip("Правый барьер")]
    public Transform rightWall;

    [Tooltip("Скорость платформы")]
    public float speed;

    private float spriteWidth; //  Ширина платформы

    float GetSpriteWidth(GameObject spriteObject)
    {
        //  Возвращаем размер по x через "SpriteRenderer"
        return spriteObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Start() { spriteWidth = GetSpriteWidth(GameObject.Find("Player")); }

    void FixedUpdate()
    {
        float platformPositionX = transform.position.x; //  позиция платформы по X 

        if (Input.GetKey(KeyCode.A) &&
            //   Смотрим что крайняя левая точна платформы не больше позиции ПРАВОГО барьера
            (leftWall.transform.position.x < (platformPositionX - spriteWidth * 0.5 - 0.2))) // нужно что бы не выходить за границы
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.D) &&
            //   Смотрим что крайняя правая точна платформы не меньше позиции ЛЕВОГО барьера
            (rightWall.transform.position.x > (platformPositionX + spriteWidth * 0.5 + 0.2)))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }
}
