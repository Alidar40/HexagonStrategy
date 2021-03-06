﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Text textType;
    public Text textHP;
    public Text textAP;


    private Unit obj;
    private Construction cons;


	void Update () {
        ShowInfo();
    }

    public void ShowInfo()
    {
        obj = GameObject.Find("Map").GetComponent<Map>().ActiveUnit;
        if (obj.Type == Unit.UnitType.Construction)
        {
            cons = GameObject.Find("Map").GetComponent<Map>().ActiveUnit.GetComponent<Construction>();
            textType.text = "Name: " + cons._ConstructionType;
            textHP.text = "HP: " + cons.Hitpoints + "/" + cons.MaxHitpoints;
            textAP.text = "AP: " + cons.CurrentNumberActionPoints + "/" + cons.StandardNumberActionPoints;
        }
        else
        {
            textType.text = "Name: " + obj.Type;
            textHP.text = "HP: " + obj.Hitpoints + "/" + obj.MaxHitpoints; 
            textAP.text = "AP: " + obj.CurrentNumberActionPoints + "/" + obj.StandardNumberActionPoints;
        }
    }

}
