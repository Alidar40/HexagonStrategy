using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GameEvents : MonoBehaviour {

    //public Button btn;
    List<Unit> UnitList;
    Map m;
    Unit u;

    void Start()
    {
        m = GameObject.Find("Map").GetComponent<Map>();
        //u = GameObject.Find("").GetComponent<Map>();
        UnitList = m.UnitList;
        //btn.onClick.AddListener(TaskOnClick);      
    }

 
    public void TaskOnClick()
    {
        foreach (Unit u in UnitList)
        {
            u.LaunchNextTurn += NextUnit_LaunchNextTurn;
            u.LaunchNextTurn += u.UpdateActionPoints;
            u.NextTurn(UnitList);
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

