using System;
using UnityEngine;

public class Edge
{
    public Node Source { get; set; }
    public Node Target { get; set; }

    public bool IsEdgeTrue { get; set; }
    
    public LineRenderer Line;
}
