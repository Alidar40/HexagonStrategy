using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public ActionButtons acb;
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);       
    }

    void TaskOnClick()
    {
        Debug.Log("test");//
        //acb.HideAll();
        //собственно, в этом кейсе мы узнаем для какого юнита кнопка была и вызываем нужную функцию
        //тег можно доставать еще и из активного юнита(он совпадает с тегом кнопок действия(если нет багов o_O )
        switch(gameObject.tag)
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


    }

  //далее идут функции вызывающие функции действий юнитов(и строений)

    void MageButtons() // Mage's Buttons типо
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
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
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
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
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
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
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
                break;
        }
    }
    void TownHallButtons() 
    {
        switch (gameObject.name)
        {
            case "Action1":
                //первая кнопка
                acb.HideAll();
                break;
            case "Action2":
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
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
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
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
            case "Action2":
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
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
            case "Action2":
                //вторая кнопка
                acb.ActivateArcherButtonsPanel();
                break;
            case "Action3":
                // какая кнопка? 
                acb.ActivatePitButtonsPanel();
                break;
            case "Action4":
                //последняя О_о кнопка
                acb.ActivateTownHallButtonsPanel();
                break;
        }
    }





}
