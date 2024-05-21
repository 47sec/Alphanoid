using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class BlockManager : MonoBehaviour
{
    [Tooltip("ѕол€, на которых будут спавнитьс€ блоки")]
    public List<BlocksField> blocksFields = new List<BlocksField>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var field in blocksFields)
        {
            field.Init();
        }
    }

    
    // Update is called once per frame
    void Update()
    {
    }

    private void DestroyBlock(Block block)
    {
        foreach(var field in blocksFields)
        {
            if (field.DestroyBlock(block))
            {
                blocksFields.Remove(field);
                Destroy(field.gameObject);
            }
        }

        if (blocksFields.Count == 0)
        {
            SceneManager.LoadScene("You Win");
        }
    }

}
