using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public static Timer timObject;
    public float timer;//время таймера
    public bool timerOn;

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
            }
            if (timer < 0)
            {
                timerOn = false;
                if (GameObject.Find("Map").GetComponent<Map>().ActivePlayer)
                {
                    GameObject.Find("GameEvents").GetComponent<GameEvents>().NextTurn();
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
        timer = 30.0f;
        timerOn = true;
    }
}
