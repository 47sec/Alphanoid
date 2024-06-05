using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class BlockManager : MonoBehaviour
{
    private List<BlocksField> blocksFields = new List<BlocksField>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform child in transform)
        {
            blocksFields.Add(child.GetComponent<BlocksField>());
            blocksFields[blocksFields.Count - 1].Init();
        }
    }

    public void DestroyField(BlocksField field)
    {
        blocksFields.Remove(field);
        Destroy(field.gameObject);
    }

    public void DestroyBlock(Block block)
    {
        foreach(var field in blocksFields)
        {
            if (field.Contains(block))
            {
                field.DestroyBlock(block);
                break;
            }
        }

        if (blocksFields.Count == 0)
        {
            SceneManager.LoadScene("You Win");
        }
    }

}
