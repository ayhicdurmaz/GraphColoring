using UnityEngine;

public class GraphGenerator
{
    public Graph graph;
    public int numberOfNodes;
    public int numberOfAdjacecny;

    public GraphGenerator(int _numberOfNodes, int _numberOfAdjacecny)
    {
        Debug.Log("Start;");
        graph = new();
        numberOfNodes = _numberOfNodes;
        numberOfAdjacecny = _numberOfAdjacecny < (numberOfNodes - 1) ? (numberOfNodes - 1) : _numberOfAdjacecny > ((numberOfNodes * (numberOfNodes - 1)) / 2) ? ((numberOfNodes * (numberOfNodes - 1)) / 2) : _numberOfAdjacecny;
        SetNodes();
    }

    public void SetNodes()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            Node node = new()
            {
                ID = i
            };
            graph.Nodes.Add(node);
        }
    }


    public Graph CreateGraph()
    {
        int count = 0;
        int attempts = 0;
        int maxAttempts = numberOfAdjacecny * 100;

        for (int i = 0; i < (numberOfNodes - 1); i++)
        {
            Edge edge = new()
            {
                Source = graph.Nodes[i],
                Target = graph.Nodes[Random.Range(i + 1, numberOfNodes)]
            };
            graph.Edges.Add(edge);
        }

        while (count < (numberOfAdjacecny - numberOfNodes) + 1 && attempts < maxAttempts)
        {
            int randomValue = Random.Range(0, numberOfNodes - 1);
            Edge edge = new()
            {
                Source = graph.Nodes[randomValue],
                Target = graph.Nodes[Random.Range(randomValue + 1, numberOfNodes)]
            };

            if (graph.Edges.FindIndex(x => x.Source == edge.Source && x.Target == edge.Target) == -1)
            {
                graph.Edges.Add(edge);
                count++;
            }
            attempts++;
        }
        return graph;
    }
}