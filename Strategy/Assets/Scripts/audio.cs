using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{


    public static audio Instance { get; private set; }


    public AudioClip impact; // наш звук

    AudioSource audioSource;// объявляем компонент аудио источника

    //public bool play=false;
    public bool pause = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAudio(impact);
    }

    void Update()
    {
        if (!audioSource.isPlaying)//рестартит при остановке
            PlayAudio(impact);

        if (Input.GetKey("p")) 
        {
            Instance.Pause();
        }

        if (Input.GetKey("=")) 
        {
            SetVolume(0.9f);
        }
        if (Input.GetKey("-"))
        { 
            SetVolume(0.2f);
        }

        Awake();
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            pause = true;
        }
        else
        {
            audioSource.UnPause();
            pause = false;
        }

    }

    public void MuteAudio()
    {

        if (audioSource.mute)
            audioSource.mute = false;
        else
            audioSource.mute = true;
    }

  public void SetVolume(float Rate)//настройка звука (от 0.00 до 1.00)
    {
        audioSource.volume = Rate;

    }

   public void Awake() //эта лютая штука сохряняет музыку при переходе между сценами
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        
        Instance = this;
       DontDestroyOnLoad(gameObject);
    }

    public void PlayAudio(AudioClip impact) //запускает аудио(или останавливает)
    {
        if (!pause)
        {
            if (!audioSource.isPlaying)
            {
                audioSource = GetComponent<AudioSource>();
                // Воспроизводим
                audioSource.PlayOneShot(impact, 0.7F);
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    



}


