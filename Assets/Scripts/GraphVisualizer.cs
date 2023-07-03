using UnityEngine;

public class GraphVisualizer : MonoBehaviour
{
    Graph graph;
    GraphGenerator graphGenerator;

    public float areaEdge;
    public int numberOfNodes;
    public int numberOfAdjacecny;

    public GameObject nodePrefabs;
    public LineRenderer lineRendererPrefab;
    public void Start()
    {
        graphGenerator = new GraphGenerator(numberOfNodes, numberOfAdjacecny);
        graph = graphGenerator.CreateGraph();
        Debug.Log(graph.ToString());
        Layout();

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

    public float radius;

    public void Layout()
    {
        float angleStep = 2 * Mathf.PI / graph.Nodes.Count;
        float currentAngle = 0f;

        foreach (var node in graph.Nodes)
        {
            float x = Mathf.Cos(currentAngle) * radius;
            float y = Mathf.Sin(currentAngle) * radius;
            node.Position = new Vector2(x, y);
            currentAngle += angleStep;
        }
    }

    // public void InitializeNodes(){
    //     foreach(var node in graph.Nodes){
    //         node.Position = new Vector2(Random.Range(-areaEdge,areaEdge), Random.Range(-areaEdge,areaEdge));
    //         node.Velocity = Vector2.zero;
    //     }
    // }

    // public void Layout()
    // {
    //     const float k = 0.1f; // Yay katsayısı
    //     const float c = 0.2f; // Elektrostatik katsayısı

    //     // İterasyon sayısı
    //     const int iterations = 100;

    //     for (int i = 0; i < iterations; i++)
    //     {
    //         // Kuvvetleri sıfırla
    //         foreach (var node in graph.Nodes)
    //         {
    //             float fx = 0.0f; // X eksenindeki kuvvet
    //             float fy = 0.0f; // Y eksenindeki kuvvet

    //             // Elektrostatik itme kuvvetini hesapla
    //             foreach (var otherNode in graph.Nodes)
    //             {
    //                 if (node != otherNode)
    //                 {
    //                     Vector2 direction = otherNode.Position - node.Position;
    //                     float distance = direction.magnitude;

    //                     float force = c / (distance * distance);

    //                     direction.Normalize();
    //                     Vector2 repulsionForce = direction * force;

    //                     fx += repulsionForce.x;
    //                     fy += repulsionForce.y;
    //                 }
    //             }

    //             // Yay çekme kuvvetini hesapla
    //             foreach (var edge in graph.Edges)
    //             {
    //                 if (edge.Source.ID == node.ID)
    //                 {
    //                     var targetNode = graph.Nodes.Find(n => n.ID == edge.Target.ID);
    //                     Vector2 direction = targetNode.Position - node.Position;
    //                     float distance = direction.magnitude;

    //                     float force = k * distance;

    //                     direction.Normalize();
    //                     Vector2 attractionForce = direction * force;

    //                     fx += attractionForce.x;
    //                     fy += attractionForce.y;
    //                 }
    //             }

    //             // Konumları güncelle
    //             node.Position += new Vector2(fx, fy);
    //         }
    //     }
    // }
}
