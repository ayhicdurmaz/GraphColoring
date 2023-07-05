using System.ComponentModel;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class GraphManager
{
    private ForceDirectedLayout forceDirectedLayout;
    public Graph graph { get; }

    private int numberOfNodes;
    private int numberOfAdjacency;

    private GameObject parent;
    private GameObject nodePrefab;
    private LineRenderer linePrefab;

    public GraphManager(int _numberOfNodes, int _numberOfAdjaceny, GameObject _nodePrefabs, LineRenderer _linePrefabs, GameObject _parent)
    {
        numberOfNodes = _numberOfNodes;
        numberOfAdjacency = _numberOfAdjaceny;
        nodePrefab = _nodePrefabs;
        linePrefab = _linePrefabs;
        parent = _parent;

        forceDirectedLayout = new ForceDirectedLayout();
        graph = forceDirectedLayout.GetLayout(numberOfNodes, numberOfAdjacency);

        SetAndVisualizeGraph();
    }

    private void SetAndVisualizeGraph()
    {
        GameObject graphHolder = new("GraphHolder");
        graphHolder.transform.parent = parent.transform;

        foreach (var node in graph.Nodes)
        {
            GameObject _node = GameObject.Instantiate(nodePrefab, node.Position, Quaternion.identity, graphHolder.transform);
            _node.name = node.ID.ToString();
            node.node = _node;
        }

        foreach (var edge in graph.Edges)
        {
            LineRenderer _line = GameObject.Instantiate(linePrefab, graphHolder.transform);
            _line.name = "Line (" + edge.Source.ID + ", " + edge.Target.ID + ")";

            _line.startColor = new Color(1, 0.521f, 0.317f);
            _line.endColor = new Color(1, 0.521f, 0.317f);

            _line.startWidth = 0.025f;
            _line.endWidth = 0.025f;

            _line.positionCount = 2;
            _line.SetPosition(0, edge.Source.Position);
            _line.SetPosition(1, edge.Target.Position);

            edge.Line = _line;
        }

        graphHolder.transform.position = new Vector3(graph.GetSurroundingRectangle()[0].x, 0, 0) * forceDirectedLayout.GetScaleRatio() * -1;
        graphHolder.transform.position += new Vector3(0, 1, 0);
    }


}
