using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Map : MonoBehaviour {
    [Header("TableOptions")]
    public int NumberOfCellsOnAxisX = 10;
    public int NumberOfCellsOnAxisY = 10;
    public GameObject CellPrefab;
    public GameObject[] UnitPrefabArray;
    public List<Unit> UnitList;

    GameCamera cam;
   // public const int GOLD = 10;//это константы  колличества ресурсов за ход
    //public const int WOOD = 10;//стоило б переименовать по понятнее..
    //public const int STONE = 10;

    public Unit ActiveUnit;
    public int Gold=0, Stone=0, Wood=0;




    public bool ActivePlayer;//активен ли игрок
    public int PlayerFraction;//фракция игрока


    void Awake()
    {
        GenerateNewTable();
        //Construction.CreateConstruction(UnitPrefabArray[4], Construction.ConstructionType.TownHall, 5, 5, UnitList, "TownHall1", 1);//последняя единица для теста
        //Construction.CreateConstruction(UnitPrefabArray[4], Construction.ConstructionType.TownHall, 15, 5, UnitList, "TownHall2s", 2);
        cam = GameObject.Find("Main Camera").GetComponent<GameCamera>();
    }

    void Start()
    {
        PlayerFraction = 1;//тут назначим ему фракцию
        ActivePlayer = true;//и пусть он делает первый ход
        cam.COSevent += callMenu;  //Теперь при использовании функции PointClick()
        //мы можем комбинировать различные функции внутрии ее
        //в самом начале мы подписываем в PointClick() функцию callMenu
        Gold += 50;
        Wood += 50;
        Stone += 50;
    }

    void Update()
    {
        cam.PointClick();
        cam.cameraMoving();
        cam.CameraZoom();
        Resources.ShowResources();
        
    }

    private Cell[][] CellArray;
    public void GenerateNewTable()
    {
        CellArray = new Cell[NumberOfCellsOnAxisX][];
        for (int i = 0; i < NumberOfCellsOnAxisX; i++)
        {
            CellArray[i] = new Cell[NumberOfCellsOnAxisY];
            GameObject Line = GameObject.Find("Line_" + i);
            if (!Line)
                Line = new GameObject();
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
                //CellArray[i][j].SetType(1);
            }
        }
    }
    public void DestroyTable()
    {
        for (int i =0; i < CellArray.Length; i++)
        {
            for (int j = 0; j < CellArray[i].Length; j++)
            {
                Destroy(CellArray[i][j].gameObject);
            }
        }
    }
    public Cell GetCell(int X, int Y)
    {
        if (X < 0 || Y < 0 || X >= NumberOfCellsOnAxisX || Y >= NumberOfCellsOnAxisY)
            return null;
        return CellArray[X][Y];
    }

    public void MovingUnit(int _i)
    {
        RaycastHit hitInfo = new RaycastHit();

#if UNITY_STANDALONE_WIN
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
#endif

#if UNITY_ANDROID
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo);
#endif
        
        if (hitInfo.collider && hitInfo.transform.gameObject.GetComponent<Cell>())
        {
            ActiveUnit.DestinationCell = hitInfo.transform.gameObject.GetComponent<Cell>();
            ActiveUnit.SetArrayRoute();
            ActiveUnit.GetDerections(ActiveUnit.DestinationCell);
            ActiveUnit.StartTransform();
            ActionButtons.actionButtons.HideCancelActionButton();
        }
    }
    public void AttackUnit(int _i)
    {
        RaycastHit hitInfo = new RaycastHit();

#if UNITY_STANDALONE_WIN
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo); 
#endif

