using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour {

    Menu menu;

    void Start () {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
    }

    public void ClickOnCancel()
    {
        menu.OpenMenu();
        Destroy(menu.gameObject);
    }
}
