using System.Net.NetworkInformation;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private ForceDirectedLayout forceDirectedLayout;
    private Graph graph;

    public int numberOfNodes;
    public int numberOfAdjacency;

    public GameObject nodePrefab;
    public LineRenderer linePrefab;

    private void Start()
    {
        forceDirectedLayout = new ForceDirectedLayout();
        graph = forceDirectedLayout.GetLayout(numberOfNodes, numberOfAdjacency);

        ShowGraph();
    }

    private void ShowGraph()
    {
        GameObject graphHolder = new("GraphHolder");
        graphHolder.transform.parent = this.transform;

        foreach (var node in graph.Nodes)
        {
            GameObject _node = Instantiate(nodePrefab, node.Position, Quaternion.identity, graphHolder.transform);
            _node.name = "Node " + node.ID;
            node.node = _node;
        }

        foreach (var edge in graph.Edges)
        {
            LineRenderer _line = Instantiate(linePrefab, graphHolder.transform);
            _line.name = "Line (" + edge.Source.ID + ", " + edge.Target.ID + ")";

            _line.startColor = new Color(1, 0.521f, 0.317f);
            _line.endColor = new Color(1, 0.521f, 0.317f);

            _line.startWidth = 0.025f;
            _line.endWidth = 0.025f;

            _line.positionCount = 2;
            _line.SetPosition(0, edge.Source.Position);
            _line.SetPosition(1, edge.Target.Position);

            edge.line = _line;
        }

        graphHolder.transform.position = new Vector3(graph.GetSurroundingRectangle()[0].x, graph.GetSurroundingRectangle()[0].y, 0) * forceDirectedLayout.GetScaleRatio() * -1; 
        graphHolder.transform.position += new Vector3(0,1,0);
    }
}
