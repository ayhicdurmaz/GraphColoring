using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    ColorCalculator colorCalculator;

    public int numberOfNodes;
    public int numberOfAdjacency;

    public GameObject nodePrefab;
    public LineRenderer linePrefab;

    GraphManager graphManager;
    Graph graph;

    private void Awake()
    {
        graphManager = new GraphManager(numberOfNodes, numberOfAdjacency, nodePrefab, linePrefab, this.gameObject);
        graph = graphManager.graph;
        colorCalculator = new(graph);
        Debug.Log(colorCalculator.FindMinimumColors());
    }

    private void Update()
    {
        OnTouch();
    }

    void OnTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hitInfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.tag == "Node")
                {
                    graph.Nodes[int.Parse(hitInfo.transform.gameObject.name)].Value++;
                }
            }
        }
    }    

}
