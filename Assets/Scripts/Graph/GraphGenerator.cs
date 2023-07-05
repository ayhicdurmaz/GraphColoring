using UnityEngine;

public class GraphGenerator
{
    public Graph graph;
    public int numberOfNodes;
    public int numberOfAdjacecny;

    public GraphGenerator(int _numberOfNodes, int _numberOfAdjacecny)
    {
        graph = new();
        numberOfNodes = _numberOfNodes;
        numberOfAdjacecny = _numberOfAdjacecny;
        graph.SetNodes(numberOfNodes);
    }

    // TODO dont use attempts use a list then remove selected index.
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

        foreach(var node in graph.Nodes){
            foreach(var edge in graph.Edges){
                if(edge.Source == node || edge.Target == node){
                    node.Edges.Add(edge);
                }
            }
        }
        
        return graph;
    }
}