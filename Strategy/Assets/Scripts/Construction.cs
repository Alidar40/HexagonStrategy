using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Unit
{

    // Cell _currentCell;
    //private static int _CurrentNumberActionPoints, _StandardNumberActionPoints;
    private static Map map;
    private static Unit unit;
    public enum ConstructionType
    {
        TownHall = 0, Barracks = 1, Pit = 2, Sawmill = 3
    };
    public ConstructionType _ConstructionType;

    void Start()
    {
        //_currentCell = this.CurrentCell;
        map = GameObject.Find("Map").GetComponent<Map>();
        
    }
    void Update()
    {

    }

    public static void CreateConstruction(GameObject UnitType, ConstructionType type, int _x, int _y, List<Unit> UnitList)
    {
        Construction NewUnit = Instantiate(UnitType).GetComponent<Construction>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = "Construction" + _x + "_" + _y;
        NewUnit._ConstructionType = type;
    }



    public static void CreateConstruction(GameObject UnitType, ConstructionType type, int _x, int _y, List<Unit> UnitList, string UnitName)
    {
        Construction NewUnit = Instantiate(UnitType).GetComponent<Construction>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = UnitName;
        NewUnit._ConstructionType = type;
    }


    private static int[][] CellInfoArray;
    public static void CreateConstructionOnClick(GameObject UnitType, ConstructionType type, List<Unit> UnitList, Cell CurrentCell)
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
            Cell newUnitCell = hitInfo.transform.gameObject.GetComponent(typeof(Cell)) as Cell;
            CellInfoArray = map.GetMatrixOfFreeCells(CurrentCell.indexX, CurrentCell.indexY, 5);
            if (CellInfoArray[newUnitCell.indexX][newUnitCell.indexY] != 9999)
            {
                CreateConstruction(UnitType, type, newUnitCell.indexX, newUnitCell.indexY, UnitList);
            }
            else
            {
                Debug.Log("Impossible");
            }
            map.ActiveUnit.DeleteFieldOpportunities();
        }

    }
}
