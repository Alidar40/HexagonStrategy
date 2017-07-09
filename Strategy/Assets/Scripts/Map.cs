using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    [Header("TableOptions")]
    public int NumberOfCellsOnAxisX = 10;
    public int NumberOfCellsOnAxisY = 10;
    public GameObject CellPrefab;


    void Start () {
        GenerateNewTable();
       // OnMouseEnter();
    }


    void FixedUpdate () {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
#endif
//#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                ScreenRay(i);
            }
                
//#endif
            
           
        //if (Input.GetAxis("Cancel") > 0)
        //{
         //   OnMouseExit();
        //}
    }



    public void ScreenRay(int androidI)
    {
        RaycastHit2D hitInfo = new RaycastHit2D();

#if UNITY_STANDALONE_WIN
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
#endif

#if UNITY_ANDROID
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(androidI).position), Vector2.zero);
       // hitInfo = Physics2D.Raycast(new Vector2 (-4.249481f, -1.17f), Vector2.zero);
#endif

        if (hitInfo.collider)
        {
            Debug.Log(hitInfo.transform.gameObject.name);
        }
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

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
