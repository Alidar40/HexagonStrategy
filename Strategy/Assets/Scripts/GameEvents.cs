using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GameEvents : MonoBehaviour {

    //public Button btn;
    List<Unit> UnitList;
    Map m;
    Unit u;
    Construction c;
    Resources r;

    void Start()
    {
        m = GameObject.Find("Map").GetComponent<Map>();
        //u = GameObject.Find("").GetComponent<Map>();
        UnitList = m.UnitList;
        //Resources.ShowResources();
        //btn.onClick.AddListener(TaskOnClick);      
    }


    public void TaskOnClick()
    {
       
    }

    public void NextTurn()
    {
        if (m.ActivePlayer) //проверяем активность плейера
        {
            foreach (Unit u in UnitList)
            {
                u.LaunchNextTurn += NextUnit_LaunchNextTurn;
                u.LaunchNextTurn += u.UpdateActionPoints;
                u.NextTurn(UnitList);
            }
            foreach (Unit c in UnitList)
            {
                if ((c.tag == "TownHall") && (c.Fraction == m.PlayerFraction))
                {
                    m.Wood += 10; //Map.WOOD;
                    m.Gold += 10;
                    m.Stone += 10;
                }
                if ((c.tag == "Sawmill") && (c.Fraction == m.PlayerFraction))
                {
                    m.Wood += 15; //Map.WOOD;
                }
                if ((c.tag == "Pit") && (c.Fraction == m.PlayerFraction))
                {
                    m.Gold += 5;// Map.GOLD;
                    m.Stone += 15;// Map.STONE;
                }
            }

            m.ActivePlayer = false;//активность пропадает при нажатии next turn
            //Timer.timObject.StartTimer(); //заново запускает таймер
        }
        Resources.ShowResources();

    }

    private void NextUnit_LaunchNextTurn(List<Unit> UnitList)
    {
        //Debug.Log("ok");
        foreach (Unit u in UnitList)
        {
            u.LaunchNextTurn -= NextUnit_LaunchNextTurn;
            
        }
    }

}

