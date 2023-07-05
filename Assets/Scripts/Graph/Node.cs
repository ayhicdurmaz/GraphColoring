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
        foreach (var edge in node.Edges)
        {
            if (edge.Source.Value == edge.Target.Value)
            {
                edge.Line.startColor = Color.red;
                edge.Line.endColor = Color.red;
            }
            else
            {
                edge.Line.startColor = Color.white;
                edge.Line.endColor = Color.white;
            }
        }
    }
}