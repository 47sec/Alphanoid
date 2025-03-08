using UnityEngine;

public class Player : MonoBehaviour
{
    public Score score;
    public HealthPoints healthPoints;
    private void Awake()
    {
        score = gameObject.GetComponent<Score>();
        healthPoints = gameObject.GetComponent<HealthPoints>();
    }
}