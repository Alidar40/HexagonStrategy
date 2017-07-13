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
        Application.LoadLevel("TestScen");
        _State = State.NewGame;
    }

    public void LoadGame()
    {
        Application.LoadLevel("TestScen");
        _State = State.Load;
    }
}
