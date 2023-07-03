using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurchtermanReingoldLayout : MonoBehaviour
{
    Graph graph;
    GraphGenerator graphGenerator;

    [Header("Graph Settings")]
    public int numberOfNodes;
    public int numberOfAdjacecny;

    [Header("ForceDirectedLayout Settings")]
    public int iterations;
    public float radius;
    public float g;
    public float k;
    public float springNormalLength;
    public float dampingValue;

    [Header("Prefabs for Visualize")]
    public GameObject nodePrefabs;
    public LineRenderer lineRendererPrefab;

    public void Start()
    {
        graphGenerator = new GraphGenerator(numberOfNodes, numberOfAdjacecny);
        graph = graphGenerator.CreateGraph();

        InitializeNodes();

        for(int i = 0; i < iterations; i++){
            Visualize();
        }

        foreach (var node in graph.Nodes)
        {
            Instantiate(nodePrefabs, node.Position, Quaternion.identity, this.transform);
            Debug.LogFormat("Node ID: {0}, Position: {1}", node.ID, node.Position);
        }

        foreach(var edge in graph.Edges){
             // İki nokta arasında bir çizgi çizmek için yeni bir LineRenderer örneği oluştur
            LineRenderer lineRenderer = Instantiate(lineRendererPrefab, transform);

            // Çizgi rengini ayarla
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            // Çizgi kalınlığını ayarla
            lineRenderer.startWidth = 0.025f;
            lineRenderer.endWidth = 0.025f;

            // Çizgi noktalarını ayarla
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, edge.Source.Position);
            lineRenderer.SetPosition(1, edge.Target.Position);
        }
    }

    public void InitializeNodes(){
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

    public void Visualize(){
        //Repulsive Force
        foreach(var nodeA in graph.Nodes){
            foreach(var nodeB in graph.Nodes){
                if(nodeA != nodeB){
                    ApplyRepulsiveForce(nodeA, nodeB);
                }
            }
        }

        //Attractive Force
        foreach (Edge edge in graph.Edges)
        {
            ApplyAttractiveForce(edge.Source, edge.Target);
        }

        foreach(var node in graph.Nodes){
            UpdateNodePositions(node);
        }
    }

    private void ApplyRepulsiveForce(Node nodeA, Node nodeB){
        Vector2 direction = nodeA.Position - nodeB.Position;
        float distance = direction.magnitude;
        float repulsiveForce = (g * 1 * 1) / (distance * distance); // Use inverse square distance for repulsive force
        nodeA.Velocity += direction.normalized * repulsiveForce;
    }

    private void ApplyAttractiveForce(Node source, Node target){
        Vector2 direction = target.Position - source.Position;
        float distance = direction.magnitude;
        float springForce = k * (distance - springNormalLength); // Use linear distance for spring force
        source.Velocity += direction.normalized * springForce;
        target.Velocity -= direction.normalized * springForce;
    }

    private void UpdateNodePositions(Node node)
    {
        node.Position += node.Velocity * Time.deltaTime;
        node.Velocity *= (1 - dampingValue);
    }

}
