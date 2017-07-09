using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public float Hitpoints, Damage;
    public int AttackRadius, BuildingRadius;
    public int CurrentNumberActionPoints, StandardNumberActionPoints;
    public Cell CurrentCell, DestinationCell;
    public enum UnitType
    {
        Swordsman = 1, Archer = 2, Mage = 3, Killer = 4,
        Construction = 99
    };
    public UnitType Type;

    struct WayCell
    {
        public int NSteps;
        public Cell PreviousCell, NextCell;
        public string ToString()   //стоит переименовать
        {
            return NSteps + "";
        }
    }
    private WayCell[][] Route;
    Map _Map;

    void Awake() {
        _Map = GameObject.Find("Map").GetComponent<Map>();
    }
    void Start()
    {
        tag = Type.ToString();
    }
    

    public void SetArrayRoute()
    {
        //Генерирует пустую матрицу
        Route = new WayCell[_Map.NumberOfCellsOnAxisX][];
        for (int i = 0; i < _Map.NumberOfCellsOnAxisX; i++)
        {
            Route[i] = new WayCell[_Map.NumberOfCellsOnAxisY];
            for (int j = 0; j < _Map.NumberOfCellsOnAxisY; j++)
                Route[i][j].NSteps = 9999;
        }
        Route[CurrentCell.indexX][CurrentCell.indexY].NSteps = 0;
        Route[CurrentCell.indexX][CurrentCell.indexY].PreviousCell = CurrentCell;
        
        int wave = 0;
        while (Route[DestinationCell.indexX][DestinationCell.indexY].NSteps == 9999 && CheckWavePropagation(wave))
        {
            for (int i = 0; i < Route.Length; i++)
                for (int j = 0; j < Route[i].Length; j++)
                    if (Route[i][j].NSteps == wave)
                    {
                        if (CheckIndex(i + 1, j + 0) && Route[i + 1][j + 0].NSteps > wave + 1)
                        {
                            Route[i + 1][j + 0].NSteps = wave + 1;
                            Route[i + 1][j + 0].PreviousCell = _Map.GetCell(i, j);
                        }

                        if (CheckIndex(i - 1, j + 0) && Route[i - 1][j + 0].NSteps > wave + 1)
                        {
                            Route[i - 1][j + 0].NSteps = wave + 1;
                            Route[i - 1][j + 0].PreviousCell = _Map.GetCell(i, j);
                        }

                        if (CheckIndex(i + 0, j + 1) && Route[i + 0][j + 1].NSteps > wave + 1)
                        {
                            Route[i + 0][j + 1].NSteps = wave + 1;
                            Route[i + 0][j + 1].PreviousCell = _Map.GetCell(i, j);
                        }

                        if (CheckIndex(i + 0, j - 1) && Route[i + 0][j - 1].NSteps > wave + 1)
                        {
                            Route[i + 0][j - 1].NSteps = wave + 1;
                            Route[i + 0][j - 1].PreviousCell = _Map.GetCell(i, j);
                        }

                        if (i % 2 == 0)
                        {
                            if (CheckIndex(i + 1, j + 1) && Route[i + 1][j + 1].NSteps > wave + 1)
                            {
                                Route[i + 1][j + 1].NSteps = wave + 1;
                                Route[i + 1][j + 1].PreviousCell = _Map.GetCell(i, j);
                            }
                            if (CheckIndex(i - 1, j + 1) && Route[i - 1][j + 1].NSteps > wave + 1)
                            {
                                Route[i - 1][j + 1].NSteps = wave + 1;
                                Route[i - 1][j + 1].PreviousCell = _Map.GetCell(i, j);
                            }
                        }
                        else
                        {
                            if (CheckIndex(i + 1, j - 1) && Route[i + 1][j - 1].NSteps > wave + 1)
                            {
                                Route[i + 1][j - 1].NSteps = wave + 1;
                                Route[i + 1][j - 1].PreviousCell = _Map.GetCell(i, j);
                            }
                            if (CheckIndex(i - 1, j - 1) && Route[i - 1][j - 1].NSteps > wave + 1)
                            {
                                Route[i - 1][j - 1].NSteps = wave + 1;
                                Route[i - 1][j - 1].PreviousCell = _Map.GetCell(i, j);
                            }
                        }
                    }
            wave++;
        }
    }
    bool CheckIndex(int X, int Y)
    {
        if (X >= 0 && Y >= 0 && Route.Length > X && Route[X].Length > Y && !_Map.GetCell(X, Y).LocatedHereUnit)
            return true;
        else
            return false;
    }
    bool CheckWavePropagation(int wave)
    {
        bool flag = false;
        for (int i = 0; i < Route.Length; i++)
            for (int j = 0; j < Route[i].Length; j++)
                if (Route[i][j].NSteps >= wave && Route[i][j].NSteps != 9999)
                    flag = true;
        return flag;
    }

    public void StartTransform()
    {
        StartCoroutine(TransformToNextCell());
    }
    public void GetDerections(Cell _cell)
    {
        if (_cell == CurrentCell)
            return;
        WayCell _WayCell = Route[_cell.indexX][_cell.indexY];
        if (!_WayCell.PreviousCell)
        {
            Debug.LogError("Error!!! Невозможно построить маршрут к ячейке " + _cell + " !");
            return;
        }
        Route[_WayCell.PreviousCell.indexX][_WayCell.PreviousCell.indexY].NextCell = _cell;
        GetDerections(_WayCell.PreviousCell);
    }
    private IEnumerator TransformToNextCell()
    {
        while (CurrentCell != DestinationCell && Route[CurrentCell.indexX][CurrentCell.indexY].NextCell && CurrentNumberActionPoints > 0)
        {
            Cell Next = Route[CurrentCell.indexX][CurrentCell.indexY].NextCell;
            SetCell(Next.indexX, Next.indexY);
            CurrentNumberActionPoints--;
            yield return new WaitForSeconds(0.25f);
        }
        yield break;
    }



    //Переносит юнит в ячейку с координатами X, Y
    public void SetCell(int X, int Y)
    {
        if (!_Map)
            _Map = GameObject.Find("Map").GetComponent<Map>();
        if (CurrentCell)
            CurrentCell.LocatedHereUnit = null;
        CurrentCell = _Map.GetCell(X, Y);
        transform.position = CurrentCell.transform.position;
        CurrentCell.LocatedHereUnit = this;
    }
    public void ToDamage(float _Damage)
    {
        Hitpoints -= _Damage;
      //  if (Hitpoints <= 0)
      //      Destroy();
    }
    public void AttackAnotherUnit(Unit AttackedUnit)
    {
        AttackedUnit.ToDamage(Damage);
        CurrentNumberActionPoints = 0;
    }





    public static void CreateUnit(GameObject UnitPrefab, UnitType type, int _x, int _y, List<Unit> UnitList)
    {
        Unit NewUnit = Instantiate(UnitPrefab).GetComponent<Unit>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = "TestUnit" + _x + "_" + _y;
        NewUnit.Type = type;
    }
    public static void CreateUnit(GameObject UnitPrefab, UnitType type, int _x, int _y, List<Unit> UnitList, string UnitName)
    {
        Unit NewUnit = Instantiate(UnitPrefab).GetComponent<Unit>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = UnitName;
        NewUnit.Type = type;
    }
    public static void CreateUnitOnClick(GameObject UnitPrefab, UnitType type, List<Unit> UnitList)
    {
        RaycastHit2D hitInfo = new RaycastHit2D();

#if UNITY_STANDALONE_WIN
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
#endif

#if UNITY_ANDROID
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
#endif
        if (hitInfo.collider)//проверка на попадание  по колайдеру
        {
            Cell currentCell = hitInfo.transform.gameObject.GetComponent(typeof(Cell)) as Cell;

            if (!currentCell.LocatedHereUnit)//проверка на то, что в том месте есть юнит
            {
                CreateUnit(UnitPrefab, type, currentCell.indexX, currentCell.indexY, UnitList);
            }
        }
    }
    public static void DeleteUnit(List<Unit> UnitList, Unit UnitToDelete)
    {
        string UnitToDeleteName = UnitToDelete.name;
        Destroy(GameObject.Find(UnitToDeleteName));
        UnitList.Remove(UnitToDelete);
    }

    public delegate void MethodContainer(List<Unit> UnitList);

    public event MethodContainer LaunchNextTurn;

    public void NextTurn(List<Unit> UnitList)
    {
        LaunchNextTurn(UnitList);
    }
    public void UpdateActionPoints(List<Unit> UnitList)

    {
        CurrentNumberActionPoints = StandardNumberActionPoints;
        LaunchNextTurn -= UpdateActionPoints;

    }


    
    private static GameObject[] ArrayFieldOpportunities;
    public void GenerateFieldOpportunities(GameObject FieldOpportunities, int R)
    {
        int[][] MatrixOfFreeCells = _Map.GetMatrixOfFreeCells(CurrentCell.indexX, CurrentCell.indexY, R);
        int N = 0;
        for (int i = 0; i < MatrixOfFreeCells.Length; i++)
            for (int j = 0; j < MatrixOfFreeCells[i].Length; j++)
                if (MatrixOfFreeCells[i][j] < 999)
                    N++;
        ArrayFieldOpportunities = new GameObject[N];
        for (int i = 0; i < MatrixOfFreeCells.Length; i++)
            for (int j = 0; j < MatrixOfFreeCells[i].Length; j++)
                if (MatrixOfFreeCells[i][j] < 999)
                {
                    N--;
                    ArrayFieldOpportunities[N] = Instantiate(FieldOpportunities);
                    ArrayFieldOpportunities[N].transform.SetParent(transform);
                    ArrayFieldOpportunities[N].transform.position = _Map.GetCell(i, j).transform.position;
                }
    }
    public void GenerateFieldOpportunitiesForMoving(GameObject FieldOpportunities, int R)
    {
        int[][] MatrixOfFreeCells = _Map.GetMatrixOfFreeCellsForMoving(CurrentCell.indexX, CurrentCell.indexY, R);
        int N = 0;
        for (int i = 0; i < MatrixOfFreeCells.Length; i++)
            for (int j = 0; j < MatrixOfFreeCells[i].Length; j++)
                if (MatrixOfFreeCells[i][j] < 999)
                    N++;
        ArrayFieldOpportunities = new GameObject[N];
        for (int i = 0; i < MatrixOfFreeCells.Length; i++)
            for (int j = 0; j < MatrixOfFreeCells[i].Length; j++)
                if (MatrixOfFreeCells[i][j] < 999)
                {
                    N--;
                    ArrayFieldOpportunities[N] = Instantiate(FieldOpportunities);
                    ArrayFieldOpportunities[N].transform.SetParent(transform);
                    ArrayFieldOpportunities[N].transform.position = _Map.GetCell(i, j).transform.position;
                }
    }
    public void DeleteFieldOpportunities()
    {
        for (int i = 0; i < ArrayFieldOpportunities.Length; i++)
            Destroy(ArrayFieldOpportunities[i]);
        ArrayFieldOpportunities = new GameObject[0];
    }
}
