using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    public enum CellType { Grass = 1, Forest = 2, Water = 3, Mountain = 4, House = 5 };
    public CellType Type;
    public Unit LocatedHereUnit;
    void Start () {
        

    }
	
	
	void Update () {
	
	}
}
