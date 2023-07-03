using System.Collections.Generic;

public class Graph
{
    public List<Node> Nodes = new();
    public List<Edge> Edges = new();

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
