using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    MapLoader ml;
    public enum State {NewGame, Load};
    public State _State;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Singleplayer()
    {
<<<<<<< HEAD
        Application.LoadLevel("SelectMap");
=======
        Application.LoadLevel("TestScen");
>>>>>>> 5dab9b106bca04a0f9dfc063084d01c3532e43a4
        _State = State.NewGame;
    }

    public void LoadGame()
    {
        Application.LoadLevel("TestScen");
        _State = State.Load;
    }

    public void OpenMenu()
    {
        Application.LoadLevel("MainMenu");
        _State = State.Load;
    }
    public void OpenCreators()
    {
        Application.LoadLevel("Creators");
        _State = State.Load;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
