using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml.Serialization;


public class MapLoader : MonoBehaviour
{

    Map MapToSave;
    Unit UnitBufer;
    Cell CellBufer;
    Camera CameraToSave;
    public Map mapPrefab;



    void Start()
    {
        MapToSave = GameObject.Find("Map").GetComponent<Map>();
        CameraToSave = GameObject.Find("Main Camera").GetComponent<Camera>();
    }



    public void Save(string saveName)
    {

        StreamWriter sw = new StreamWriter(saveName);

        sw.WriteLine("CameraSettings");
        sw.WriteLine(CameraToSave.transform.position.x);
        sw.WriteLine(CameraToSave.transform.position.y);
        sw.WriteLine(CameraToSave.transform.position.z);

        sw.WriteLine("MapSettings");
        sw.WriteLine(MapToSave.NumberOfCellsOnAxisX);
        sw.WriteLine(MapToSave.NumberOfCellsOnAxisY);

        for (int i = 0; i < MapToSave.NumberOfCellsOnAxisX; i++)
        {
            for (int j = 0; j < MapToSave.NumberOfCellsOnAxisY; j++)
            {
                sw.WriteLine(MapToSave.GetCell(i,j).Type);
            }
        }

        sw.WriteLine("UnitsSettings");
        sw.WriteLine(MapToSave.UnitList.Count);
        for (int i = 0; i < MapToSave.UnitList.Count; i++)
        {
            sw.WriteLine(MapToSave.UnitList[i].CurrentCell.indexX);
            sw.WriteLine(MapToSave.UnitList[i].CurrentCell.indexY);
        }

        sw.Close();
    }

    public void Load(string saveName)
    {
        Destroy(GameObject.Find("Map"));
        Destroy(GameObject.Find("TestUnit1_1"));
        Map newMap = Instantiate(mapPrefab);
        newMap.name = "Map";
        StreamReader sr = new StreamReader(saveName);
        string buf;
        buf = sr.ReadLine();
        Vector3 newCamPos = new Vector3(Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()));
        CameraToSave.transform.position = newCamPos;
        buf = sr.ReadLine();
        buf = sr.ReadLine();
        newMap.NumberOfCellsOnAxisX = Convert.ToInt32(buf, 10);
        buf = sr.ReadLine();
        newMap.NumberOfCellsOnAxisY = Convert.ToInt32(buf, 10);
        newMap.GenerateNewTable();
        for (int i = 0; i < newMap.NumberOfCellsOnAxisX; i++)
        {
            for (int j = 0; j < newMap.NumberOfCellsOnAxisY; j++)
            {
                CellBufer = GameObject.Find("Cell_" + i + "_" + j).GetComponent<Cell>();
                buf = sr.ReadLine();
                if (buf == "Grass")
                {
                    CellBufer.SetType(1);
                }
                if (buf == "Forest")
                {
                    CellBufer.SetType(2);
                }
                if (buf == "Water")
                {
                    CellBufer.SetType(3);
                }
                if (buf == "Mountain")
                {
                    CellBufer.SetType(4);
                }
                if (buf == "House")
                {
                    CellBufer.SetType(5);
                }
            }
        }

        buf = sr.ReadLine();
        for (int _i = 0; _i < Convert.ToInt32(sr.ReadLine(), 10); _i++)
        {
            Unit.CreateUnit(newMap.UnitPrefabArray[0], Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), newMap.UnitList);
        }

        sr.Close();

    }
}

