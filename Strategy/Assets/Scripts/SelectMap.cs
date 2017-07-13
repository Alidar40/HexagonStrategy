using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : MonoBehaviour {

	void Start () {
        //if (!PlayerPrefs.HasKey("SelectMap"))

    }
	
	public void LoadMap(int index)
    {
        PlayerPrefs.SetInt("SelectMap", index);
    }
}
