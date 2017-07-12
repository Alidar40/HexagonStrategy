using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligence : MonoBehaviour {

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


   public int xTown = 15;
   public int yTown = 5;
    int xBarracks;
    int yBarracks;



    public void Start ()
    {
      
        m = GameObject.Find("Map").GetComponent<Map>();
        UnitList = m.UnitList;
        Gold = 50; Stone = 50; Wood = 50;
      
        
    }


    public void BasicAlgorithm()
    {
     if (FindEfficientBuild("Sawmill") < 2)
        {
            BuildSawmill();
        }

        if (FindEfficientBuild("Pit") == 0)
        {
            BuildPit();
        }

        if (FindEfficientBuild("Barracks")==0)
        {
            BuildBarracks();
        }
        else
        {
            //заказываем юнитов
            if ((Gold >= 50))
                    OrderUnit("Killer", 3, Unit.UnitType.Killer, 50);
            else
            if (Gold >= 20)
                    OrderUnit("Archer", 1, Unit.UnitType.Archer, 20);
            else
            if (Gold >= 10)
                    OrderUnit("Sawmill", 0, Unit.UnitType.Swordsman, 10);
        }
            
        if (FindOutHowManySoldiers()>=5)
           {
            AttackOn();
           }
            
          if ((Gold >= 20) && (Wood >= 30)) BuildPit();


         foreach (Unit c in UnitList)
        {
            if ((c.tag == "Sawmill") && (c.Fraction == 2))
            {
                Wood += 15;
            }
            if ((c.tag == "Pit") && (c.Fraction == 2))
            {
                Gold += 15;
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
        Timer.timObject.StartTimer();

    }

    public int FindEfficientBuild(string tag)//поиск здания
    {
    
        int number = 0;
        //здесь пробегаем по списку строений и по их статусам
        foreach (Unit c in UnitList)
        {
            if ((c.tag == tag) && (c.Fraction ==2))
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
            
            if (((u.tag == "Swordsman")|| (u.tag == "Archer") || (u.tag == "Mage") || (u.tag == "Killer")) && (u.Fraction == 2))
            {
                number++;
            }
        }
        return number;
    }
   
    //----------------так можно чекнуть тип ячейки--------------------------------------------------
    public bool CheckIndexForType(int X, int Y,Cell.CellType type)
    {
        if (m.GetCell(X, Y) && m.GetCell(X, Y).Type == type)
            return true;
        else
            return false;
    }
    //-------------------------------------------------------------------

    public void BuildSawmill()//внутри должна быть функция,которая ищет ячейку рядом с лесом и ближе всего к базе
    {
        

        if ((Gold >= 10) && (Wood >= 15))
            for (int k = 1;k< m.NumberOfCellsOnAxisX;k++)
        {
            for (int i = xTown - k + 1; i <= xTown + k; i++)
            {
                if (m.CheckIndex(i, yTown+k))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, i, yTown+k, m.UnitList, 2);
                    Wood -= 15; Gold -= 10;
                   return;
                }
            }
            for (int j = yTown + k ; j >= yTown - k; j--)
            {
                if (m.CheckIndex(xTown + k, j))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, xTown + k, j , m.UnitList, 2);
                    Wood -= 15; Gold -= 10;
                    return;
                }
            }
            for (int i = xTown + k - 1; i >= xTown -k; i--)
            {
                if (m.CheckIndex(i, yTown - k))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, i, yTown - k, m.UnitList, 2);
                    Wood -= 15; Gold -= 10;
                    return;
                }
            }
            for (int j = yTown - k + 1; j <= yTown + k; j++)
            {
                if (m.CheckIndex(xTown - k, j))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, xTown - k, j, m.UnitList, 2);
                    Wood -= 15; Gold -= 10;
                    return;
                }
            }
        }
      



     
       
    }

    public void BuildPit()//внутри должна быть функция,которая ищет ячейку рядом с рудой и ближе всего к базе
    {
        
        if ((Gold>=20)&&(Wood>=30))
        for (int k = 1; k < m.NumberOfCellsOnAxisX; k++)
        {
            for (int i = xTown - k + 1; i <= xTown + k; i++)
            {
                if (m.CheckIndex(i, yTown + k))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[6], Construction.ConstructionType.Pit, i, yTown + k, m.UnitList, 2);
                    Wood -= 30; Gold -= 20;
                    return;
                }
            }
            for (int j = yTown + k - 1; j >= yTown - k; j--)
            {
                if (m.CheckIndex(xTown + k, j))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[6], Construction.ConstructionType.Pit, xTown + k, j, m.UnitList, 2);
                    Wood -= 30; Gold -= 20;
                    return;
                }
            }
            for (int i = xTown + k - 1; i >= xTown - k; i--)
            {
                if (m.CheckIndex(i, yTown - k))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[6], Construction.ConstructionType.Pit, i, yTown - k, m.UnitList, 2);
                    Wood -= 30; Gold -= 20;
                    return;
                }
            }
            for (int j = yTown - k + 1; j <= yTown + k; j++)
            {
                if (m.CheckIndex(xTown - k, j))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[6], Construction.ConstructionType.Pit, xTown - k, j, m.UnitList, 2);
                    Wood -= 30; Gold -= 20;
                    return;
                }
            }
        }
        
      
        
    }

    public void BuildBarracks()// найти ячейку рядом с базой и построить в этой ячейке казарму
    {
        
        if ((Gold >= 30) && (Wood >= 20)&&(Stone>=20))
            for (int k = 1; k < m.NumberOfCellsOnAxisX; k++)
        {
            for (int i = xTown - k + 1; i <= xTown + k; i++)
            {
                if (m.CheckIndex(i, yTown + k))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, i, yTown + k, m.UnitList, 2);
                    xBarracks = i; yBarracks = yTown + k;
                        Gold -= 30;Wood -= 20;Stone -= 20;
                        return;

                }
            }
            for (int j = yTown + k - 1; j >= yTown - k; j--)
            {
                if (m.CheckIndex(xTown + k, j))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, xTown + k, j, m.UnitList, 2);
                    xBarracks = xTown + k; yBarracks = j;
                        Gold -= 30; Wood -= 20; Stone -= 20;
                        return;
                }
            }
            for (int i = xTown + k - 1; i >= xTown - k; i--)
            {
                if (m.CheckIndex(i, yTown - k))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, i, yTown - k, m.UnitList, 2);
                    xBarracks = i; yBarracks = yTown - k;
                        Gold -= 30; Wood -= 20; Stone -= 20;
                        return;
                }
            }
            for (int j = yTown - k + 1; j <= yTown + k; j++)
            {
                if (m.CheckIndex(xTown - k, j))
                {
                    Construction.CreateConstruction(m.UnitPrefabArray[5], Construction.ConstructionType.Barracks, xTown - k, j, m.UnitList, 2);
                    xBarracks = xTown - k; yBarracks = j;
                        Gold -= 30; Wood -= 20; Stone -= 20;
                        return;
                }
            }
        }
       
       
    }

   
    public void OrderUnit(string tag, int prefabNumber, Unit.UnitType uType, int Value) //заказ юнита
    {
       

            for (int k = 3; k < m.NumberOfCellsOnAxisX; k++)
            {
                for (int i = xTown - k + 1; i <= xTown + k; i++)
                {
                    if (m.CheckIndex(i, yTown + k))
                    {
                        Unit.CreateUnit(m.UnitPrefabArray[prefabNumber], uType, i, yTown + k, m.UnitList,2);
                        Gold -= Value; 
                        return;

                    }
                }
                for (int j = yTown + k - 1; j >= yTown - k; j--)
                {
                    if (m.CheckIndex(xTown + k, j))
                    {
                        Unit.CreateUnit(m.UnitPrefabArray[prefabNumber], uType, xTown + k, j, m.UnitList, 2);
                       Gold -= Value; 
                        return;
                    }
                }
                for (int i = xTown + k - 1; i >= xTown - k; i--)
                {
                    if (m.CheckIndex(i, yTown - k))
                    {
                    Unit.CreateUnit(m.UnitPrefabArray[prefabNumber], uType, i, yTown - k, m.UnitList, 2);
                    Gold -= Value; 
                        return;
                    }
                }
                for (int j = yTown - k + 1; j <= yTown + k; j++)
                {
                    if (m.CheckIndex(xTown - k, j))
                    {
                    Unit.CreateUnit(m.UnitPrefabArray[prefabNumber], uType, xTown - k, j, m.UnitList, 2);
                    Gold -= Value;
                        return;
                    }
                }
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
        GameObject obj=gameObject;//это чтобы не ругался
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
