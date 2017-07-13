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
<<<<<<< HEAD
        Application.LoadLevel("TestScen");
=======
>>>>>>> 5dab9b106bca04a0f9dfc063084d01c3532e43a4
    }
}
