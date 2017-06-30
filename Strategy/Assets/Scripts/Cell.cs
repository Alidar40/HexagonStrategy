using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    public enum CellType { Grass = 1, Forest = 2, Water = 3, Mountain = 4, House = 5 };
    public Sprite[] SpriteCellArray;
    public CellType Type;
    public Unit LocatedHereUnit;
    private SpriteRenderer _SpriteRenderer;
    void Start () {
        _SpriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	
	void Update () {
	
	}
    public void SetType(int newType)
    {
        _SpriteRenderer.sprite = SpriteCellArray[newType];
        switch (newType)
        {
            case 1:
                Type = CellType.Grass;
                break;
            case 2:
                Type = CellType.Forest;
                break;
            case 3:
                Type = CellType.Water;
                break;
            case 4:
                Type = CellType.Mountain;
                break;
            case 5:
                Type = CellType.House;
                break;
        }
    }
}
