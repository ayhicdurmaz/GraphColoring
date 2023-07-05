using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ColorCalculator
{
    Graph graph;

    private int nodeCount; // Number of nodes
    private List<int>[] adjacencyList; // List representing edges

    public ColorCalculator(Graph _graph)
    {
        graph = _graph;
        this.nodeCount = graph.Nodes.Count;
        adjacencyList = new List<int>[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            adjacencyList[i] = new List<int>();
        }
        foreach(var edge in graph.Edges){
            AddEdge(edge.Source.ID, edge.Target.ID);
        }
    }

    public void AddEdge(int node1, int node2)
    {
        adjacencyList[node1].Add(node2);
        adjacencyList[node2].Add(node1); // Since our graph is undirected
    }

    public int FindMinimumColors()
    {
        int[] nodeColors = new int[nodeCount]; // Array to store node colors
        for (int i = 0; i < nodeCount; i++)
        {
            nodeColors[i] = -1; // -1 means no color assigned yet
        }

        for (int currentNode = 0; currentNode < nodeCount; currentNode++)
        {
            List<int> neighborColors = new List<int>();

            foreach (int neighbor in adjacencyList[currentNode])
            {
                if (nodeColors[neighbor] != -1)
                {
                    neighborColors.Add(nodeColors[neighbor]);
                }
            }

            int availableColor = 0;
            while (neighborColors.Contains(availableColor))
            {
                availableColor++;
            }

            nodeColors[currentNode] = availableColor;
        }

        return nodeColors.Max() + 1;
    }
}

