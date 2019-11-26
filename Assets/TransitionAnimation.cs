using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAnimation : MonoBehaviour
{
    static TransitionAnimation transition;
    public Animator transitionAnim;

    private void Awake()
    {
        transition = this;
    }

    public static TransitionAnimation getInstance()
    {
        return transition;
    }

    void Start()
    {
        DontDestroyOnLoad(transition);
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "bomb-loading_scene" && scene.name != "Menu_principal")
            transitionAnim.SetTrigger("start");
    }
    
    public void EndAnim()
    {
        transitionAnim.SetTrigger("end");
    }
}
