using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BlockManager : MonoBehaviour
{
    [Tooltip("������� �����")]
    public List<GameObject> blocks = new List<GameObject>();

    [Tooltip("����� �����")]
    public GameObject sampleBlock;

    [Tooltip("�����")]
    public GameObject player;

    [Tooltip("����, �� ������� ����� ���������� �����")]
    public RectTransform blocksField;

    [Tooltip("������ ������")]
    public Vector2 blockScale;

    [Tooltip("������������� ������������ ����� �� ����")]
    public bool autoPlaceMode;

    [ConditionalHide(nameof(autoPlaceMode), true, false)]
    [Tooltip("���������� ������")]
    public uint blocksAmount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < blocksAmount; i++)
        {
            blocks.Add(Instantiate(sampleBlock));
            blocks[i].transform.localScale = blockScale * sampleBlock.transform.localScale;
        }

        List<Vector2> points = getPoints();

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.position = points[i];
        }
    }

    private List<Vector2> getPoints()
    {
        List<Vector2> points = new List<Vector2>();

        Vector2 max = blocksField.offsetMax;
        Vector2 min = blocksField.offsetMin;
        Vector2 distance = max - min;

        // ������� ������: https://math.stackexchange.com/questions/1039482/how-to-evenly-space-a-number-of-points-in-a-rectangle
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
