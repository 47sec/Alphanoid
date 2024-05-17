using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class BlockManager : MonoBehaviour
{
    [Tooltip("Игровые блоки")]
    public List<GameObject> blocks = new List<GameObject>();

    [Tooltip("Макет блока")]
    public GameObject sampleBlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blocks.Add(Instantiate(sampleBlock));
        blocks.Add(Instantiate(sampleBlock));
        blocks[0].transform.position = new Vector2(-1, -1);
        blocks[1].transform.position = new Vector2(1, 1);
    }


    // Update is called once per frame
    void Update()
    {
    }
}
