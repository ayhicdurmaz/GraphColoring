using System.Drawing;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public List<Node> Nodes = new();
    public List<Edge> Edges = new();

    public Vector2[] GetSurroundingRectangle()
    {
        Vector2[] rectangle = new Vector2[3];

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        foreach (var node in Nodes)
        {
            if (node.Position.x < minX)
                minX = node.Position.x;
            if (node.Position.x > maxX)
                maxX = node.Position.x;
            if (node.Position.y < minY)
                minY = node.Position.y;
            if (node.Position.y > maxY)
                maxY = node.Position.y;
        }

        float centerX = (minX + maxX) / 2f;
        float centerY = (minY + maxY) / 2f;
        Vector2 centerPoint = new(centerX, centerY);

        rectangle[0] = centerPoint;
        rectangle[1] = new Vector2(minX, minY);
        rectangle[2] = new Vector2(maxX, maxY);

        return rectangle;
    }

    public override string ToString()
    {
        string stringNodesAndEdges = "";

        foreach (var node in Nodes)
        {
            stringNodesAndEdges += node.ID + ", ";
        }
        stringNodesAndEdges += '\n';
        stringNodesAndEdges += "{";

        foreach (var edge in Edges)
        {
            stringNodesAndEdges += "(" + edge.Source.ID + "," + edge.Target.ID + ") , ";
        }

        stringNodesAndEdges += "}";

        return stringNodesAndEdges;
    }
}
