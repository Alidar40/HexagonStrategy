using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    [Header("TableOptions")]
    public int NumberOfCellsOnAxisX = 10;
    public int NumberOfCellsOnAxisY = 10;
    public GameObject CellPrefab;

    void Start () {
        GenerateNewTable();

    }
	

	void Update () {
	
	}
    public void GenerateNewTable()
    {
        for (int i = 0; i < NumberOfCellsOnAxisX; i++)
        {
            GameObject Line = new GameObject();
            Line.transform.parent = transform;
            Line.transform.localPosition = Vector3.zero;
            Line.name = "Line_" + i;
            for (int j = 0; j < NumberOfCellsOnAxisY; j++)
            {
                GameObject newCell = Instantiate(CellPrefab);
                newCell.transform.parent = Line.transform;
                newCell.transform.position = transform.position + new Vector3(i, j);
                newCell.name = "Cell_" + i + "_" + j;
            }
        }
    }
}
