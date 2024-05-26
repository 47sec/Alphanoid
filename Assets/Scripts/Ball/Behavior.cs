using UnityEditor; // Подключение библиотеки UnityEditor для использования редактора Unity
using UnityEngine; // Подключение библиотеки UnityEngine для использования функционала Unity
using UnityEngine.UIElements; // Подключение библиотеки UnityEngine.UIElements для использования UI элементов

public class MoveBall : MonoBehaviour
{
    // Поле для хранения ссылки на компонент Rigidbody2D, который отвечает за физику объекта
    [SerializeField]
    Rigidbody2D rb;

    // Логическая переменная для отслеживания, активирован ли мяч
    private bool ballIsActivated;

    // Поле для хранения ссылки на платформу, на которой расположен мяч
    [Tooltip("Платформа")]
    public Transform platform;

    // Поле для хранения скорости мяча
    [Tooltip("Скорость мяча")]
    public float speed;

    // Метод, который вызывается при старте игры
    private void Start()
    {
        // Изначально мяч не активирован
        ballIsActivated = false;
    }

    // Метод, который вызывается каждый кадр
    void Update()
    {
        // Если мяч не активирован и была нажата клавиша пробел
        if (!ballIsActivated && Input.GetKeyDown(KeyCode.Space))
        {
            // Активируем мяч и устанавливаем флаг активации
            BallActivate();
            ballIsActivated = true;
        }

        // Если мяч не активирован, перемещаем его вместе с платформой
        if (!ballIsActivated)
        {
            transform.position = new Vector2(platform.position.x, transform.position.y);
        }

        // Проверка скорости мяча и её корректировка
        if (ballIsActivated && rb.velocity.magnitude < speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    // Метод для активации мяча
    private void BallActivate()
    {
        // Получаем компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Устанавливаем начальную скорость мяча с случайным горизонтальным направлением
        rb.velocity = new Vector2(Random.Range(-5f, 5f), speed);
    }

    // Метод для корректировки угла отражения мяча
    private void AdjustBallAngle()
    {
        // Добавляем небольшой случайный угол (1-2 градуса) к текущему направлению скорости мяча
        float angleAdjustment = Random.Range(-2f, 2f);
        Vector2 velocity = rb.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        angle += angleAdjustment;
        rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
    }

    // Метод, который вызывается при столкновении с другим объектом
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем тэг объекта, с которым произошло столкновение
        switch (collision.gameObject.tag)
        {
            // Если столкнулись с объектом, имеющим тэг "Lose"
            case "Lose":
                // Отправляем сообщение платформе о нанесении урона
                platform.SendMessage("Damaged", 1);

                // Сбрасываем активацию мяча и его скорость
                ballIsActivated = false;
                rb.velocity = Vector2.zero;

                // Возвращаем платформу и мяч в начальное положение
                platform.position = new Vector2(0, -4);
                transform.position = new Vector2(0, -3);
                break;

            // В остальных случаях ничего не делаем
            default:
                break;
        }

        // Корректируем угол отражения только если мяч активирован и столкнулся с платформой
        if (ballIsActivated && collision.gameObject.tag == "Scored")
        {
            AdjustBallAngle();
        }
    }

    // Метод для увеличения очков
    private void Scored(uint points)
    {
        // Отправляем сообщение платформе о добавлении очков
        platform.SendMessage("AddScore", points);
    }
}
