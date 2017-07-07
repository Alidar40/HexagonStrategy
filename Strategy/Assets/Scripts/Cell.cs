using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    public enum CellType { Grass = 1, Forest = 2, Water = 3, Mountain = 4};
    public Sprite[] SpriteCellArray;
    public CellType Type;
    public Unit LocatedHereUnit;
    public int indexX, indexY;
    private SpriteRenderer _SpriteRenderer;
    void Start () {
        _SpriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	
	void Update () {
	
	}

    public string ToString()   //стоит переменовать
    {
        return indexX + " " + indexY;
    }
    public void SetType(int newType)
    {
        if (newType < 0 || newType > 5)
            return;
        if (!_SpriteRenderer)
            _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SpriteRenderer.sprite = SpriteCellArray[newType - 1];
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
        }
    }
}
