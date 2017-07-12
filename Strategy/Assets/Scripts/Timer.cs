using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    public static Timer timObject;
    public float timer;//время таймера
    public bool timerOn;
    public Text timerText;

    public void Start()
    {
       timObject = GetComponent<Timer>();
       StartTimer();
    }

    public void Update () {
        if (timerOn)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                ShowTime();
            }
            if (timer < 0)
            {
                timerOn = false;
                if (GameObject.Find("Map").GetComponent<Map>().ActivePlayer)
                {
                    GameObject.Find("GameEvents").GetComponent<GameEvents>().NextTurn();
                    GameObject.Find("Map").GetComponent<ArtificialIntelligence>().BasicAlgorithm();
                    StartTimer();
                }
                else
                {
                    GameObject.Find("Map").GetComponent<Map>().ActivePlayer = true;
                    StartTimer();
                }
            }
        }
	}

    public void StartTimer()
    {
        timer = 60.0f;
        timerOn = true;
    }


    public void ShowTime()
    {
        timerText.text = ((int) timer).ToString();
    }
}
