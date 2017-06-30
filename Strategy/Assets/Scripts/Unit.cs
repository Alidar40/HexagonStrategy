using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    float Hitpoints, Damage;
    void Start () {
		
	}

    void ToDamage(float _Damage)
    {
        Hitpoints -= _Damage;
        if (Hitpoints <= 0)
            Destroy();
    }
    void Destroy()
    {

    }
}
