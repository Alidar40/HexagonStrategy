﻿using System;
using System.IO;
using UnityEngine;


public class MapLoader : MonoBehaviour
{
    Map MapToSave;
    Menu menu;
    Unit UnitBufer;
    Construction ConstructionUnit;
    Cell CellBufer;
    Camera CameraToSave;
    GameObject mapPrefab;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public TextAsset[] MapArray;
    public TextAsset QuickMap;

    void Start()
    {
        MapToSave = GameObject.Find("Map").GetComponent<Map>();
        mapPrefab = GameObject.Find("Map");
        CameraToSave = GameObject.Find("Main Camera").GetComponent<Camera>();
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        if ((menu._State == Menu.State.Load))
        {
            Load("QuickSave");
            Destroy(menu.gameObject);
        }
        else if (menu._State == Menu.State.NewGame)
        {
            if (PlayerPrefs.HasKey("SelectMap"))
                Load(PlayerPrefs.GetInt("SelectMap"));
            Destroy(menu.gameObject);
        }
        
    }

    public void Save(string saveName)
    {

#if UNITY_STANDALONE_WIN
        StreamWriter sw = new StreamWriter(saveName + ".txt");
#endif

#if UNITY_ANDROID
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + saveName + ".txt");
#endif

        sw.WriteLine("TurnCounter");
        sw.WriteLine(MapToSave.TurnCounter);

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
            if (MapToSave.UnitList[i].Type == Unit.UnitType.Construction)
            {
                sw.WriteLine("Construction");
                ConstructionUnit = GameObject.Find(MapToSave.UnitList[i].name).GetComponent<Construction>();
                sw.WriteLine(ConstructionUnit._ConstructionType);
                sw.WriteLine(ConstructionUnit.name);
                sw.WriteLine(ConstructionUnit.CurrentCell.indexX);
                sw.WriteLine(ConstructionUnit.CurrentCell.indexY);
                sw.WriteLine(ConstructionUnit.Fraction);
                sw.WriteLine(ConstructionUnit.Hitpoints);
                sw.WriteLine(ConstructionUnit.Damage);
                sw.WriteLine(ConstructionUnit.CurrentNumberActionPoints);
                sw.WriteLine(ConstructionUnit.StandardNumberActionPoints);

            }
            else
            {
                sw.WriteLine("Unit");
                sw.WriteLine(MapToSave.UnitList[i].Type);
                sw.WriteLine(MapToSave.UnitList[i].name);
                sw.WriteLine(MapToSave.UnitList[i].CurrentCell.indexX);
                sw.WriteLine(MapToSave.UnitList[i].CurrentCell.indexY);
                sw.WriteLine(MapToSave.UnitList[i].Fraction);
                sw.WriteLine(MapToSave.UnitList[i].Hitpoints);
                sw.WriteLine(MapToSave.UnitList[i].Damage);
                sw.WriteLine(MapToSave.UnitList[i].CurrentNumberActionPoints);
                sw.WriteLine(MapToSave.UnitList[i].StandardNumberActionPoints);
            }

        }

        sw.Close();
    }

    public void Load(int index)
    {
        string[] SaveStringArray = StringToArray(MapArray[index].text);
        int StringCount = 0;
        string buf;
        MapToSave.TurnCounter = Convert.ToInt32(SaveStringArray[++StringCount], 10);
        StringCount++;
        Vector3 newCamPos = new Vector3(Convert.ToSingle(SaveStringArray[++StringCount]), Convert.ToSingle(SaveStringArray[++StringCount]), Convert.ToSingle(SaveStringArray[++StringCount]));
        CameraToSave.transform.position = newCamPos;
        StringCount++;
        MapToSave.NumberOfCellsOnAxisX = Convert.ToInt32(SaveStringArray[++StringCount], 10);
        MapToSave.NumberOfCellsOnAxisY = Convert.ToInt32(SaveStringArray[++StringCount], 10);
        MapToSave.DestroyTable();
        MapToSave.GenerateNewTable();
        for (int i = 0; i < MapToSave.NumberOfCellsOnAxisX; i++)
        {
            for (int j = 0; j < MapToSave.NumberOfCellsOnAxisY; j++)
            {
                CellBufer = MapToSave.GetCell(i, j);
                buf = SaveStringArray[++StringCount];
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
        StringCount++;
        int NUnit = Convert.ToInt32(SaveStringArray[++StringCount], 10);
        for (int _i = 0; _i < NUnit; _i++)
        {
            buf = SaveStringArray[++StringCount];
            if (buf == "Construction")
            {
                buf = SaveStringArray[++StringCount];
                if (buf == "TownHall")
                {
                    buf = SaveStringArray[++StringCount];
                    Construction.CreateConstruction(MapToSave.UnitPrefabArray[4], Construction.ConstructionType.TownHall, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                if (buf == "Barracks")
                {
                    buf = SaveStringArray[++StringCount];
                    Construction.CreateConstruction(MapToSave.UnitPrefabArray[5], Construction.ConstructionType.Barracks, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                if (buf == "Pit")
                {
                    buf = SaveStringArray[++StringCount];
                    Construction.CreateConstruction(MapToSave.UnitPrefabArray[6], Construction.ConstructionType.Pit, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                if (buf == "Sawmill")
                {
                    buf = SaveStringArray[++StringCount];
                    Construction.CreateConstruction(MapToSave.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                ConstructionUnit = GameObject.Find(buf).GetComponent<Construction>();
                ConstructionUnit.Hitpoints = Convert.ToSingle(SaveStringArray[++StringCount]);
                ConstructionUnit.Damage = Convert.ToSingle(SaveStringArray[++StringCount]);
                ConstructionUnit.CurrentNumberActionPoints = Convert.ToInt32(SaveStringArray[++StringCount]);
                ConstructionUnit.StandardNumberActionPoints = Convert.ToInt32(SaveStringArray[++StringCount]);
            }
            if (buf == "Unit")
            {
                buf = SaveStringArray[++StringCount];
                if (buf == "Swordsman")
                {
                    buf = SaveStringArray[++StringCount];
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[0], Unit.UnitType.Swordsman, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                if (buf == "Archer")
                {
                    buf = SaveStringArray[++StringCount];
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[1], Unit.UnitType.Archer, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf,
                    Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                if (buf == "Mage")
                {
                    buf = SaveStringArray[++StringCount];
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[2], Unit.UnitType.Mage, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                if (buf == "Killer")
                {
                    buf = SaveStringArray[++StringCount];
                    Unit.CreateUnit(MapToSave.UnitPrefabArray[3], Unit.UnitType.Killer, Convert.ToInt32(SaveStringArray[++StringCount], 10), Convert.ToInt32(SaveStringArray[++StringCount], 10), MapToSave.UnitList, buf, Convert.ToInt32(SaveStringArray[++StringCount], 10));
                }
                UnitBufer = GameObject.Find(buf).GetComponent<Unit>();
                UnitBufer.Hitpoints = Convert.ToSingle(SaveStringArray[++StringCount]);
                UnitBufer.Damage = Convert.ToSingle(SaveStringArray[++StringCount]);
                UnitBufer.CurrentNumberActionPoints = Convert.ToInt32(SaveStringArray[++StringCount]);
                UnitBufer.StandardNumberActionPoints = Convert.ToInt32(SaveStringArray[++StringCount]);
            }
        }
    }
    public void Load(string saveName)
    {
#if UNITY_STANDALONE_WIN
        if (File.Exists(saveName + ".txt"))
        {
#endif
#if UNITY_ANDROID
        if (File.Exists(Application.persistentDataPath + "/" + saveName + ".txt"))
        {
#endif
            while (MapToSave.UnitList.Count > 0)
            {
                Unit.DeleteUnit(MapToSave.UnitList, MapToSave.UnitList[0]);
            }
#if UNITY_STANDALONE_WIN
            StreamReader sr = new StreamReader(saveName + ".txt");
#endif
#if UNITY_ANDROID
           StreamReader sr = new StreamReader(Application.persistentDataPath + "/" + saveName + ".txt");
#endif
            string buf;
            buf = sr.ReadLine();
            MapToSave.TurnCounter = Convert.ToInt32(sr.ReadLine(), 10);
            buf = sr.ReadLine();
            Vector3 newCamPos = new Vector3(Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()));
            CameraToSave.transform.position = newCamPos;
            buf = sr.ReadLine();
            buf = sr.ReadLine();
            MapToSave.NumberOfCellsOnAxisX = Convert.ToInt32(buf, 10);
            buf = sr.ReadLine();
            MapToSave.NumberOfCellsOnAxisY = Convert.ToInt32(buf, 10);
            MapToSave.DestroyTable();
            MapToSave.GenerateNewTable();

            for (int i = 0; i < MapToSave.NumberOfCellsOnAxisX; i++)
            {
                for (int j = 0; j < MapToSave.NumberOfCellsOnAxisY; j++)
                {
                    CellBufer = MapToSave.GetCell(i, j);
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
                if (buf == "Construction")
                {
                    buf = sr.ReadLine();
                    if (buf == "TownHall")
                    {
                        buf = sr.ReadLine();
                        Construction.CreateConstruction(MapToSave.UnitPrefabArray[4], Construction.ConstructionType.TownHall, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    if (buf == "Barracks")
                    {
                        buf = sr.ReadLine();
                        Construction.CreateConstruction(MapToSave.UnitPrefabArray[5], Construction.ConstructionType.Barracks, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    if (buf == "Pit")
                    {
                        buf = sr.ReadLine();
                        Construction.CreateConstruction(MapToSave.UnitPrefabArray[6], Construction.ConstructionType.Pit, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    if (buf == "Sawmill")
                    {
                        buf = sr.ReadLine();
                        Construction.CreateConstruction(MapToSave.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    ConstructionUnit = GameObject.Find(buf).GetComponent<Construction>();
                    ConstructionUnit.Hitpoints = Convert.ToSingle(sr.ReadLine());
                    ConstructionUnit.Damage = Convert.ToSingle(sr.ReadLine());
                    ConstructionUnit.CurrentNumberActionPoints = Convert.ToInt32(sr.ReadLine());
                    ConstructionUnit.StandardNumberActionPoints = Convert.ToInt32(sr.ReadLine());
                }
                if (buf == "Unit")
                {
                    buf = sr.ReadLine();
                    if (buf == "Swordsman")
                    {
                        buf = sr.ReadLine();
                        Unit.CreateUnit(MapToSave.UnitPrefabArray[0], Unit.UnitType.Swordsman, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    if (buf == "Archer")
                    {
                        buf = sr.ReadLine();
                        Unit.CreateUnit(MapToSave.UnitPrefabArray[1], Unit.UnitType.Archer, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    if (buf == "Mage")
                    {
                        buf = sr.ReadLine();
                        Unit.CreateUnit(MapToSave.UnitPrefabArray[2], Unit.UnitType.Mage, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    if (buf == "Killer")
                    {
                        buf = sr.ReadLine();
                        Unit.CreateUnit(MapToSave.UnitPrefabArray[3], Unit.UnitType.Killer, Convert.ToInt32(sr.ReadLine(), 10), Convert.ToInt32(sr.ReadLine(), 10), MapToSave.UnitList, buf, Convert.ToInt32(sr.ReadLine(), 10));
                    }
                    UnitBufer = GameObject.Find(buf).GetComponent<Unit>();
                    UnitBufer.Hitpoints = Convert.ToSingle(sr.ReadLine());
                    UnitBufer.Damage = Convert.ToSingle(sr.ReadLine());
                    UnitBufer.CurrentNumberActionPoints = Convert.ToInt32(sr.ReadLine());
                    UnitBufer.StandardNumberActionPoints = Convert.ToInt32(sr.ReadLine());
                }
            }
            sr.Close();
        }
        else
        {
            Debug.Log("File doesn't exist");
        }
    }




    public void ClickOnMenu()
    {
        Save("QuickSave");
        //Destroy(menu);
        Application.LoadLevel("MainMenu");
    }

    public string[] StringToArray(string s)
    {
        int N = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '\n')
                N++;
        }
        string[] sa = new string[N];
        N = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != '\n' && s[i] != '\r')
                sa[N] += s[i];
            if (s[i] == '\n')
                N++;
        }
        return sa;
    }

}