#if UNITY_ANDROID
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo);
#endif

        if (hitInfo.collider)
        {
            Cell currentCell = hitInfo.transform.gameObject.GetComponent(typeof(Cell)) as Cell;

            //проверка на то, что в том месте есть юнит
            if (currentCell && ActiveUnit != currentCell.LocatedHereUnit && currentCell.LocatedHereUnit && DistanceToCell(ActiveUnit.CurrentCell, currentCell, ActiveUnit.AttackRadius) <= ActiveUnit.AttackRadius)
            {
                if (ActiveUnit.Fraction != currentCell.LocatedHereUnit.Fraction)//проверка на совпадение цели и активного юнита
                {
                    ActiveUnit.AttackAnotherUnit(currentCell.LocatedHereUnit);
                    ActionButtons.actionButtons.HideCancelActionButton();
                }

            }
            //else
            //{
            //    cam.StartAttackUnit();
            //}
        }
    }

    //Где R - максимальный радиус поиска
    public float DistanceToCell(Cell C1, Cell C2, int R)
    {
        return GetMatrixOfFreeCells(C1.indexX, C1.indexY, R)[C2.indexX][C2.indexY];
    }

    public void callMenu()
    {
        GameObject.Find("GameUICamera/Canvas/Info").SetActive(false);
        RaycastHit hitInfo = new RaycastHit();

#if UNITY_STANDALONE_WIN
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
#endif

#if UNITY_ANDROID
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo);
#endif
        if (hitInfo.collider)//проверка на попадание  по колайдеру
        {
            Cell currentCell = hitInfo.transform.gameObject.GetComponent(typeof(Cell)) as Cell;

            if (currentCell && currentCell.LocatedHereUnit)//проверка на то, что в том месте есть юнит
            {
                
                ActiveUnit = currentCell.LocatedHereUnit;
                if (PlayerFraction == ActiveUnit.Fraction)//меню будет открываться только если тыкаешь по своей фракции
                {

                        switch (currentCell.LocatedHereUnit.Type.ToString())//выбираем по типу юнита, какое меню вывести на экран
                    {
                        case "Swordsman":
                            ActionButtons.actionButtons.ActivateSwordsmanButtonsPanel();
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
                        case "Construction":
                            switch (((Construction)currentCell.LocatedHereUnit)._ConstructionType.ToString())
                            {

                                case "TownHall":
                                    ActionButtons.actionButtons.ActivateTownHallButtonsPanel();
                                    //Debug.Log("test");
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
                            }
                            break;
                        default:        //по дефолту убирает меню
                            ActionButtons.actionButtons.HideAll();
                            break;
                    }
                }
                else
                {
                    ActionButtons.actionButtons.HideAll();
                    GameObject.Find("GameUICamera/Canvas/Info").SetActive(true);
                }

            }
        }
    }


    public int[][] GetMatrixOfFreeCells(int X, int Y, int R)
    {
        //Генерирует пустую матрицу
        int[][] Route = new int[NumberOfCellsOnAxisX][];
        for (int i = 0; i < NumberOfCellsOnAxisX; i++)
        {
            Route[i] = new int[NumberOfCellsOnAxisY];
            for (int j = 0; j < NumberOfCellsOnAxisY; j++)
                Route[i][j] = 9999;
        }
        Route[X][Y] = 0;

        int wave = 0;
        while (wave < R)
        {
            for (int i = 0; i < Route.Length; i++)
                for (int j = 0; j < Route[i].Length; j++)
                    if (Route[i][j] == wave)
                    {
                        if (GetCell(i + 1, j + 0) && Route[i + 1][j + 0] > wave + 1)
                            Route[i + 1][j + 0] = wave + 1;

                        if (GetCell(i - 1, j + 0) && Route[i - 1][j + 0] > wave + 1)
                            Route[i - 1][j + 0] = wave + 1;

                        if (GetCell(i + 0, j + 1) && Route[i + 0][j + 1] > wave + 1)
                            Route[i + 0][j + 1] = wave + 1;

                        if (GetCell(i + 0, j - 1) && Route[i + 0][j - 1] > wave + 1)
                            Route[i + 0][j - 1] = wave + 1;

                        if (i % 2 == 0)
                        {
                            if (GetCell(i + 1, j + 1) && Route[i + 1][j + 1] > wave + 1)
                                Route[i + 1][j + 1] = wave + 1;
                            if (GetCell(i - 1, j + 1) && Route[i - 1][j + 1] > wave + 1)
                                Route[i - 1][j + 1] = wave + 1;
                        }
                        else
                        {
                            if (GetCell(i + 1, j - 1) && Route[i + 1][j - 1] > wave + 1)
                                Route[i + 1][j - 1] = wave + 1;
                            if (GetCell(i - 1, j - 1) && Route[i - 1][j - 1] > wave + 1)
                                Route[i - 1][j - 1] = wave + 1;
                        }
                    }
            wave++;
        }
        return Route;
    }
    public int[][] GetMatrixOfFreeCellsForMoving(int X, int Y, int R)
    {
        //Генерирует пустую матрицу
        int[][] Route = new int[NumberOfCellsOnAxisX][];
        for (int i = 0; i < NumberOfCellsOnAxisX; i++)
        {
            Route[i] = new int[NumberOfCellsOnAxisY];
            for (int j = 0; j < NumberOfCellsOnAxisY; j++)
                Route[i][j] = 9999;
        }
        Route[X][Y] = 0;

        int wave = 0;
        while (wave < R)
        {
            for (int i = 0; i < Route.Length; i++)
                for (int j = 0; j < Route[i].Length; j++)
                    if (Route[i][j] == wave)
                    {
                        if (CheckIndex(i + 1, j + 0) && Route[i + 1][j + 0] > wave + 1)
                            Route[i + 1][j + 0] = wave + 1;

                        if (CheckIndex(i - 1, j + 0) && Route[i - 1][j + 0] > wave + 1)
                            Route[i - 1][j + 0] = wave + 1;

                        if (CheckIndex(i + 0, j + 1) && Route[i + 0][j + 1] > wave + 1)
                            Route[i + 0][j + 1] = wave + 1;

                        if (CheckIndex(i + 0, j - 1) && Route[i + 0][j - 1] > wave + 1)
                            Route[i + 0][j - 1] = wave + 1;

                        if (i % 2 == 0)
                        {
                            if (CheckIndex(i + 1, j + 1) && Route[i + 1][j + 1] > wave + 1)
                                Route[i + 1][j + 1] = wave + 1;
                            if (CheckIndex(i - 1, j + 1) && Route[i - 1][j + 1] > wave + 1)
                                Route[i - 1][j + 1] = wave + 1;
                        }
                        else
                        {
                            if (CheckIndex(i + 1, j - 1) && Route[i + 1][j - 1] > wave + 1)
                                Route[i + 1][j - 1] = wave + 1;
                            if (CheckIndex(i - 1, j - 1) && Route[i - 1][j - 1] > wave + 1)
                                Route[i - 1][j - 1] = wave + 1;
                        }
                    }
            wave++;
        }
        return Route;
    }
    public bool CheckIndex(int X, int Y)
    {
        if (GetCell(X, Y) && !GetCell(X, Y).LocatedHereUnit)
            return true;
        else
            return false;
    }
}
