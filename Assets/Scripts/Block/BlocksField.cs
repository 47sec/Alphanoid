using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Rendering.Universal;
using System;
using NUnit.Framework.Internal;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Array2DEditor;

public class BlocksField : MonoBehaviour
{
    private RectTransform blocksField;
    private List<Block> blocks = new List<Block>();

    public enum Mode { SemiAuto, SemiManual, Manual };
    public enum Alignment { Left, Center, Right };

    public Mode mode;

    [Tooltip("Макет блоков")]
    public List<Block> sampleBlocks = new List<Block>();

    [Tooltip("Размер блоков")]
    public Vector2 blockScale;


    [Tooltip("Расстояние между блоками (без учёта размера блока)")]
    public Vector2 blocksDistance;

    [Tooltip("Отступы")]
    public Vector2 additionalOffset;

    [Tooltip("Выравнивание блоков")]
    public Alignment alginmentType;


    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiAuto, HideInInspector = true)]
    [Tooltip("Количество рядов и строк блоков")]
    public Vector2 blocksRowsNumber;


    [ConditionalEnumHide(nameof(mode), (int)Mode.SemiManual, HideInInspector = true)]
    [Tooltip("Ручное настраивание кол-ва блоков в рядах (только в Semi-Manual режиме)")]
    public List<uint> rows = new List<uint>();

    [ConditionalEnumHide(nameof(mode), (int)Mode.Manual, HideInInspector = true)]
    [Tooltip("Расположение блоков")]
    public Array2DBool mat = new Array2DBool();

    private Randomizer RNG = Randomizer.CreateRandomizer();

    public BlocksField Init()
    {
        blocksField = GetComponent<RectTransform>();

        foreach (Transform child in transform)
            blocks.Add(child.GetComponent<Block>());
        fixChances();

        switch (mode)
        {
            case Mode.SemiAuto:
                semiAutoPlace();
                break;
            case Mode.SemiManual:
                place();
                break;
            case Mode.Manual:
                manualPlace();
                break;

        }


        if (blocks.Count == 0)
            GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>().DestroyField(this);


        return this;
    }

    private void fixChances()
    {
        float sum = 0;
        foreach (var block in sampleBlocks)
        {
            sum += block.chanceMod;
        }

        foreach (var block in sampleBlocks)
        {
            block.setRelativeChance(sum);
        }

        sampleBlocks.Sort((x, y) => y.relativeChance.CompareTo(x.relativeChance));
    }

    Block getRandomBlock()
    {
        float rnd = (float)RNG.NextDouble();
        foreach (var block in sampleBlocks)
        {
            if (rnd < block.relativeChance)
            {
                return block;
            }
            rnd -= block.relativeChance;
        }

        return sampleBlocks[0];
    }

    //private void autoPlace()
    //{
    //    for (int i = 0; i < blocksAmount; i++)
    //    {
    //        blocks.Add(Instantiate(getRandomBlock(), transform));
    //        blocks[i].transform.localScale *= blockScale;
    //    }

    //    List<Vector2> points = getAutoPoints();

    //    for (int i = 0; i < blocks.Count; i++)
    //    {
    //        blocks[i].transform.position = points[i];
    //    }
    //}
    //private List<Vector2> getAutoPoints()
    //{
    //    List<Vector2> points = new List<Vector2>();

    //    Vector2 max = blocksField.offsetMax;
    //    Vector2 min = blocksField.offsetMin;
    //    Vector2 distance = max - min;

    //    // Формула отсюда: https://math.stackexchange.com/questions/1039482/how-to-evenly-space-a-number-of-points-in-a-rectangle
    //    float y_points = Mathf.Sqrt((distance.x / distance.y) * blocksAmount + (((distance.x - distance.y) * (distance.x - distance.y)) / (4 * distance.y * distance.y)))
    //        - ((distance.x - distance.y) / (2 * distance.y));

    //    float x_points = (blocksAmount / y_points);

    //    uint missingPoints = blocksAmount - (uint)(Mathf.Floor(x_points) * Mathf.Round(y_points));

    //    if (missingPoints > 0)
    //        y_points++;

    //    for (uint i = 0; i < y_points; i++)
    //    {
    //        for (uint j = 0; j < x_points; j++)
    //        {
    //            float y_point = min.y + (0.5f + i) * (distance.y / y_points);
    //            float x_point = min.x + (0.5f + j) * (distance.x / x_points);

    //            points.Add(new Vector2(x_point, y_point));
    //        }
    //    }

    //    return points;
    //}

    private void semiAutoPlace()
    {
        rows.Clear();

        for (int i = 0; i < blocksRowsNumber.y; i++)
            rows.Add((uint)blocksRowsNumber.x);

        place();
    }

    private void manualPlace()
    {
        rows.Clear();

        for (int i = 0; i < mat.GridSize.y; i++)
            rows.Add((uint)mat.GridSize.x);

        place();
    }

    private void place()
    {
        switch (alginmentType)
        {
            case Alignment.Left:
                {
                    Vector2 leftTopPoint = blocksField.offsetMax
                        - new Vector2(blocksField.offsetMax.x - blocksField.offsetMin.x, 0) // Точка слева сверху
                        + additionalOffset
                        + new Vector2(blockScale.x / 2, -blockScale.y / 2); // Отступ на половину ширины блока
                    placeAllRows(leftTopPoint, Vector2.right, 0);
                    break;
                }
            case Alignment.Center:
                {
                    Vector2 centerTopPoint = blocksField.offsetMax
                        - new Vector2((blocksField.offsetMax.x - blocksField.offsetMin.x) / 2, 0) // Точка по центру сверху
                        + additionalOffset
                        + new Vector2(0, -blockScale.y / 2); // Отступ на половину ширины блока
                    placeAllRows(centerTopPoint, Vector2.right, 1);
                    break;
                }
            case Alignment.Right:
                {
                    Vector2 rightTopPoint = blocksField.offsetMax // Точка справа сверху
                        + additionalOffset
                        - (blockScale / 2); // Отступ на половину ширины блока
                    placeAllRows(rightTopPoint, Vector2.left, 0);
                    break;
                }
        }
    }

    private void placeAllRows(Vector2 startingPoint, Vector2 dir, uint center)
    {
        for (int i = 0; i < rows.Count; i++)
        {
            Vector2 rowPoint = startingPoint
                + new Vector2(0, blockScale.y + blocksDistance.y) * i * Vector2.down; // Изменения высоты для каждого ряда
            placeRow(rowPoint, dir, rows[i], center, i);
        }
    }

    private void placeRow(Vector2 startingPoint, Vector2 dir, uint amount, uint center, int rowNumber)
    {
        for (int i = 0; i < amount; i++)
        {
            if (mode == Mode.Manual && !mat.GetCell(i, rowNumber))
                continue;

            float odd = (amount % 2 == 0 ? 1f : 0f) / 2; // Фигня для выравнивания по центру
            Block tmp_block = Instantiate(getRandomBlock(), transform);
            tmp_block.transform.localScale = blockScale;
            tmp_block.transform.position = startingPoint
                + new Vector2(dir.x * ((blockScale.x + blocksDistance.x) * i), 0) // Изменения координат для каждого отдельного блока
                - new Vector2((int)(amount / 2) * (blockScale.x + blocksDistance.x) - (odd * (blockScale.x + blocksDistance.x)), 0) * center; // Фигня для выравнивания по центру

            blocks.Add(tmp_block);
        }
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
