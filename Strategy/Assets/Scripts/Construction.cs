using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Unit
{

    Cell _currentCell;

    public enum ConstructionType
    {
        TownHall = 0, Barracks = 1, Pit = 2, Sawmill = 3
    };
    public ConstructionType _ConstructionType;

    void Start()
    {
        _currentCell = this.CurrentCell;

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


            if (Mathf.Pow(newUnitCell.indexX - CurrentCell.indexX, 2f) + Mathf.Pow(newUnitCell.indexY - CurrentCell.indexY, 2f) <= 25f)
            {
                if (!newUnitCell.LocatedHereUnit)//проверка на то, что в том месте отсутсвует юнит
                {
                    CreateConstruction(UnitType, type, newUnitCell.indexX, newUnitCell.indexY, UnitList);
                }
                else
                {
                    Debug.Log("Клетка занята");
                }
            }
            else
            {
                Debug.Log("Вне радиуса");
            }
        }
    }
}
