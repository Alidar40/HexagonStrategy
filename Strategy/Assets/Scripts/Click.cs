using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class Click : MonoBehaviour {

    public Button btn;
    public  List<Unit> UnitList;
    public Map m;
    public Unit u;

    void Start()
    {
        UnitList = m.UnitList;
        btn.onClick.AddListener(TaskOnClick);      
    }

 
    public void TaskOnClick()
    {
        u.LaunchNextTurn += NextUnit_LaunchNextTurn;
        u.NextTurn(UnitList);

    }

    private void NextUnit_LaunchNextTurn(List<Unit> UnitList)
    {
        Debug.Log("ok");
        u.LaunchNextTurn -= NextUnit_LaunchNextTurn;
    }

}

