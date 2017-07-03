using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    [Header("TableOptions")]
    public int NumberOfCellsOnAxisX = 10;
    public int NumberOfCellsOnAxisY = 10;
    public GameObject CellPrefab;
    public GameObject[] UnitPrefabArray;
    public Unit[] UnitArray;

    void Awake()
    {
        GenerateNewTable();

        UnitArray = new Unit[1];
        UnitArray[0] = Instantiate(UnitPrefabArray[0]).GetComponent<Unit>();
        UnitArray[0].SetCell(1, 1);
    }


    void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN
        int i = 0;
        if (Input.GetMouseButtonDown(0))
#endif
#if UNITY_ANDROID
            for (int i = 0; i < Input.touchCount; ++i)
                if (Input.GetTouch(i).phase == TouchPhase.Began)
#endif
        {
            ScreenRay(i);
        }

    }

    private Cell[][] CellArray;
    public void GenerateNewTable()
    {
        CellArray = new Cell[NumberOfCellsOnAxisX][];
        for (int i = 0; i < NumberOfCellsOnAxisX; i++)
        {
            CellArray[i] = new Cell[NumberOfCellsOnAxisY];
            GameObject Line = new GameObject();
            Line.transform.parent = transform;
            Line.transform.localPosition = Vector3.zero;
            Line.name = "Line_" + i;
            for (int j = 0; j < NumberOfCellsOnAxisY; j++)
            {
                GameObject newCell = Instantiate(CellPrefab);
                newCell.transform.parent = Line.transform;
                if (i % 2 == 0)
                    newCell.transform.position = transform.position + new Vector3((float)i / 4 * 3, j) + new Vector3(0, 0.5f);
                else
                    newCell.transform.position = transform.position + new Vector3((float)i / 4 * 3, j);
                newCell.name = "Cell_" + i + "_" + j;
                CellArray[i][j] = newCell.GetComponent<Cell>();
                CellArray[i][j].indexX = i;
                CellArray[i][j].indexY = j;
                CellArray[i][j].SetType(1);
            }
        }
    }
    public Cell GetCell(int X, int Y)
    {
        if (X < 0 || Y < 0 || X >= NumberOfCellsOnAxisX || Y >= NumberOfCellsOnAxisY)
            return null;
        return CellArray[X][Y];
    }

    public void ScreenRay(int _i)
    {
        RaycastHit2D hitInfo = new RaycastHit2D();

#if UNITY_STANDALONE_WIN
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
#endif

#if UNITY_ANDROID
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(_i).position), Vector2.zero);
#endif

        if (hitInfo.collider)
        {
            UnitArray[0].DestinationCell = hitInfo.transform.gameObject.GetComponent<Cell>();
            UnitArray[0].SetArrayRoute();
            UnitArray[0].GetDerections(UnitArray[0].DestinationCell);
            UnitArray[0].StartTransform();

        }
    }
}
