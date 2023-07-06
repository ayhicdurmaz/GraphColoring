using UnityEngine;
using UnityEngine.UI;

public class ForceDirectedLayout
{
    Graph graph;
    GraphGenerator graphGenerator;

    private const int iterations = 1000;
    private const float radius = 2;
    private const float g = 0.5f;
    private const float k = 1f;
    private const float springNormalLength = 0.5f;
    private const float dampingValue = 0.1f;

    public Graph GetLayout(int numberOfNodes, int numberOfAdjacecny, float targetWidth, float targetHeight)
    {
        graphGenerator = new GraphGenerator(numberOfNodes, numberOfAdjacecny);
        graph = graphGenerator.CreateGraph();

        InitializeNodes();

        for (int i = 0; i < iterations; i++)
        {
            Visualize();
        }

        Vector2 ratio = GetScaleRatio(targetWidth, targetHeight);

        foreach (var node in graph.Nodes)
        {
            FitIntoScreen(node, ratio);
        }

        Graph forceDirectedGraph = new()
        {
            Nodes = graph.Nodes,
            Edges = graph.Edges
        };

        return forceDirectedGraph;
    }

    public void InitializeNodes()
    {
        float angleStep = 2 * Mathf.PI / graph.Nodes.Count;
        float currentAngle = 0f;

        foreach (var node in graph.Nodes)
        {
            float x = Mathf.Cos(currentAngle) * radius;
            float y = Mathf.Sin(currentAngle) * radius;
            node.Position = new Vector2(x, y);
            node.Velocity = Vector2.zero;
            currentAngle += angleStep;
        }
    }

    public void Visualize()
    {
        foreach (var nodeA in graph.Nodes)
        {
            foreach (var nodeB in graph.Nodes)
            {
                if (nodeA != nodeB)
                {
                    ApplyRepulsiveForce(nodeA, nodeB);
                }
            }
        }

        foreach (Edge edge in graph.Edges)
        {
            ApplyAttractiveForce(edge.Source, edge.Target);
        }

        foreach (var node in graph.Nodes)
        {
            UpdateNodePositions(node);
        }
    }

    private void ApplyRepulsiveForce(Node nodeA, Node nodeB)
    {
        Vector2 direction = nodeA.Position - nodeB.Position;
        float distance = direction.magnitude;
        float repulsiveForce = (g * 1 * 1) / (distance * distance);
        nodeA.Velocity += direction.normalized * repulsiveForce;
    }

    private void ApplyAttractiveForce(Node source, Node target)
    {
        Vector2 direction = target.Position - source.Position;
        float distance = direction.magnitude;
        float springForce = k * (distance - springNormalLength);
        source.Velocity += direction.normalized * springForce;
        target.Velocity -= direction.normalized * springForce;
    }

    private void UpdateNodePositions(Node node)
    {
        node.Position += node.Velocity * Time.deltaTime;

        node.Velocity *= (1 - dampingValue);
    }

    public void FitIntoScreen(Node node, Vector2 ratio)
    {
        node.Position = Vector2.Scale(ratio, node.Position);
    }

    public Vector2 GetScaleRatio(float targetWidth, float targetHeight)
    {
        Vector2[] rectangle = graph.GetSurroundingRectangle();

        Vector2 scaleRatio;

        float sourceWidth = rectangle[2].x - rectangle[1].x;
        float sourceHeight = rectangle[2].y - rectangle[1].y;

        float sourceAspectRatio = sourceWidth / sourceHeight;
        float targetAspectRatio = targetWidth / targetHeight;

        // TODO change it to camera size
        if (sourceAspectRatio > targetAspectRatio)
        {
            float ratio = (sourceHeight * (targetWidth / sourceWidth)) / sourceHeight;
        
            scaleRatio = new Vector2(ratio, ratio);
        }
        else
        {
            float ratio = (sourceWidth * (targetHeight / sourceHeight)) / sourceWidth;

            scaleRatio = new Vector2(ratio, ratio);
        }

        return scaleRatio;
    }

}
