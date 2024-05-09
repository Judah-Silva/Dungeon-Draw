using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource src;
    public List<AudioClip> clips;

    private int currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        src.loop = true;
        PlayMainMenu();
    }

    private void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != currentIndex)
        {
            switch (currentIndex)
            {
                case 0:
                    PlayMainMenu();
                    break;
                case 1:
                    PlayShop();
                    break;
                case 6:
                    PlayBattle();
                    break;
                case 7:
                    PlayBattle();
                    break;
            }

            if (currentIndex is >= 2 and <= 5 && currentScene is < 2 or > 5)
            {
                PlayFiller();
            }
            currentScene = currentIndex;
        }
    }

    void PlayMainMenu()
    {
        src.clip = clips[0];
        src.Play();
    }

    void PlayShop()
    {
        src.clip = clips[1];
        src.Play();
    }

    void PlayFiller()
    {
        src.clip = clips[2];
        src.Play();
    }

    void PlayBattle()
    {
        src.clip = CombatManager.Instance.IsBoss() ? clips[4] : clips[3];
        
        src.Play();
    }
}
