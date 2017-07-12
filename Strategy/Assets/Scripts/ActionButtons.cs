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


    void Start()
    {
        actionButtons = GetComponent<ActionButtons>();
        HideAll();
        HideCancelActionButton();

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
    }
    public void ActivateArcherButtonsPanel()
    {
        HideAll();
        ArcherButtonsPanel.SetActive(true);
    }
    public void ActivateMageButtonsPanel()
    { 
        HideAll();
        MageButtonsPanel.SetActive(true);
    }
    public void ActivateKillerButtonsPanel()
    {
        HideAll();
        KillerButtonsPanel.SetActive(true);
    }
    public void ActivateTownHallButtonsPanel()
    {
        HideAll();
        TownHallButtonsPanel.SetActive(true);
    }
    public void ActivateBarracksButtonsPanel()
    {
        HideAll();
        BarracksButtonsPanel.SetActive(true);
    }
    public void ActivatePitButtonsPanel()
    {
        HideAll();
        PitButtonsPanel.SetActive(true);
    }
    public void ActivateSawmillButtonsPanel()
    {
        HideAll();
        SawmillButtonsPanel.SetActive(true);
    }
    public void ActivateCancelActionButton()
    {
        HideAll();
        CancelActionButton.SetActive(true);
    }

}
