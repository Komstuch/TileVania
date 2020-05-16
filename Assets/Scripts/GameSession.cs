﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] GameObject deathCanvas;

    private void Awake()
    {   //Singleton implementation
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        Instantiate(deathCanvas);
        //SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}