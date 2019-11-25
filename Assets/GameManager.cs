using System;
using System.Collections;
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

    int currentSceneIndex = 2;    

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
            CompletedLevels = currentSceneIndex - 1;
            Player.getInstance().spawnsDistribution = (playerData.scenes == null ? new int[(int)LevelQuantity] : playerData.scenes);
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

    IEnumerator SceneTransitor()
    {
        TransitionAnimation.getInstance().EndAnim();
        yield return new WaitForSeconds(0.8f);
        LoadScene("bomb-loading_scene");
    }

    public void StartGame()
    {
        AudioManager.GetInstance().Stop("bgm-ada_theme");
        SceneTransition.indextoBuild = currentSceneIndex;
        StartCoroutine(SceneTransitor());
        //TransitionAnimation.getInstance().EndAnim();
        //LoadScene("bomb-loading_scene");
    }

    public void RestartGame()
    {
        IOManager.ResetData();
        currentSceneIndex = 2;
        CompletedLevels = 0;
        LoadScene(0);
        SceneTransition.indextoBuild = 1;
    }

    public int EvaluateDiscount()
    {
        float discount = maximunDiscount *(CompletedLevels / LevelQuantity) + minimunDiscount;
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
        SceneTransition.indextoBuild = currentSceneIndex;
        //transitionAnim.SetTrigger("end");
        //SceneManager.LoadSceneAsync("bomb-loading_scene");
        StartCoroutine(SceneTransitor());
    }

    public void RebuildCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}