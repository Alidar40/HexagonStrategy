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
    }
    private WayCell[][] Route;
    Map _Map;

    void Awake() {
        _Map = GameObject.Find("Map").GetComponent<Map>();
    }
    void Start()
    {
        GetDerections();
    }

    //Вызывается после каждого пересчета
    void GetDerections()
    {
        Route = new WayCell[_Map.NumberOfCellsOnAxisX][];
        for (int i = 0; i < _Map.NumberOfCellsOnAxisX; i++)
            Route[i] = new WayCell[_Map.NumberOfCellsOnAxisY];
        Route[CurrentCell.indexX][CurrentCell.indexY].NSteps = 0;
        Route[CurrentCell.indexX][CurrentCell.indexY].PreviousCell = CurrentCell;

        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 1, CurrentCell.indexY - 1));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 1, CurrentCell.indexY + 0));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 1, CurrentCell.indexY + 1));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 0, CurrentCell.indexY + 1));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX - 1, CurrentCell.indexY + 1));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX - 1, CurrentCell.indexY + 0));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX - 1, CurrentCell.indexY - 1));
        A(CurrentCell, _Map.GetCell(CurrentCell.indexX + 0, CurrentCell.indexY - 1));
    }

    
    void A(Cell Previous, Cell Now)
    {
        //Проверка на правильность индекса
        if (Route[Now.indexX][Now.indexY].NSteps < Route[Previous.indexX][Previous.indexY].NSteps + 1)
            return;
        Route[Now.indexX][Now.indexY].NSteps = Route[Previous.indexX][Previous.indexY].NSteps + 1;
        Route[Now.indexX][Now.indexY].PreviousCell = Previous;

        A(Now, _Map.GetCell(Now.indexX + 1, Now.indexY - 1));
        A(Now, _Map.GetCell(Now.indexX + 1, Now.indexY + 0));
        A(Now, _Map.GetCell(Now.indexX + 1, Now.indexY + 1));
        A(Now, _Map.GetCell(Now.indexX + 0, Now.indexY + 1));
        A(Now, _Map.GetCell(Now.indexX - 1, Now.indexY + 1));
        A(Now, _Map.GetCell(Now.indexX - 1, Now.indexY + 0));
        A(Now, _Map.GetCell(Now.indexX - 1, Now.indexY - 1));
        A(Now, _Map.GetCell(Now.indexX + 0, Now.indexY - 1));
    }
    public void SetCell(int X, int Y)
    {
        CurrentCell = _Map.GetCell(X, Y);
        transform.position = CurrentCell.transform.position;
    }
    void ToDamage(float _Damage)
    {
        Hitpoints -= _Damage;
        if (Hitpoints <= 0)
            Destroy();
    }
    void Destroy()
    {

    }
}
