using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class has two methods named Grid and CreateGraph. 
/// The method Grid, generate a grid of given rows and column values.
/// the method CreateGraph, creates a pattern of the given prefab.
/// </summary>
public class GraphGenerator : MonoBehaviour
{
    public int numberOfRowsAnsColumns = 15;
    public int numberOfNodes;

    public GameObject nodePrefabs;

    private void Start() //(TODO) remove this method
    {
        GameObject nodesParents = new GameObject("ParentOfNodes");
        CreateGraph(nodesParents);
    }

    /// <summary>
    /// Calculates the grid area according to the borders of the screen. Saves the center coordinates of each region.
    /// </summary>
    /// <returns>Returns a 2D array of Vector3.</returns>
    public Vector3[,] Grid()
    {
        Vector3[,] _grid = new Vector3[numberOfRowsAnsColumns, numberOfRowsAnsColumns]; // Make dimensions length according to screen positions

        float xOffset = ((GetScreenBorders().x * 2) / 10);
        float xGrid = (GetScreenBorders().x) - (xOffset);
        float gridLength = (xGrid * 2) / numberOfRowsAnsColumns;

        float x0 = (-xGrid) + (gridLength / 2);
        float y0 = (numberOfRowsAnsColumns - 1) * (gridLength / 2);

        int i = 0, j = 0;

        for (float x = x0; i < numberOfRowsAnsColumns; x += (gridLength))
        { //y < (((numberOfRowsAnsColumns * 2) - 2) * gridLength)
            for (float y = y0; j < numberOfRowsAnsColumns; y -= (gridLength))
            {
                _grid[i, j] = new Vector3(x, y, 0);
                j++;
            }
            j = 0;
            i++;
        }

        return _grid;
    }

    /// <summary>
    /// This code randomly select some positions from grid. Instantiate nodes to those points.
    /// </summary>
    /// <param name="parentOfGraph"></param>
    /// <returns>Returns a array of GameObject</returns>
    public GameObject[] CreateGraph(GameObject parentOfGraph)
    {
        GameObject[] nodes = new GameObject[numberOfNodes];
        Vector3[,] gridPos = Grid();
        bool[,] gridFillness = new bool[numberOfRowsAnsColumns, numberOfRowsAnsColumns];

        int count = 0;
        int[] rowCount = new int[numberOfRowsAnsColumns];
        int[] columnCount = new int[numberOfRowsAnsColumns];

        int maxAttempts = 100;
        int attempts = 0;


        while (count < numberOfNodes && attempts < maxAttempts)
        {

            int row = Random.Range(0, numberOfRowsAnsColumns);
            int column = Random.Range(0, numberOfRowsAnsColumns);

            if (!gridFillness[row, column] && rowCount[row] < 2 && columnCount[column] < 2)
            {
                nodes[count] = Instantiate(nodePrefabs, gridPos[row, column], Quaternion.identity, parentOfGraph.transform);
                nodes[count].name = "Node(" + count + ")";
                rowCount[row]++;
                columnCount[column]++;
                gridFillness[row, column] = true;
                count++;
            }

            attempts++;
        }

        return nodes;
    }

    /// <summary>
    /// This code gets screen borders.
    /// </summary>
    /// <returns>Returns a Vector3</returns>
    public Vector3 GetScreenBorders()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
}
