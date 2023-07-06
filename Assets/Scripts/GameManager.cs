using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    ColorCalculator colorCalculator;
    GraphManager graphManager;
    Graph graph;

    public int colorLength;

    public int numberOfNodes;
    public int numberOfAdjacency;

    public GameObject nodePrefab;
    public LineRenderer linePrefab;

    public GameObject colorHolderPrefab;
    public Transform colorContainer;
    public Color[] colorsForGame = new Color[12];
    public Color selectedColor = new();

    public float offsetX, offsetY;
    public Vector2 gamePlayArea;

    public bool gameOver = false;

    private void Awake()
    {
        gamePlayArea = GetGamePlayArea();
        Singleton.colorList = colorsForGame;
        LevelDesign();
    }

    private void Start()
    {
        SetColorsOnScene();
    }

    private void Update()
    {
        OnTouch();
        if (gameOver)
        {
            Restart();
        }
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LevelDesign()
    {
        numberOfNodes = Random.Range(3, 8);
        numberOfAdjacency = Random.Range((numberOfNodes - 1), ((numberOfNodes * (numberOfNodes - 1)) / 2));
        graphManager = new GraphManager(numberOfNodes, numberOfAdjacency, (gamePlayArea.x * 2), (gamePlayArea.y * 2), nodePrefab, linePrefab, this.gameObject);
        graph = graphManager.graph;
        colorCalculator = new(graph);
        colorLength = colorCalculator.FindMinimumColors();
    }

    private void OnTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hitInfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.tag == "Node")
                {
                    graph.Nodes[int.Parse(hitInfo.transform.gameObject.name)].Value = System.Array.IndexOf(Singleton.colorList, selectedColor);
                    IsAllEdgesTrue();
                }
                if (hitInfo.transform.tag == "Color")
                {
                    selectedColor = hitInfo.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                }
            }

        }
    }

    private void SetColorsOnScene()
    {

        float gapBetweenColorHolders = (gamePlayArea.x * 2) / colorLength;

        Vector3 colorHoldersPositions = new(((gapBetweenColorHolders / 2) - gamePlayArea.x), -4f, 0);

        for (int i = 0; i < colorLength; i++)
        {
            GameObject _colorHolder = Instantiate(colorHolderPrefab, colorHoldersPositions, Quaternion.identity, colorContainer);
            _colorHolder.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Singleton.colorList[i];
            colorHoldersPositions += new Vector3(gapBetweenColorHolders, 0, 0);
        }
        selectedColor = Singleton.colorList[1];
    }

    public void IsAllEdgesTrue()
    {
        foreach (var edge in graph.Edges)
        {
            if (!edge.IsEdgeTrue)
            {
                gameOver = false;
                break;
            }
            else
            {
                gameOver = true;
            }
        }
    }

    private Vector2 GetGamePlayArea()
    {
        Vector2 _gamePlayArea = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - new Vector3(offsetX, offsetY);
        return _gamePlayArea;
    }


}
