﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    bool isLevelOver = false;

    public int minimunDiscount = 3;
    public float maximunDiscount = 17;
    public float LevelQuantity = 0;
    public float CompletedLevels = 0;
    int currentSceneIndex = 1;

    PlayerData playerData;

    void Start()
    {
        if (gameManager == null)
            gameManager = this;

        DontDestroyOnLoad(this);

        try
        {
            playerData = IOManager.RetriveData();
            currentSceneIndex = playerData.scenePlayerStopped == 0? 1: playerData.scenePlayerStopped;
            CompletedLevels = currentSceneIndex;
        }
        catch {
            
        }
    }
    
    public static GameManager GetInstance()
    {
        return gameManager;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GameOver(string scene_name)
    {
        LoadScene(scene_name);
    }

    public bool IsLevelOver()
    {
        return isLevelOver;
    }

    public void EndLevel()
    {
        isLevelOver = true;
    }

    public void StartGame()
    {
        AudioManager.GetInstance().Stop("bgm-ada_theme");
        LoadScene(currentSceneIndex);
    }

    public void RestartGame()
    {
        IOManager.ResetData();
        currentSceneIndex = 1;
        CompletedLevels = 1;
        LoadScene(0);
    }

    public int EvaluateDiscount()
    {
        float discount = maximunDiscount *(CompletedLevels / LevelQuantity) + minimunDiscount;
        Debug.Log(currentSceneIndex);
        return Mathf.RoundToInt(discount);
    }
    
    public int incrementSceneIndex()
    {
        return ++currentSceneIndex;
    }

    public int getCurrentSceneIndex()
    {
        return currentSceneIndex;
    }

    public void BuildNextScene()
    {
        CompletedLevels++;
        currentSceneIndex++;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
}