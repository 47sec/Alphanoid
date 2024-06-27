using System;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;

    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        CreateEdgeCollider();
    }

    private void CreateEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edgeCollider.SetPoints(edges);
    }
}
