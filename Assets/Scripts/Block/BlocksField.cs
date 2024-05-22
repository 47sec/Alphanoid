using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Rendering.Universal;

public class BlocksField : MonoBehaviour
{
    private RectTransform blocksField;
    private List<Block> blocks = new List<Block>();

    public enum Mode { FullAuto, SemiAuto, SemiManual, Manual };
    public enum Alignment { Left, Center, Right };

    public Mode mode;

    [ConditionalEnumHide(nameof(mode), (int)Mode.FullAuto, (int)Mode.SemiAuto, (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("����� �����")]
    public Block sampleBlock;

    [ConditionalEnumHide(nameof(mode), (int)Mode.FullAuto, (int)Mode.SemiAuto, (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("������ ������")]
    public Vector2 blockScale;


    [ConditionalEnumHide(nameof(mode), (int)Mode.FullAuto, HideInInspector = true)]
    [Tooltip("���������� ������")]
    public uint blocksAmount = 0;


    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiAuto, (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("���������� ����� ������� (��� ����� ������� �����)")]
    public Vector2 blocksDistance;

    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiAuto, (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("�������")]
    public Vector2 additionalOffset;

    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiAuto, (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("������������ ������")]
    public Alignment alginmentType;


    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiAuto, HideInInspector = true)]
    [Tooltip("���������� ����� � ����� ������")]
    public Vector2 blocksRowsNumber;
    
    
    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("������ ������������ ���-�� ������ � ����� (������ � Semi-Manual ������)")]
    public List<uint> rows = new List<uint>();

    public BlocksField Init()
    {
        blocksField = GetComponent<RectTransform>();

        foreach (Transform child in transform)
            blocks.Add(child.GetComponent<Block>());

        switch (mode)
        {
            case Mode.FullAuto:
                autoPlace();
                break;
            case Mode.SemiAuto:
                semiAutoPlace();
                break;
            case Mode.SemiManual:
                semiManualPlace();
                break;
        }


        if (blocks.Count == 0)
            GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>().DestroyField(this);

        return this;
    }

    private void autoPlace()
    {
        for (int i = 0; i < blocksAmount; i++)
        {
            blocks.Add(Instantiate(sampleBlock, transform));
            blocks[i].transform.localScale = blockScale * sampleBlock.transform.localScale;
        }

        List<Vector2> points = getAutoPoints();

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.position = points[i];
        }
    }

    private void semiAutoPlace()
    {
        rows.Clear();

        for (int i = 0; i < blocksRowsNumber.y; i++)
            rows.Add((uint)blocksRowsNumber.x);

        place();
    }

    private void semiManualPlace()
    {
        place();
    }

    private void place()
    {
        switch (alginmentType)
        {
            case Alignment.Left:
                placeLeft();
                break;
            case Alignment.Center:
                placeCenter();
                break;
            case Alignment.Right:
                placeRight();
                break;
        }
    }

    private void placeLeft()
    {
        //����� ������
        Vector2 leftTopPoint = blocksField.offsetMax - new Vector2(blocksField.offsetMax.x - blocksField.offsetMin.x, 0) + additionalOffset
            + new Vector2((blockScale.x * sampleBlock.transform.localScale.x) / 2, -(blockScale.y * sampleBlock.transform.localScale.y) / 2);

        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[i]; j++)
            {
                Block tmp_block = Instantiate(sampleBlock);
                tmp_block.transform.localScale = blockScale * sampleBlock.transform.localScale;
                tmp_block.transform.position = leftTopPoint + new Vector2
                    (
                        (tmp_block.transform.localScale.x + blocksDistance.x) * j,
                        -((tmp_block.transform.localScale.y + blocksDistance.y) * i)
                    );
                blocks.Add(tmp_block);
            }
        }
    }

    private void placeCenter()
    {
        //����� ������
        Vector2 centerTopPoint = blocksField.offsetMax - new Vector2((blocksField.offsetMax.x - blocksField.offsetMin.x) / 2, 0) + additionalOffset;

        Vector2 localBlockScale = blockScale * sampleBlock.transform.localScale;


        for (int i = 0; i < rows.Count; i++)
        {
            float odd = (rows[i] % 2 == 0 ? 1f : 0f) / 2;

            //�����, ������ ������� ��������� (����� ������)
            Vector2 startPoint = centerTopPoint - new Vector2((int)(rows[i] / 2) * (localBlockScale.x + blocksDistance.x) - (odd * (localBlockScale.x + blocksDistance.x)), 0);
            // �������� ���������� ������ * ������ �����(��������)
            // (�������� ���������� ������ * ������ �����) + �������� ������ �����(������)

            for (int j = 0; j < rows[i]; j++)
            {
                Block tmp_block = Instantiate(sampleBlock, transform);
                tmp_block.transform.localScale = localBlockScale;

                tmp_block.transform.position = startPoint + new Vector2
                    (
                        ((tmp_block.transform.localScale.x + blocksDistance.x) * j),
                        -(((tmp_block.transform.localScale.y + blocksDistance.y) * i) + additionalOffset.y + (tmp_block.transform.localScale.y / 2))
                    );
                blocks.Add(tmp_block);
            }
        }
    }

    private void placeRight()
    {
        //������ ������
        Vector2 rightTopPoint = blocksField.offsetMax + additionalOffset
            - ((blockScale * sampleBlock.transform.localScale) / 2);


        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[i]; j++)
            {
                Block tmp_block = Instantiate(sampleBlock, transform);
                tmp_block.transform.localScale = blockScale * sampleBlock.transform.localScale;
                tmp_block.transform.position = rightTopPoint + new Vector2
                    (
                        -((tmp_block.transform.localScale.x + blocksDistance.x) * j),
                        -((tmp_block.transform.localScale.y + blocksDistance.y) * i)
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

    public bool Contains(Block block)
    {
        return blocks.Contains(block);
    }

    public void DestroyBlock(Block block)
    {
        //int blockId = blocks.FindIndex(int id => { id == block.GetInstanceID()});

        blocks.Remove(block);
        Destroy(block.gameObject);

        if (blocks.Count == 0)
            GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>().DestroyField(this);
    }

}
