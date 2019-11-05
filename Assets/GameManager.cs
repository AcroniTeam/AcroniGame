using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    bool isLevelOver = false;

    public int minimunDiscount = 3;
    public int maximunDiscount = 17;
    public int LevelQuantity = 0;
    public int CompletedLevels = 0;
    int currentSceneIndex = 1;

    static PlayerData playerData;

    void Start()
    {
        if (gameManager == null)
            gameManager = this;

        DontDestroyOnLoad(this);

        try
        {
            playerData = IOManager.RetriveData();
            currentSceneIndex = playerData.scenePlayerStopped;
            CompletedLevels = currentSceneIndex;
        }
        catch { }
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

    public int EvaluateDiscount()
    {
        return (CompletedLevels/LevelQuantity)*maximunDiscount + minimunDiscount;
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
        SceneManager.LoadSceneAsync(incrementSceneIndex());
    }
}