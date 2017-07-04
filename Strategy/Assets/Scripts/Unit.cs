using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    float Hitpoints, Damage;
    public Cell CurrentCell, DestinationCell;
    struct WayCell
    {
        public int NSteps;
        public Cell PreviousCell, NextCell;
        public string ToString()
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
    }

    //Вызывается после каждого пересчета
    public void SetArrayRoute()
    {
        Route = new WayCell[_Map.NumberOfCellsOnAxisX][];
        for (int i = 0; i < _Map.NumberOfCellsOnAxisX; i++)
        {
            Route[i] = new WayCell[_Map.NumberOfCellsOnAxisY];
            for (int j = 0; j < _Map.NumberOfCellsOnAxisY; j++)
                Route[i][j].NSteps = 100000;
        }
            
        Route[CurrentCell.indexX][CurrentCell.indexY].NSteps = 0;
        Route[CurrentCell.indexX][CurrentCell.indexY].PreviousCell = CurrentCell;

        
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 1, CurrentCell.indexY + 0));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 0, CurrentCell.indexY + 1));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX - 1, CurrentCell.indexY + 0));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 0, CurrentCell.indexY - 1));
        if (CurrentCell.indexX % 2 == 0)
        {
            A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 1, CurrentCell.indexY + 1));
            A(CurrentCell, _Map.GetCell(CurrentCell.indexX - 1, CurrentCell.indexY + 1));
        }
        else
        {
            A(CurrentCell, _Map.GetCell(CurrentCell.indexX - 1, CurrentCell.indexY - 1));
            A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 1, CurrentCell.indexY - 1));
        }

            
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
        Route[_WayCell.PreviousCell.indexX][_WayCell.PreviousCell.indexY].NextCell = _cell;
        GetDerections(_WayCell.PreviousCell);
    }
    private IEnumerator TransformToNextCell()
    {
        while (CurrentCell != DestinationCell)
        {
            Cell Next = Route[CurrentCell.indexX][CurrentCell.indexY].NextCell;
            SetCell(Next.indexX, Next.indexY);
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }

    void A(Cell Previous, Cell Now)
    {
        if (null == Now)
            return;
        //Проверка на правильность индекса
        if (Route[Now.indexX][Now.indexY].NSteps < Route[Previous.indexX][Previous.indexY].NSteps + 1)
            return;
        Route[Now.indexX][Now.indexY].NSteps = Route[Previous.indexX][Previous.indexY].NSteps + 1;
        Route[Now.indexX][Now.indexY].PreviousCell = Previous;

        A(Now, _Map.GetCell(Now.indexX + 1, Now.indexY + 0));
        A(Now, _Map.GetCell(Now.indexX + 0, Now.indexY + 1));
        A(Now, _Map.GetCell(Now.indexX - 1, Now.indexY + 0));
        A(Now, _Map.GetCell(Now.indexX + 0, Now.indexY - 1));

        if (Now.indexX % 2 == 0)
        {
            A(Now, _Map.GetCell(Now.indexX + 1, Now.indexY + 1));
            A(Now, _Map.GetCell(Now.indexX - 1, Now.indexY + 1));
        }
        else
        {
            A(Now, _Map.GetCell(Now.indexX - 1, Now.indexY - 1));
            A(Now, _Map.GetCell(Now.indexX + 1, Now.indexY - 1));
        }
    }
    public void SetCell(int X, int Y)
    {
        if (!_Map)
            _Map = GameObject.Find("Map").GetComponent<Map>();
        CurrentCell = _Map.GetCell(X, Y);
        transform.position = CurrentCell.transform.position;
    }
    void ToDamage(float _Damage)
    {
        Hitpoints -= _Damage;
      //  if (Hitpoints <= 0)
      //      Destroy();
    }
    //void Destroy()
    //{
    //
//    }

    public static void CreateUnit(GameObject UnitType, int _x, int _y, List<Unit> UnitList)
    {
        Unit NewUnit = Instantiate(UnitType).GetComponent<Unit>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = "TestUnit" + _x + "_" + _y;
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

}
