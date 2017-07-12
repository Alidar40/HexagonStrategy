using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    public void Singleplayer()
    {
        SceneManager.LoadScene("TestScen", LoadSceneMode.Single);
    }
}