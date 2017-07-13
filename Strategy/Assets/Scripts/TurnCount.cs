using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnCount : MonoBehaviour {

    Text textCount;
    Map _Map;

	void Start () {
        _Map = GameObject.Find("Map").GetComponent<Map>();
        textCount = GameObject.Find("Main Camera/GameUICamera/Canvas/TurnCounter").GetComponent<Text>();
    }
	
	void Update () {
        textCount.text = _Map.TurnCounter.ToString();
	}
}
