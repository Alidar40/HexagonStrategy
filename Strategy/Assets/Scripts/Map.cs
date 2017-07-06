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
    //public float cameraSpeed = 0.1F;
    //public Camera GameCamera;
    //bool movingCamera = false;
    GameCamera cam;

    public Unit ActiveUnit;

    void Awake()
    {
        GenerateNewTable();
        Unit.CreateUnit(UnitPrefabArray[0], 1, 1, UnitList);
        cam = GameObject.Find("Main Camera").GetComponent<GameCamera>();    
    }

    //Vector2 touchDeltaPosition;
   // Vector2 newPosition;
    //Vector2 lastPosition;
    void Update()
    {
        cam.PointClick();
        cam.cameraMoving();

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
        RaycastHit2D hitInfo = new RaycastHit2D();

#if UNITY_STANDALONE_WIN
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
#endif

#if UNITY_ANDROID
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(_i).position), Vector2.zero);
#endif
        if (hitInfo.collider)//проверка на попадание  по колайдеру
        {
            Cell currentCell = hitInfo.transform.gameObject.GetComponent(typeof(Cell)) as Cell;

            if (currentCell.LocatedHereUnit)//проверка на то, что в том месте есть юнит
            {
                switch (currentCell.LocatedHereUnit.tag)//выбираем по тегу, какое меню вывести на экран
                {
                    case "Swordsman":
                        {
                        ActionButtons.actionButtons.ActivateSwordsmanButtonsPanel();
                        }
                        break;
                    case "Archer":
                        ActionButtons.actionButtons.ActivateArcherButtonsPanel();
                        break;
                    case "Mage":
                        ActionButtons.actionButtons.ActivateMageButtonsPanel();
                        break;
                    case "Killer":
                        ActionButtons.actionButtons.ActivateKillerButtonsPanel();
                        break;
                    case "TownHall":
                        ActionButtons.actionButtons.ActivateTownHallButtonsPanel();
                        break;
                    case "Barracks":
                        ActionButtons.actionButtons.ActivateBarracksButtonsPanel();
                        break;
                    case "Pit":
                        ActionButtons.actionButtons.ActivatePitButtonsPanel();
                        break;
                    case "Sawmill":
                        ActionButtons.actionButtons.ActivateSawmillButtonsPanel();
                        break;
                    default:        //по дефолту убирает меню
                        ActionButtons.actionButtons.HideAll();
                        break;
                }

            }
        }
    }
}
