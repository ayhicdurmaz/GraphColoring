using System;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int ID { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public GameObject node;

    public List<Edge> Edges = new();

    private int value;

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                this.value = value;
                OnValueChanged?.Invoke(this);
            }
        }
    }

    public event Action<Node> OnValueChanged = OnNodeValueChanged;

    private static void OnNodeValueChanged(Node node)
    {
        node.node.GetComponent<SpriteRenderer>().color = Singleton.colorList[node.Value];
        foreach (var edge in node.Edges)
        {
            if (edge.Source.Value == edge.Target.Value)
            {
                edge.Line.startColor = Singleton.colorList[11];
                edge.Line.endColor = Singleton.colorList[11];
                edge.IsEdgeTrue = false;
            }
            else
            {
                edge.Line.startColor = Singleton.colorList[10];
                edge.Line.endColor = Singleton.colorList[10];
                edge.IsEdgeTrue = true;
            }
        }
    }
}