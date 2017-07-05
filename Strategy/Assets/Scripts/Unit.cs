﻿using System.Collections;
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

    //Вызывается после каждого пересчета, генерирует матрицу перемещений для юнита из данной точки, в конечную
    public void SetArrayRoute()
    {
        //Генерирут пустую матрицу
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
        while (Route[DestinationCell.indexX][DestinationCell.indexY].NSteps == 9999)
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
        Debug.Log(wave);      
    }
    bool CheckIndex(int X, int Y)
    {
        if (X >= 0 && Y >= 0 && Route.Length > X && Route[X].Length > Y)
            return true;
        else
            return false;
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



    //Переносит юнит в ячейку с координатами X, Y
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
        if (Hitpoints <= 0)
            Destroy();
    }
    void Destroy()
    {

    }
}
