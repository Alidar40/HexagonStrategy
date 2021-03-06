﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligence : MonoBehaviour
{

    public static ArtificialIntelligence artificialIntelligence;

    public bool SufficientExtractionResources = false;//есть хотя бы 1 лесопилка и 1 каменоломня//пока не используется

    public bool EfficientExtractionResources = false;//есть эффективное кол-во лесопилок и каменоломнь ( например 3,2)
    public bool PlatoonIsReady = false; // отряд готов ( вообще эти булены стоит инициализировать в старте)
    public bool CanEndGame = false; //этот флаг необходимо менять в случае, если была атакована ратуша противника

    public int Gold = 0, Stone = 0, Wood = 0;


    List<Unit> UnitList;
    Map m;
    Unit u;
    Construction c;
    int TownHallMaxHitpoints = 200;
    int BarracksMaxHitpoints = 50;
    int PitMaxHitpoints = 30;
    int SawmillMaxHitpoints = 30;
    public Unit Town;
    public int xTown = 0;
    public int yTown = 0;
    int xBarracks;
    int yBarracks;



    public void Start()
    {

        m = GameObject.Find("Map").GetComponent<Map>();
        UnitList = m.UnitList;
        Gold = 50; Stone = 50; Wood = 50;

    }
    void Update()
    {
        if (!Town)
            foreach (Unit c in UnitList)
            {
                if ((c.tag == "TownHall") && (c.Fraction == 2))
                {
                   GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().xTown= xTown = c.CurrentCell.indexX;
                   GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().yTown=  yTown = c.CurrentCell.indexY;
                    Town = c;
                }
            }
    }

    public void BasicAlgorithm()
    {
        if (!GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().Attack)
            foreach (Unit c in UnitList)
            {
                if ((c.tag == "TownHall") && (c.Fraction == 2) && (c.Hitpoints < TownHallMaxHitpoints))
                {
                    GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().Attack = true;
                    break;
                }
                if ((c.tag == "Barracks") && (c.Fraction == 2) && (c.Hitpoints < BarracksMaxHitpoints))
                {
                    GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().Attack = true;
                    break;
                }
                if ((c.tag == "Pit") && (c.Fraction == 2) && (c.Hitpoints < PitMaxHitpoints))
                {
                    GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().Attack = true;
                    break;
                }
                if ((c.tag == "Sawmill") && (c.Fraction == 2) && (c.Hitpoints < SawmillMaxHitpoints))
                {
                    GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().Attack = true;
                    break;
                }

            }


        if (FindEfficientBuild("Sawmill") < 2)
        {
            BuildSawmill();
        }

        if (FindEfficientBuild("Pit") < 3)
        {
            BuildPit();
        }

        if (FindEfficientBuild("Barracks") == 0)
        {
            BuildBarracks();
        }
        else if (FindOutHowManySoldiers() < 20)
        {//заказываем юнитов
            if (FindOutHowManyUnit("Swordsman") < 5)
            {
                if (Gold >= 7)
                    OrderUnit("Swordsman", 0, Unit.UnitType.Swordsman, 10);
            }
            else
            if (FindOutHowManyUnit("Archer") < 3)
            {
                if (Gold >= 15)
                    OrderUnit("Archer", 1, Unit.UnitType.Archer, 20);
            }
            else if (FindOutHowManyUnit("Killer") < 1)
            {
                if ((Gold >= 40))
                    OrderUnit("Killer", 3, Unit.UnitType.Killer, 50);
            }
            else
            {
                if ((Gold >= 40))
                    OrderUnit("Killer", 3, Unit.UnitType.Killer, 50);
                else
                if (Gold >= 15)
                    OrderUnit("Archer", 1, Unit.UnitType.Archer, 20);
                else
                if (Gold >= 7)
                    OrderUnit("Swordsman", 0, Unit.UnitType.Swordsman, 10);
            }

        }

        if (FindOutHowManySoldiers() >= 5)
        {
            AttackOn();
        }


        foreach (Unit c in UnitList)
        {
            if ((c.tag == "Sawmill") && (c.Fraction == 2))
            {
                Wood += 15;
            }
            if ((c.tag == "Pit") && (c.Fraction == 2))
            {
                Gold += 5;
                Stone += 15;
            }
            if ((c.tag == "TownHall") && (c.Fraction == 2))
            {
                Wood += 10;
                Gold += 10;
                Stone += 10;
            }
        }

        //вызов скрипта ведения боя
        GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().BasicAlgorithm();
        //делаем поейера активным и запускаем таймер
        GameObject.Find("Map").GetComponent<Map>().ActivePlayer = true;
        //Timer.timObject.StartTimer();

    }

    public int FindEfficientBuild(string tag)//поиск здания
    {

        int number = 0;
        //здесь пробегаем по списку строений и по их статусам
        foreach (Unit c in UnitList)
        {
            if ((c.tag == tag) && (c.Fraction == 2))
            {
                number++;
            }
        }
        //если находим подходящее - добавляем единичку


        return number;
    }

    public int FindOutHowManySoldiers()
    {

        int number = 0;
        foreach (Unit u in UnitList)
        {

            if (((u.tag == "Swordsman") || (u.tag == "Archer") || (u.tag == "Mage") || (u.tag == "Killer")) && (u.Fraction == 2))
            {
                number++;
            }
        }
        return number;
    }

    public int FindOutHowManyUnit(string tag)
    {

        int number = 0;
        foreach (Unit u in UnitList)
        {

            if ((u.tag == tag) && (u.Fraction == 2))
            {
                number++;
            }
        }
        return number;
    }


    //----------------так можно чекнуть тип ячейки--------------------------------------------------
    public bool CheckIndexForType(int X, int Y, Cell.CellType type)
    {
        if (m.GetCell(X, Y) && m.GetCell(X, Y).Type == type)
            return true;
        else
            return false;
    }
    //-------------------------------------------------------------------

    public void BuildSawmill()//внутри должна быть функция,которая ищет ячейку рядом с лесом и ближе всего к базе
    {
        if (Town.CurrentNumberActionPoints <= 0)
            return;
        if ((Gold < 10) || (Wood < 15))
            return;
        int w = 1;
        int[][] M = m.GetMatrixOfFreeCells(xTown, yTown, 5);
        while (w <= 5)
        {
            for (int i = 0; i < m.NumberOfCellsOnAxisX; i++)
                for (int j = 0; j < m.NumberOfCellsOnAxisY; j++)
                    if (M[i][j] == w && !m.GetCell(i, j).LocatedHereUnit)
                    {
                        if (Construction.CreateConstruction(m.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, i, j, m.UnitList, 2))
                        {
                            Wood -= 15; Gold -= 10; Town.CurrentNumberActionPoints = 0;
                            return;
                        }
                    }
            w++;
        }


    }

    public void BuildPit()//внутри должна быть функция,которая ищет ячейку рядом с рудой и ближе всего к базе
    {
        if (Town.CurrentNumberActionPoints <= 0)
            return;
        if ((Gold < 20) || (Wood < 30))
            return;
        int w = 1;
        int[][] M = m.GetMatrixOfFreeCells(xTown, yTown, 5);
        while (w <= 5)
        {
            for (int i = 0; i < m.NumberOfCellsOnAxisX; i++)
                for (int j = 0; j < m.NumberOfCellsOnAxisY; j++)
                    if (M[i][j] == w && !m.GetCell(i, j).LocatedHereUnit)
                    {
                        if (Construction.CreateConstruction(m.UnitPrefabArray[6], Construction.ConstructionType.Pit, i, j, m.UnitList, 2))
                        {
                            Wood -= 30; Gold -= 20; Town.CurrentNumberActionPoints = 0;
                            return;
                        }
                    }
            w++;
        }




    }

    public void BuildBarracks()// найти ячейку рядом с базой и построить в этой ячейке казарму
    {
        if (Town.CurrentNumberActionPoints <= 0)
            return;
        if ((Gold < 30) || (Wood < 20) || (Stone < 20))
            return;
        int w = 1;
        int[][] M = m.GetMatrixOfFreeCells(xTown, yTown, 5);
        while (w < 5)
        {
            for (int i = 0; i < m.NumberOfCellsOnAxisX; i++)
                for (int j = 0; j < m.NumberOfCellsOnAxisY; j++)
                    if (M[i][j] == w && !m.GetCell(i, j).LocatedHereUnit)
                    {
                        if (Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, i, j, m.UnitList, 2))
                        {
                            Gold -= 30; Wood -= 20; Stone -= 20; Town.CurrentNumberActionPoints = 0;
                            return;
                        }
                    }
            w++;
        }

        //if ((Gold >= 30) && (Wood >= 20)&&(Stone>=20))
        //    for (int k = 1; k < m.NumberOfCellsOnAxisX; k++)
        //{
        //    for (int i = xTown - k + 1; i <= xTown + k; i++)
        //    {
        //        if (m.CheckIndex(i, yTown + k))
        //        {
        //            Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, i, yTown + k, m.UnitList, 2);
        //            xBarracks = i; yBarracks = yTown + k;
        //                Gold -= 30;Wood -= 20;Stone -= 20;
        //                return;

        //        }
        //    }
        //    for (int j = yTown + k - 1; j >= yTown - k; j--)
        //    {
        //        if (m.CheckIndex(xTown + k, j))
        //        {
        //            Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, xTown + k, j, m.UnitList, 2);
        //            xBarracks = xTown + k; yBarracks = j;
        //                Gold -= 30; Wood -= 20; Stone -= 20;
        //                return;
        //        }
        //    }
        //    for (int i = xTown + k - 1; i >= xTown - k; i--)
        //    {
        //        if (m.CheckIndex(i, yTown - k))
        //        {
        //            Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, i, yTown - k, m.UnitList, 2);
        //            xBarracks = i; yBarracks = yTown - k;
        //                Gold -= 30; Wood -= 20; Stone -= 20;
        //                return;
        //        }
        //    }
        //    for (int j = yTown - k + 1; j <= yTown + k; j++)
        //    {
        //        if (m.CheckIndex(xTown - k, j))
        //        {
        //            Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, xTown - k, j, m.UnitList, 2);
        //            xBarracks = xTown - k; yBarracks = j;
        //                Gold -= 30; Wood -= 20; Stone -= 20;
        //                return;
        //        }
        //    }
        //}


    }


    public void OrderUnit(string tag, int prefabNumber, Unit.UnitType uType, int Value) //заказ юнита
    {
        int w = 1;
        int[][] M = m.GetMatrixOfFreeCells(xTown, yTown, 5);
        while (w < 5)
        {
            for (int i = 0; i < m.NumberOfCellsOnAxisX; i++)
                for (int j = 0; j < m.NumberOfCellsOnAxisY; j++)
                    if (M[i][j] == w && !m.GetCell(i, j).LocatedHereUnit)
                    {
                        Unit.CreateUnit(m.UnitPrefabArray[prefabNumber], uType, i, j, m.UnitList, 2);
                        Gold -= Value;
                        return;
                    }
            w++;
        }
    }

    public bool FindEnemyAroundImportantBuildings()
    {
        bool Assault = false;
        // Debug.Log("проверяем тревогу");


        //проверяет есть ли противники в зоне видимости важных зданий
        //как реализовать? можно создать ещё одну матрицу, в которой отмечать ячейки видимости, при постройке зданий
        //и потом проверять их по общей матрице

        return Assault;
    }

    public GameObject FindAttackedBuilding()
    {
        GameObject obj = gameObject;//это чтобы не ругался
                                    //бежит по списку строений ии и проверяет статусы
                                    //если находит атакованный, то возвращает его
                                    //иначе возвращает ратушу
        return obj;
    }

    public void ProtectBuilding(GameObject obj)
    {
        //проверяет , есть ли вражеские юниты рядом с объектом
        //если есть, назначает цель атаки
        // если нет - назначает цель для перемещения
    }

    public void AttackOn()//просто запускает режим боя у ИИ
    {
        Debug.Log("Выступает на врага");
        GameObject.Find("Map").GetComponent<BattleArtificialIntelligence>().Attack = true;

    }

    public bool ThereAreResourcesForBuilding()
    {
        //Debug.Log("проверяем ресурсы для строительства");
        bool kuchaBabosov = false; // заведомо полагаем, что нет у нас кучи бабосов...плак...
        //проверяет, хватает ли ресурсов на постройку лесопилки/каменоломни
        return kuchaBabosov;
    }


}