using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Resources : MonoBehaviour {

    public Text textGold;
    public Text textStone;
    public Text textWood;

    private Map map;


    public void ShowResources()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        textGold.text = map.Gold.ToString();
        textStone.text = map.Stone.ToString();
        textWood.text = map.Wood.ToString();
    }
}
