using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Unit
{
    private static Map map;
    private static Unit unit;
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
                //StandardNumberActionPoints = 5;
                //Damage = 5;
                MaxHitpoints = 200;
                Hitpoints = MaxHitpoints;
                BuildingRadius = 5;
                Armor = 10;
                break;
            case "Barracks":
                //StandardNumberActionPoints = 3;
                //Damage = 8;
                MaxHitpoints = 50;
                Hitpoints = MaxHitpoints;
                BuildingRadius = 3;
                Armor = 8;
                break;
            case "Pit":
                //StandardNumberActionPoints = 3;
                //Damage = 0;
                MaxHitpoints = 30;
                Hitpoints = MaxHitpoints;
                Armor = 5;
                break;
            case "Sawmill":
                //StandardNumberActionPoints = 8;
                //Damage = 10;
                MaxHitpoints = 30;
                Hitpoints = MaxHitpoints;
                Armor = 5;
                break;
        }
    }
    void Update()
    {

    }

    public static void CreateConstruction(GameObject UnitType, ConstructionType type, int _x, int _y, List<Unit> UnitList, int Fraction)
    {
        Construction NewUnit = Instantiate(UnitType).GetComponent<Construction>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = "Construction" + _x + "_" + _y;
        NewUnit._ConstructionType = type;
        NewUnit.Fraction = Fraction;
    }



    public static void CreateConstruction(GameObject UnitType, ConstructionType type, int _x, int _y, List<Unit> UnitList, string UnitName, int Fraction)
    {
        Construction NewUnit = Instantiate(UnitType).GetComponent<Construction>();
        NewUnit.SetCell(_x, _y);
        UnitList.Add(NewUnit);
        NewUnit.name = UnitName;
        NewUnit._ConstructionType = type;
        NewUnit.Fraction = Fraction;
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

            if (CellInfoArray[newUnitCell.indexX][newUnitCell.indexY] != 9999  && CellInfoArray[newUnitCell.indexX][newUnitCell.indexY] != 0  && !newUnitCell.LocatedHereUnit)
            {
                CreateConstruction(UnitType, type, newUnitCell.indexX, newUnitCell.indexY, UnitList, GameObject.Find("Map").GetComponent<Map>().ActiveUnit.Fraction);
                CurrentCell.LocatedHereUnit.CurrentNumberActionPoints = 0;
                newUnitCell.LocatedHereUnit.CurrentNumberActionPoints = 0;
            }
            else
            {
                Debug.Log("Impossible");
            }
            map.ActiveUnit.DeleteFieldOpportunities();
            ActionButtons.actionButtons.HideCancelActionButton();
        }
    }

}
