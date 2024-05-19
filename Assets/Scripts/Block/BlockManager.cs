using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BlockManager : MonoBehaviour
{
    private List<GameObject> blocks = new List<GameObject>();

    [Tooltip("Макет блока")]
    public GameObject sampleBlock;

    [Tooltip("Поле, на котором будут спавниться блоки")]
    public RectTransform blocksField;

    [Tooltip("Размер блоков")]
    public Vector2 blockScale;

    [Tooltip("Автоматическое распределение блоков по полю")]
    public bool autoPlaceMode;

    [ConditionalHide(nameof(autoPlaceMode), true)]
    [Tooltip("Количество блоков")]
    public uint blocksAmount = 0;

    [ConditionalHide(nameof(autoPlaceMode), false, true)]
    [Tooltip("Количество рядов и строк блоков")]
    public Vector2 blocksRowsNumber;

    [ConditionalHide(nameof(autoPlaceMode), false, true)]
    [Tooltip("Расстояние между блоками (без учёта размера блока)")]
    public Vector2 blocksDistance;

    [ConditionalHide(nameof(autoPlaceMode), false, true)]
    [Tooltip("Отступы")]
    public Vector2 additionalOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (autoPlaceMode)
            autoPlace();
        else
            manualPlace();
    }

    private void autoPlace()
    {
        for (int i = 0; i < blocksAmount; i++)
        {
            blocks.Add(Instantiate(sampleBlock));
            blocks[i].transform.localScale = blockScale * sampleBlock.transform.localScale;
        }

        List<Vector2> points = getAutoPoints();

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.position = points[i];
        }
    }

    private void manualPlace()
    {
        Vector2 leftTopPoint = blocksField.offsetMax - new Vector2(blocksField.offsetMax.x - blocksField.offsetMin.x, 0);
        Debug.Log(leftTopPoint);

        for (int i = 0; i < blocksRowsNumber.y; i++)
        {
            for (int j = 0; j < blocksRowsNumber.x; j++)
            {
                GameObject tmp_block = Instantiate(sampleBlock);
                tmp_block.transform.localScale = blockScale * sampleBlock.transform.localScale;
                tmp_block.transform.position = leftTopPoint + new Vector2
                    (
                        ((tmp_block.transform.localScale.x + blocksDistance.x) * j) + additionalOffset.x + (tmp_block.transform.localScale.x / 2),
                        -(((tmp_block.transform.localScale.y + blocksDistance.y) * i) + additionalOffset.y + (tmp_block.transform.localScale.y / 2))
                    );

                blocks.Add(tmp_block);
            }
        }
    }

    private List<Vector2> getAutoPoints()
    {
        List<Vector2> points = new List<Vector2>();

        Vector2 max = blocksField.offsetMax;
        Vector2 min = blocksField.offsetMin;
        Vector2 distance = max - min;

        // Формула отсюда: https://math.stackexchange.com/questions/1039482/how-to-evenly-space-a-number-of-points-in-a-rectangle
        float y_points = Mathf.Sqrt((distance.x / distance.y) * blocksAmount + (((distance.x - distance.y) * (distance.x - distance.y)) / (4 * distance.y * distance.y)))
            - ((distance.x - distance.y) / (2 * distance.y));

        float x_points = (blocksAmount / y_points);

        uint missingPoints = blocksAmount - (uint)(Mathf.Floor(x_points) * Mathf.Round(y_points));

        if (missingPoints > 0)
            y_points++;

        for (uint i = 0; i < y_points; i++)
        {
            for (uint j = 0; j < x_points; j++)
            {
                float y_point = min.y + (0.5f + i) * (distance.y / y_points);
                float x_point = min.x + (0.5f + j) * (distance.x / x_points);

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
            SceneManager.LoadScene("You Win");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
