using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class BlockManager : MonoBehaviour
{
    [Tooltip("������� �����")]
    public List<GameObject> blocks = new List<GameObject>();

    [Tooltip("����� �����")]
    public GameObject sampleBlock;

    [Tooltip("�����")]
    public GameObject player;

    [Tooltip("����, �� ������� ����� ���������� �����")]
    public GameObject blocksField;

    [Tooltip("������ ������")]
    public Vector2 scale = new Vector2(1, 1);

    [Tooltip("���������� ������")]
    public uint blocksAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < blocksAmount; i++)
        {
            blocks.Add(Instantiate(sampleBlock));
            blocks[i].transform.position.Scale(scale);
        }


    }

    private List<Vector2> getPoints()
    {
        List<Vector2> points = new List<Vector2>();

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
