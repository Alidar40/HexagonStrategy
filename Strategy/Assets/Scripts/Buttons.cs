using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public ActionButtons acb;
    public Button yourButton;
    GameCamera gameCamera;
    static Map map;
    Text bufText;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        map = GameObject.Find("Map").GetComponent<Map>();
        gameCamera = GameObject.Find("Main Camera").GetComponent<GameCamera>();
        btn.onClick.AddListener(TaskOnClick);       
    }

    private void Update()
    {
        if (map.ActiveUnit.Type == Unit.UnitType.Mage)
        {
            bufText = GameObject.Find("MageButtonsPanel/UnitActionWindow/Action3/Text").GetComponent<Text>();
            switch (map.ActiveUnit.Fraction)
            {
                case 1:
                    bufText.text = "Heal";
                    break;
                case 2:
                    bufText.text = "Fireball";
                    break;
            }
            bufText = GameObject.Find("MageButtonsPanel/UnitActionWindow/Action4/Text").GetComponent<Text>();
            switch (map.ActiveUnit.Fraction)
            {
                case 1:
                    bufText.text = "Gain Defence";
                    break;
                case 2:
                    bufText.text = "Weaken Defence";
                    break;
            }
        }

        if (map.ActiveUnit.Type == Unit.UnitType.Killer)
        {
            bufText = GameObject.Find("KillerButtonsPanel/UnitActionWindow/Action5/Text").GetComponent<Text>();
            switch (map.ActiveUnit.Fraction)
            {
                case 1:
                    bufText.text = "Ignore Defence";
                    break;
                case 2:
                    bufText.text = "Poison";
                    break;
            }
        }


    }

    void TaskOnClick()
    {
        //собственно, в этом кейсе мы узнаем для какого юнита кнопка была и вызываем нужную функцию
        //тег можно доставать еще и из активного юнита(он совпадает с тегом кнопок действия(если нет багов o_O )
        if (GameObject.Find("Map").GetComponent<Map>().ActivePlayer)
        {
            switch (gameObject.tag)
            {
                case "Swordsman":
                    //тег мечника( да, знаю, что это фехтовальщик...гугл не захотел переводить по-другому)
                    SwordsmanButtons();
                    break;
                case "Archer":
                    ArcherButtons();
                    break;
                case "Mage":
                    MageButtons();
                    break;
                case "Killer":
                    KillerButtons();
                    break;
                case "TownHall":
                    TownHallButtons();
                    break;
                case "Barracks":
                    BarracksButtons();
                    break;
                case "Pit":
                    PitButtons();
                    break;
                case "Sawmill":
                    SawmillButtons();
                    break;
            }
            //if (map.ActiveUnit.CurrentNumberActionPoints > 0)
            //    acb.HideAll();
        }
    }

  //далее идут функции вызывающие функции действий юнитов(и строений)
  //Первая кнопка по умолчанию является кнопкой закрытия меню
  //Вторая кнопка для юнитов, кнопка перемещения

    void MageButtons() // Mage's Buttons типо
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartMovingUnit();
                break;
            case "Action3":
                switch (map.ActiveUnit.Fraction)
                {
                    case 1:
                        if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                        {
                            gameCamera.COSevent -= map.callMenu;
                            gameCamera.COSevent += Heal_COSevent;
                            ActionButtons.actionButtons.ActivateCancelActionButton();
                            map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.AttackRadius);
                        }
                        break;
                    case 2:
                        if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                        {
                            gameCamera.COSevent -= map.callMenu;
                            gameCamera.COSevent += FB_COSevent;
                            ActionButtons.actionButtons.ActivateCancelActionButton();
                            map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.AttackRadius);
                        }
                        break;
                }
                break;
            case "Action4":
                


                break;
        }
    }


    void SwordsmanButtons()
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartMovingUnit();
                break;
            case "Action3":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartAttackUnit();
                break;
        }
    }

    void ArcherButtons() 
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartMovingUnit();
                break;
            case "Action3":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartAttackUnit();
                break;
            case "Action4":
                
                break;
        }
    }

    void KillerButtons()
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartMovingUnit();
                break;
            case "Action3":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                    gameCamera.StartAttackUnit();
                break;
            case "Action4":
                
                break;
        }
    }

    void TownHallButtons() 
    {
        switch (gameObject.name)
        {
            case "Action1":
                acb.HideAll();
                break;
            case "Action2":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {

                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += Barracks_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += BarracksResourcesDecrease;
                }
                break;
            case "Action3":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {

                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += Pit_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += PitResourcesDecrease;
                }
                break;
            case "Action4":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {

                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += SawMill_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += SawmillResourcesDecrease;
                }
                break;
        }
    }

    void BarracksButtons() 
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {
                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += SwordsmanSpawn_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += SwordsmanResourcesDecrease;
                }
                break;
            case "Action3":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {
                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += ArcherSpawn_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += ArcherResourcesDecrease;
                }
                break;
            case "Action4":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {
                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += MageSpawn_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += MageResourcesDecrease;
                }
                break;
            case "Action5":
                if (map.ActiveUnit.CurrentNumberActionPoints > 0)
                {
                    gameCamera.COSevent -= map.callMenu;
                    gameCamera.COSevent += KillerSpawn_COSevent;
                    ActionButtons.actionButtons.ActivateCancelActionButton();
                    map.ActiveUnit.GenerateFieldOpportunities(gameCamera.FieldOpportunitiesAttack, map.ActiveUnit.BuildingRadius);
                    RDevent += BarracksResourcesDecrease;
                }
                break;
        }
    }

    void PitButtons() 
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
        }
    }
    void SawmillButtons() 
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
        }
    }

    private void Barracks_COSevent()  //Это функцию мы вызываем вместо callMenu в PointClick() при создании бараков
    {
        Construction.CreateConstructionOnClick(map.UnitPrefabArray[5], Construction.ConstructionType.Barracks, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;       //Возвращаем все
        gameCamera.COSevent -= Barracks_COSevent;   //на исходные места
        RDevent -= BarracksResourcesDecrease;
    }

    private void Pit_COSevent()
    {
        Construction.CreateConstructionOnClick(map.UnitPrefabArray[6], Construction.ConstructionType.Pit, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= Pit_COSevent;
        RDevent -= PitResourcesDecrease;
    }

    private void SawMill_COSevent()
    {
        Construction.CreateConstructionOnClick(map.UnitPrefabArray[7], Construction.ConstructionType.Sawmill, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= SawMill_COSevent;
        RDevent -= SawmillResourcesDecrease;
    }

    private void KillerSpawn_COSevent()
    {
        Unit.CreateUnitOnClick(map.UnitPrefabArray[3], Unit.UnitType.Killer, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= KillerSpawn_COSevent;
        RDevent -= KillerResourcesDecrease;
    }

    private void MageSpawn_COSevent()
    {
        Unit.CreateUnitOnClick(map.UnitPrefabArray[2], Unit.UnitType.Mage, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= MageSpawn_COSevent;
        RDevent -= MageResourcesDecrease;
    }

    private void ArcherSpawn_COSevent()
    {
        Unit.CreateUnitOnClick(map.UnitPrefabArray[1], Unit.UnitType.Archer, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= ArcherSpawn_COSevent;
        RDevent -= ArcherResourcesDecrease;
    }

    private void SwordsmanSpawn_COSevent()
    {
        Unit.CreateUnitOnClick(map.UnitPrefabArray[0], Unit.UnitType.Swordsman, map.UnitList, map.ActiveUnit.GetComponent<Construction>().CurrentCell);
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= SwordsmanSpawn_COSevent;
        RDevent -= SwordsmanResourcesDecrease;
    }

    private void FB_COSevent()
    {
        map.ActiveUnit.ThrowFireball();
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= FB_COSevent;
    }

    private void Heal_COSevent()
    {
        map.ActiveUnit.HealOnClick();
        gameCamera.COSevent += map.callMenu;
        gameCamera.COSevent -= Heal_COSevent;
    }

    private static void BarracksResourcesDecrease()
    {
        map.Gold -= 30;
        map.Stone -= 20;
        map.Wood -= 20;
    }
    private static void PitResourcesDecrease()
    {
        map.Wood -= 30;
        map.Gold -= 20;
        RDevent -= PitResourcesDecrease;
    }
    private static void SawmillResourcesDecrease()
    {
        map.Wood -= 15;
        map.Gold -= 10;
        RDevent -= SawmillResourcesDecrease;
    }
    private static void SwordsmanResourcesDecrease()
    {
        map.Gold -= 5;
        RDevent -= SwordsmanResourcesDecrease;
    }
    private static void ArcherResourcesDecrease()
    {
        map.Gold -= 7;
        RDevent -= ArcherResourcesDecrease;
    }
    private static void MageResourcesDecrease()
    {
        map.Gold -= 10;
        RDevent -= MageResourcesDecrease;
    }
    private static void KillerResourcesDecrease()
    {
        map.Gold -= 10;
        RDevent -= KillerResourcesDecrease;
    }

    public static void UnsubscribeAllDecreases()
    {
        RDevent -= BarracksResourcesDecrease;
        RDevent -= PitResourcesDecrease;
        RDevent -= SawmillResourcesDecrease;
        RDevent -= SwordsmanResourcesDecrease;
        RDevent -= ArcherResourcesDecrease;
        RDevent -= MageResourcesDecrease;
        RDevent -= KillerResourcesDecrease;

    }
    public delegate void ResourcesDecreaseDelegate();
    public static event ResourcesDecreaseDelegate RDevent;

    public static void ResourcesDecrease()
    {
        RDevent();
    }

}
