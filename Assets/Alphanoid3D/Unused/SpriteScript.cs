using UnityEngine;
using UnityEngine.UI;

public class SpriteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Image image;
    public float maximum = 1f;
    public float minimum = 0f;
    public float red = 0.5f;
    public float blue = 0.5f;
    public float green = 0.5f;
    public float alpha = 0.5f;
    public float speed = 0.2f;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Lerp(minimum, maximum, alpha);

        alpha += speed * Time.deltaTime;

        image.color = new Color(red, green, blue, alpha);

        if (alpha > 1f || alpha < 0f)
        {
            alpha = maximum;

            float temp = maximum;
            maximum = minimum;
            minimum = temp;

            speed = -speed;
        }
    }
}
