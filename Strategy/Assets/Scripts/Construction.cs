using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Unit
{
    private static Map map;
    public static Unit unit;
    public enum ConstructionType
    {
        TownHall = 0, Barracks = 1, Pit = 2, Sawmill = 3
    };
    public ConstructionType _ConstructionType;
    string tag;

    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        tag = _ConstructionType.ToString();
        switch (tag)
        {
            case "TownHall":
                MaxHitpoints = 200;
                Hitpoints = MaxHitpoints;
                BuildingRadius = 5;
                Armor = 10;
                break;
            case "Barracks":
                MaxHitpoints = 50;
                Hitpoints = MaxHitpoints;
                BuildingRadius = 3;
                Armor = 8;
                break;
            case "Pit":
                MaxHitpoints = 30;
                Hitpoints = MaxHitpoints;
                Armor = 5;
                break;
            case "Sawmill":
                MaxHitpoints = 30;
                Hitpoints = MaxHitpoints;
                Armor = 5;
                break;
        }
    }


    public static bool CreateConstruction(GameObject UnitType, ConstructionType type, int _x, int _y, List<Unit> UnitList, int Fraction)
    {
        if (!map)
            map = GameObject.Find("Map").GetComponent<Map>();
        if (!CheckPossibilityOfBuildingConstruction(type, map.GetCell(_x, _y).Type))
            return false;
        Construction NewUnit = Instantiate(UnitType).GetComponent<Construction>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = "Construction" + _x + "_" + _y;
        NewUnit._ConstructionType = type;
        NewUnit.Fraction = Fraction;
        return true;
    }



    public static bool CreateConstruction(GameObject UnitType, ConstructionType type, int _x, int _y, List<Unit> UnitList, string UnitName, int Fraction)
    {
        if (!map)
            map = GameObject.Find("Map").GetComponent<Map>();
        if (!CheckPossibilityOfBuildingConstruction(type, map.GetCell(_x, _y).Type))
            return false;
        Construction NewUnit = Instantiate(UnitType).GetComponent<Construction>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = UnitName;
        NewUnit._ConstructionType = type;
        NewUnit.Fraction = Fraction;
        return true;
    }


    private static int[][] CellInfoArray;
    public static bool CreateConstructionOnClick(GameObject UnitType, ConstructionType type, List<Unit> UnitList, Cell CurrentCell)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool f = false;
#if UNITY_STANDALONE_WIN
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
#endif

#if UNITY_ANDROID
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo);
#endif
        if (hitInfo.collider)//проверка на попадание  по колайдеру
        {
            Cell newUnitCell = hitInfo.transform.gameObject.GetComponent(typeof(Cell)) as Cell;
            CellInfoArray = map.GetMatrixOfFreeCells(CurrentCell.indexX, CurrentCell.indexY, 5);

            if (CellInfoArray[newUnitCell.indexX][newUnitCell.indexY] != 9999  && CellInfoArray[newUnitCell.indexX][newUnitCell.indexY] != 0  
                && !newUnitCell.LocatedHereUnit && CheckPossibilityOfBuildingConstruction(type, newUnitCell.Type))
            {
                CreateConstruction(UnitType, type, newUnitCell.indexX, newUnitCell.indexY, UnitList, GameObject.Find("Map").GetComponent<Map>().ActiveUnit.Fraction);
                CurrentCell.LocatedHereUnit.CurrentNumberActionPoints = 0;
                newUnitCell.LocatedHereUnit.CurrentNumberActionPoints = 0;
                Buttons.ResourcesDecrease();
                f = true;
            }
            else
            {
                Debug.Log("Impossible");
                Buttons.UnsubscribeAllDecreases();
                f = false;
            }
        }
        map.ActiveUnit.DeleteFieldOpportunities();
        ActionButtons.actionButtons.HideCancelActionButton();
        return f;
    }

}
