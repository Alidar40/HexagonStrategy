using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArtificialIntelligence : MonoBehaviour
{
    public static BattleArtificialIntelligence battleArtificialIntelligence;

    Cell cell;
    List<Unit> UnitList;
    Map m;
    Unit u;

    public bool Attack = false;
    public bool Deff = false;
    public int xTown;
    public int yTown;
    int xBarracks;
    int yBarracks;

    void Start()
    {
        m = GameObject.Find("Map").GetComponent<Map>();
        UnitList = m.UnitList;
        battleArtificialIntelligence = GetComponent<BattleArtificialIntelligence>();

    }


    void Update()
    {
       
    }

    public void BasicAlgorithm()
    {
        if (Attack)
            GoToEnemy();
    }

    public Unit FindEnemy()
    {

        for (int k = 1; k < m.NumberOfCellsOnAxisX; k++)
        {
            for (int i = xTown - k + 1; i <= xTown + k; i++)
            {
                if (m.GetCell(i, yTown + k))
                {
                    if (m.GetCell(i, yTown + k).LocatedHereUnit)
                        if (m.GetCell(i, yTown + k).LocatedHereUnit.Fraction == 1)
                        {
                            return (m.GetCell(i, yTown + k).LocatedHereUnit);
                        }
                }
            }
            for (int j = yTown + k; j >= yTown - k; j--)
            {
                if (m.GetCell(xTown + k, j))
                {

                    if (m.GetCell(xTown + k, j).LocatedHereUnit)
                        if (m.GetCell(xTown + k, j).LocatedHereUnit.Fraction == 1)
                        {
                            return (m.GetCell(xTown + k, j).LocatedHereUnit);
                        }
                }
            }
            for (int i = xTown + k - 1; i >= xTown - k; i--)
            {
                if (m.GetCell(i, yTown - k))
                {

                    if (m.GetCell(i, yTown - k).LocatedHereUnit)
                        if (m.GetCell(i, yTown - k).LocatedHereUnit.Fraction == 1)
                        {
                            return (m.GetCell(i, yTown - k).LocatedHereUnit);
                        }
                }
            }
            for (int j = yTown - k + 1; j <= yTown + k; j++)
            {
                if (m.GetCell(xTown - k, j))
                {

                    if (m.GetCell(xTown - k, j).LocatedHereUnit)
                        if (m.GetCell(xTown - k, j).LocatedHereUnit.Fraction == 1)
                        {
                            return (m.GetCell(xTown - k, j).LocatedHereUnit);
                        }
                }
            }
        }

        return (m.GetCell(xTown, yTown).LocatedHereUnit);

    }

    public void GoToEnemy()
    {
        foreach (Unit u in UnitList)
        {
            if (((u.tag == "Swordsman") || (u.tag == "Archer") || (u.tag == "Mage") || (u.tag == "Killer")) && (u.Fraction == 2))
            {
                u.DestinationCell = FindEnemy().CurrentCell;
                if (m.DistanceToCell(u.CurrentCell, u.DestinationCell, u.AttackRadius) <= u.AttackRadius)
                {
                    u.AttackAnotherUnit(u.DestinationCell.LocatedHereUnit);
                }
                else
                {
                    u.SetArrayRoute();
                    u.GetDerections(u.DestinationCell);
                    u.StartTransform();
                }

            }
        }
    }

}