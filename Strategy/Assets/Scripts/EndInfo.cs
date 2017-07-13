using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EndInfo : MonoBehaviour {

    Text textEnd;
    Map map;
    //Menu menu;

	// Use this for initialization
	void Start () {
        textEnd = GameObject.Find("Main Camera/GameUICamera/Canvas/EndInfo/EndText").GetComponent<Text>();
        map = GameObject.Find("Map").GetComponent<Map>();
        if (map.PlayerTownHall == null)
        {
            textEnd.text = "YOU LOST";
        }
        else
        {
            textEnd.text = "YOU WIN";
        }
	}
	
    public void ClickOnOk()
    {
        File.Delete("QuickSave.txt");
        //Destroy(menu);
        Application.LoadLevel("MainMenu");

    }
}
