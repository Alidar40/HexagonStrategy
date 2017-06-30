using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    float Hitpoints, Damage;
    public Cell CurrentCell;

    Map _Map;
    void Awake() {
        _Map = GameObject.Find("Map").GetComponent<Map>();
    }

    public void SetCell(int X, int Y)
    {
        CurrentCell = _Map.GetCell(X, Y);
        transform.position = CurrentCell.transform.position;
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
