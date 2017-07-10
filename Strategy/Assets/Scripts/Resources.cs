using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Resources : MonoBehaviour {

    public static Text textGold;
    public static Text textStone;
    public static Text textWood;

    private static Map map;

    private void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        textGold = GameObject.Find("Resources/Gold/Text").GetComponent<Text>();
        textStone = GameObject.Find("Resources/Stone/Text").GetComponent<Text>();
        textWood = GameObject.Find("Resources/Wood/Text").GetComponent<Text>();
    }


    public static void ShowResources()
    {
        textGold.text = map.Gold.ToString();
        textStone.text = map.Stone.ToString();
        textWood.text = map.Wood.ToString();
    }
}
