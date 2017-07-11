using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour
{
    public static ActionButtons actionButtons;
    public GameObject SwordsmanButtonsPanel;
    public GameObject ArcherButtonsPanel;
    public GameObject MageButtonsPanel;
    public GameObject KillerButtonsPanel;
    public GameObject TownHallButtonsPanel;
    public GameObject BarracksButtonsPanel;
    public GameObject PitButtonsPanel;
    public GameObject SawmillButtonsPanel;
    public GameObject CancelActionButton;
    InfoPanel SwordsmanInfoPanel;
    InfoPanel ArcherInfoPanel;
    InfoPanel MageInfoPanel;
    InfoPanel KillerInfoPanel;
    InfoPanel TownHallInfoPanel;
    InfoPanel BarracksInfoPanel;
    InfoPanel PitInfoPanel;
    InfoPanel SawmillInfoPanel;
    InfoPanel CancelInfoPanel;

    void Start()
    {
        actionButtons = GetComponent<ActionButtons>();
        HideAll();
        HideCancelActionButton();
        SwordsmanInfoPanel = SwordsmanButtonsPanel.GetComponentInChildren<InfoPanel>();
        ArcherInfoPanel = ArcherButtonsPanel.GetComponentInChildren<InfoPanel>();
        MageInfoPanel = MageButtonsPanel.GetComponentInChildren<InfoPanel>();
        KillerInfoPanel = KillerButtonsPanel.GetComponentInChildren<InfoPanel>();
        TownHallInfoPanel = TownHallButtonsPanel.GetComponentInChildren<InfoPanel>();
        BarracksInfoPanel = BarracksButtonsPanel.GetComponentInChildren<InfoPanel>();
        PitInfoPanel = PitButtonsPanel.GetComponentInChildren<InfoPanel>();
        SawmillInfoPanel = SawmillButtonsPanel.GetComponentInChildren<InfoPanel>();
        CancelInfoPanel = CancelActionButton.GetComponentInChildren<InfoPanel>();
    }

    public void HideGreen()
    {
        KillerButtonsPanel.SetActive(false);
    }

    public void HideAll()
    {
        SwordsmanButtonsPanel.SetActive(false);
        ArcherButtonsPanel.SetActive(false);
        MageButtonsPanel.SetActive(false);
        KillerButtonsPanel.SetActive(false);
        TownHallButtonsPanel.SetActive(false);
        BarracksButtonsPanel.SetActive(false);
        PitButtonsPanel.SetActive(false);
        SawmillButtonsPanel.SetActive(false);
    }
    public void HideCancelActionButton()
    {
        CancelActionButton.SetActive(false);
    }

    public void ActivateSwordsmanButtonsPanel()
    {
        HideAll();
        SwordsmanButtonsPanel.SetActive(true);
        //SwordsmanInfoPanel.ShowInfo();
    }
    public void ActivateArcherButtonsPanel()
    {
        HideAll();
        ArcherButtonsPanel.SetActive(true);
        //ArcherInfoPanel.ShowInfo();
    }
    public void ActivateMageButtonsPanel()
    { 
        HideAll();
        MageButtonsPanel.SetActive(true);
        //MageInfoPanel.ShowInfo();
    }
    public void ActivateKillerButtonsPanel()
    {
        HideAll();
        KillerButtonsPanel.SetActive(true);
        //KillerInfoPanel.ShowInfo();
    }
    public void ActivateTownHallButtonsPanel()
    {
        HideAll();
        TownHallButtonsPanel.SetActive(true);
        //TownHallInfoPanel.ShowInfo(); 
    }
    public void ActivateBarracksButtonsPanel()
    {
        HideAll();
        BarracksButtonsPanel.SetActive(true);
        //BarracksInfoPanel.ShowInfo();
    }
    public void ActivatePitButtonsPanel()
    {
        HideAll();
        PitButtonsPanel.SetActive(true);
        //PitInfoPanel.ShowInfo();
    }
    public void ActivateSawmillButtonsPanel()
    {
        HideAll();
        SawmillButtonsPanel.SetActive(true);
        //SawmillInfoPanel.ShowInfo();
    }
    public void ActivateCancelActionButton()
    {
        HideAll();
        CancelActionButton.SetActive(true);
        //CancelInfoPanel.ShowInfo();
    }

}
