using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float minutes;
    public float seconds;
    public TextMeshProUGUI timer_minutes;
    public TextMeshProUGUI timer_seconds;

    float currentSeconds = 0f;
    float currentMinutes = 0f;
    bool isCompleted = false;
    static CountdownTimer countdownTimer;

    void Start()
    {
        currentMinutes = minutes;
        currentSeconds = seconds;
        countdownTimer = this;
    }

    public static CountdownTimer getInstance()
    {
        return countdownTimer;
    }

    bool isRunning = true;
    public void StopTimer()
    {
        isRunning = false;
        //try
        //{
        //    FindObjectOfType<PuzzleTilemap>().Clear();
        //}catch { }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning)
            return;

        if(countTo > 0)
        {
            countTo -= 1 * Time.unscaledDeltaTime;
            timer_minutes.color = new Color32(161, 0, 255, 255);
            timer_seconds.color = new Color32(161, 0, 255, 255);

            if (countTo > 0)
            {
                timer_minutes.text = "0";
                timer_seconds.text = Mathf.FloorToInt(countTo).ToString();
            }

            if (countTo > 0 && countTo < 0.5)
            {
                AudioManager.GetInstance().AcelerateSfx();
                FreezeMenu.GetFreezeMenu().UnfreezeScreen();
                Store.GetInstance().NormalizeAnimation();
                MenuController.GetInstance().NormalizeAnimation();
                Player.getInstance().GetPlayerMovement().NormalizeAnimation();
                Time.timeScale = 1;
            }
            return;
        }

        if(timer_minutes.color.Equals(new Color32(161,0,255,255)))
        {
            timer_minutes.color = new Color32(161, 161, 161, 255);
            timer_seconds.color = new Color32(161, 161, 161, 255);
        }

        if (isCompleted)
        {
            GameManager.GetInstance().GameOver("nova_aerea");
            return;
        }

        currentSeconds -= 1 * Time.unscaledDeltaTime;
        int sec = Mathf.FloorToInt(currentSeconds);

        if (currentSeconds <= 0 && currentMinutes == 0)
        {
            isCompleted = true;
            return;
        }

        timer_minutes.text = (currentMinutes>0?"0":"")+currentMinutes;
        timer_seconds.text = (sec > 9?sec +"":("0"+sec));

        if (currentSeconds <= 0 && currentMinutes != 0)
        {
            currentSeconds = 60;
            currentMinutes--;
        }
    }

    float countTo = 0;
    public void CountTo(int seconds)
    {
        countTo = ++seconds;
    }
}