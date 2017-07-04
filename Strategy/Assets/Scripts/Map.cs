using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
    [Header("TableOptions")]
    public int NumberOfCellsOnAxisX = 10;
    public int NumberOfCellsOnAxisY = 10;
    public GameObject CellPrefab;
    public GameObject[] UnitPrefabArray;
    public List<Unit> UnitList;
    public float cameraSpeed = 0.1F;
    public Camera GameCamera;
    bool movingCamera = false;


    void Awake()
    {
        GenerateNewTable();
        Unit.CreateUnit(UnitPrefabArray[0], 1, 1, UnitList);
    }

    Vector2 touchDeltaPosition;
    Vector2 newPosition;
    Vector2 lastPosition;
    void Update()
    {
#if UNITY_STANDALONE_WIN
       
 
        if (Input.GetMouseButtonDown(0))
        {
            callMenu();
        }
        if (Input.GetMouseButtonDown(1))
        {
            newPosition = Input.mousePosition;
            lastPosition = newPosition;
        }
        if (Input.GetMouseButton(1))
        {
            newPosition = Input.mousePosition;
            touchDeltaPosition = newPosition - lastPosition; 
            GameCamera.transform.Translate(touchDeltaPosition.x * -cameraSpeed, touchDeltaPosition.y * -cameraSpeed, 0);
            touchDeltaPosition = Input.mousePosition;
            lastPosition = newPosition;
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                movingCamera = true;
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                GameCamera.transform.Translate(touchDeltaPosition.x * -cameraSpeed, touchDeltaPosition.y * -cameraSpeed, 0);

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (movingCamera)
                {
                    movingCamera = false;
                }
                else
                {
                    //ScreenRay(0);
                    callMenu();
                }
            }
        }
#endif
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
            UnitList[0].DestinationCell = hitInfo.transform.gameObject.GetComponent<Cell>();
            UnitList[0].SetArrayRoute();
            UnitList[0].GetDerections(UnitList[0].DestinationCell);
            UnitList[0].StartTransform();

        }
    }

    //public List<Unit> returnList()
    //{
    //    return UnitList;
    //}

    public void callMenu()
    {
        Debug.Log("Вызов меню");
    }
}
