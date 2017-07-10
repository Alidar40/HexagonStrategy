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

    void Start()
    {
        m = GameObject.Find("Map").GetComponent<Map>();
        //u = GameObject.Find("").GetComponent<Map>();
        UnitList = m.UnitList;
        //btn.onClick.AddListener(TaskOnClick);      
    }

 
    public void TaskOnClick()
    {
       
    }

    public void NextTurn()
    {
        if (GameObject.Find("Map").GetComponent<Map>().ActivePlayer) //проверяем активность плейера
        {
            foreach (Unit u in UnitList)
            {
                u.LaunchNextTurn += NextUnit_LaunchNextTurn;
                u.LaunchNextTurn += u.UpdateActionPoints;
                u.NextTurn(UnitList);
            }
            foreach (Unit c in UnitList)
            {
                if ((c.tag == "Sawmill") && (c.Fraction == GameObject.Find("Map").GetComponent<Map>().ActiveUnit.Fraction))
                {
                    GameObject.Find("Map").GetComponent<Map>().Wood += Map.WOOD;
                }
                if ((c.tag == "Pit") && (c.Fraction == GameObject.Find("Map").GetComponent<Map>().ActiveUnit.Fraction))
                {
                    GameObject.Find("Map").GetComponent<Map>().Gold += Map.GOLD;
                    GameObject.Find("Map").GetComponent<Map>().Stone += Map.STONE;
                }
            }

            GameObject.Find("Map").GetComponent<Map>().ActivePlayer = false;//активность пропадает при нажатии next turn
            Timer.timObject.StartTimer(); //заново запускает таймер
        }

    }

    private void NextUnit_LaunchNextTurn(List<Unit> UnitList)
    {
        Debug.Log("ok");
        foreach (Unit u in UnitList)
        {
            u.LaunchNextTurn -= NextUnit_LaunchNextTurn;
            
        }
    }

}

