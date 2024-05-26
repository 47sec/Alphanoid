using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlatformPhysics : MonoBehaviour
{
    [Tooltip("Больше число, больше угол отскока на краях")]
    public float angleMod = 8f;

    [Tooltip("Меньше число, больше зона центра")]
    public float centerSize = 2.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Hit": //  Если платформа столкнулась с мячом
                {
                   Rigidbody2D ball_rb = other.attachedRigidbody;

                    float distance = ball_rb.transform.position.x - transform.position.x;
                    float direction_x = new Vector2(distance, 0).normalized.x; // Определяю направление по x

                    //  значение center_clamp это значение с
                    //  учетом расстояние до центра объект и ограничивается
                    //  в определенном диапазоне после чего округляется и 
                    //  корректируется в соответствии с направлением и расстоянием до центра
                    float center_clamp = Mathf.Floor(Mathf.Clamp(Mathf.Abs(distance) * centerSize, 0, 1)) * Mathf.Abs(distance) + 0.05f;

                    // рассчитываю скорость с учетом столкновения с центром
                    Vector2 newVelocity =
                        ball_rb.velocity *
                        new Vector2(0f, -1f) +
                        new Vector2(center_clamp * direction_x * angleMod, 0f);

                    // Нормализую скорость если она НЕ нулевая
                    if (newVelocity != Vector2.zero) { ball_rb.velocity = newVelocity.normalized * ball_rb.velocity.magnitude; }

                    break;
                }
        }
    }

}
