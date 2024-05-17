using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class BlockManager : MonoBehaviour
{
    [Tooltip("Игровые блоки")]
    public List<GameObject> blocks = new List<GameObject>();

    [Tooltip("Макет блока")]
    public GameObject sampleBlock;

    [Tooltip("Игрок")]
    public GameObject player;

    [Tooltip("Поле, на котором будут спавниться блоки")]
    public RectTransform blocksField;

    [Tooltip("Размер блоков")]
    public Vector2 scale = new Vector2(1, 1);

    [Tooltip("Количество блоков")]
    public uint blocksAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < blocksAmount; i++)
        {
            blocks.Add(Instantiate(sampleBlock));
            blocks[i].transform.position.Scale(scale);
        }

        List<Vector2> points = getPoints();

        for (int i = 0; i < points.Count; i++)
        {
            blocks[i].transform.position = points[i];
        }
    }

    private List<Vector2> getPoints()
    {
        List<Vector2> points = new List<Vector2>();

        float x_size = blocksField.transform.localScale.x;
        float y_size = blocksField.transform.localScale.y;

        // Формула отсюда: https://math.stackexchange.com/questions/1039482/how-to-evenly-space-a-number-of-points-in-a-rectangle
        uint y_points = (uint)(Mathf.Floor(Mathf.Sqrt((x_size / y_size) * blocksAmount + (Mathf.Pow(x_size - y_size, 2) / 4 * Mathf.Pow(y_size, 2)))
            - ((x_size - y_size) / 2 * y_size)));

        if (y_points == 0)
            y_points = 1;

        Debug.Log(x_size + " " + y_size);
        Debug.Log(y_points);
        uint x_points = (uint)(Mathf.Floor(blocksAmount / y_points));

        uint missingPoints = blocksAmount - (x_points * y_points);
        Debug.Log(x_points + " " + y_points + " " + missingPoints);

        Vector2 max = blocksField.offsetMax;
        Vector2 min = blocksField.offsetMin;

        Vector2 distance = max - min;

        Debug.Log(max);
        Debug.Log(min);
        Debug.Log(distance);

        for (uint i = 0; i < y_points; i++)
        {
            for (uint j = 0; j < x_points; j++)
            {
                float x_point = min.x + j * (distance.x / x_points) + 0.384f;
                float y_point = min.y + i * (distance.y / y_points) + 0.149f;
                //Debug.Log(x_point + " " + y_point);
                points.Add(new Vector2(x_point, y_point));
            }
        }

        return points;
    }

    private void DestroyBlock(Block block)
    {
        //int blockId = blocks.FindIndex(int id => { id == block.GetInstanceID()});

        blocks.Remove(block.gameObject);
        Destroy(block.gameObject);

        if (blocks.Count == 0)
        {
            player.SendMessage("Win");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
