using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformInput : MonoBehaviour
{
    [SerializeField][Tooltip("Скорость платформы")]
    private float speed;

    private Vector2 input;
    private Rigidbody2D rb;
    private List<Rigidbody2D> attachedBalls = new List<Rigidbody2D>();

    private void Start()
    {
        attachedBalls.Add(GameObject.FindGameObjectWithTag("Hit").GetComponent<Rigidbody2D>());
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Умножение на Vector2.right ограничивает движение только по горизотнальной плоскости
        rb.velocity = input * speed * Vector2.right; 

        foreach(var ball in attachedBalls)
        {
            ball.position = rb.position * Vector2.right + ball.transform.position * Vector2.up;
        }
    }

    public void attachBall(Rigidbody2D ball)
    {
        attachedBalls.Add(ball);
    }

    // Метод вызывается компонентом Player Input 
    private void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }

    // Метод вызывается компонентом Player Input 
    private void OnJump(InputValue inputValue)
    {
        if (attachedBalls.Count < 1) 
            return;

        activateBall(attachedBalls[0]);
        attachedBalls.RemoveAt(0);
    }

    public void sendAllBalls()
    {
        foreach (var ball in attachedBalls)
        {
            activateBall(ball);
        }
        attachedBalls.Clear();
    }

    public void activateBall(Rigidbody2D ball)
    {
        ball.velocity = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.forward) * Vector2.up * ball.GetComponent<BallScript>().getSpeed();
        ball.GetComponent<BallScript>().setDirection(ball.velocity.normalized);
    }
}
