using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;


public class MapLoader : MonoBehaviour
{
    Map MapToSave;
    Unit UnitBufer;
    Cell CellBufer;
    Camera CameraToSave;
    GameObject mapPrefab;

    void Start()
    {
        MapToSave = GameObject.Find("Map").GetComponent<Map>();
        mapPrefab = GameObject.Find("Map");
        CameraToSave = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void Save(string saveName)
    {
        StreamWriter sw = new StreamWriter(saveName + ".txt");

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
                sw.WriteLine(MapToSave.GetCell(i, j).Type);
            }
        }

        sw.WriteLine("UnitsSettings");
        sw.WriteLine(MapToSave.UnitList.Count);
        for (int i = 0; i < MapToSave.UnitList.Count; i++)
        {
            sw.WriteLine(MapToSave.UnitList[i].Type);
            sw.WriteLine(MapToSave.UnitList[i].name);
            sw.WriteLine(MapToSave.UnitList[i].CurrentCell.indexX);
            sw.WriteLine(MapToSave.UnitList[i].CurrentCell.indexY);
            sw.WriteLine(MapToSave.UnitList[i].Hitpoints);
            sw.WriteLine(MapToSave.UnitList[i].Damage);
            sw.WriteLine(MapToSave.UnitList[i].CurrentNumberActionPoints);
            sw.WriteLine(MapToSave.UnitList[i].StandardNumberActionPoints);

        }

        sw.Close();
    }

    public void Load(string saveName)
    {
        if (File.Exists(saveName + ".txt"))
        {
            while (MapToSave.UnitList.Count > 0)
            {
                Unit.DeleteUnit(MapToSave.UnitList, MapToSave.UnitList[0]);
            }
            StreamReader sr = new StreamReader(saveName + ".txt");
            string buf;
            buf = sr.ReadLine();
            Vector3 newCamPos = new Vector3(Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()));
            CameraToSave.transform.position = newCamPos;
            buf = sr.ReadLine();
            buf = sr.ReadLine();
            MapToSave.NumberOfCellsOnAxisX = Convert.ToInt32(buf, 10);
            buf = sr.ReadLine();
            MapToSave.NumberOfCellsOnAxisY = Convert.ToInt32(buf, 10);
            MapToSave.GenerateNewTable();
            for (int i = 0; i < MapToSave.NumberOfCellsOnAxisX; i++)
            {
                for (int j = 0; j < MapToSave.NumberOfCellsOnAxisY; j++)
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

            int NUnit = Convert.ToInt32(sr.ReadLine(), 10);
            for (int _i = 0; _i < NUnit; _i++)
            {
                buf = sr.ReadLine();
                if (buf == "Swordsman")
                {
                    buf = sr.ReadLine();
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[0], Unit.UnitType.Swordsman, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf,1);
                }
                if (buf == "Archer")
                {
                    buf = sr.ReadLine();
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[0], Unit.UnitType.Archer, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf,1);
                }
                if (buf == "Mage")
                {
                    buf = sr.ReadLine();
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[0], Unit.UnitType.Mage, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf,1);
                }
                if (buf == "Killer")
                {
                    buf = sr.ReadLine();
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[0], Unit.UnitType.Killer, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf,1);
                }
                UnitBufer = GameObject.Find(buf).GetComponent<Unit>();
                UnitBufer.Hitpoints = Convert.ToSingle(sr.ReadLine());
                UnitBufer.Damage = Convert.ToSingle(sr.ReadLine());
                UnitBufer.CurrentNumberActionPoints = Convert.ToInt32(sr.ReadLine());
                UnitBufer.StandardNumberActionPoints = Convert.ToInt32(sr.ReadLine());
            }
            sr.Close();
        }
        else
        {
            Debug.Log("File doesn't exist");
        }
    }
}

