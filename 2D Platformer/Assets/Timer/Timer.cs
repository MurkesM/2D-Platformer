using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public decimal currentTime = 0;
    public bool updateCurrentTime = true;

    decimal end_game_time = 0;

    float best_time = 0;
    string best_time_key = "Best Time";

    public TextMeshProUGUI bestTimeTMP;
    public TextMeshProUGUI currentTimeTMP;

    void Awake()
    {
        PlayerControls.PlayerKilled += PlayerKilled;
        PlayerControls.PlayerFinishedGame += PlayerFinishedGame;

        best_time = PlayerPrefs.GetFloat(best_time_key);
        bestTimeTMP.SetText($"Best Time: {Decimal.Round((decimal)best_time, 2)}");
    }

    void Update()
    {
        //get current time since start
        if (updateCurrentTime)
            currentTime = (decimal)Time.realtimeSinceStartup;

        //update UI
        if (currentTimeTMP)
            currentTimeTMP.SetText($"Time: {Decimal.Round(currentTime, 2)}");
    }

    void OnDestroy()
    {
        PlayerControls.PlayerKilled -= PlayerKilled;
        PlayerControls.PlayerFinishedGame -= PlayerFinishedGame;
    }

    public void PlayerKilled()
    {
        updateCurrentTime = false;

        end_game_time = currentTime;
    }

    public void PlayerFinishedGame()
    {
        updateCurrentTime = false;

        end_game_time = currentTime;

        if ((float)end_game_time < best_time)
        {
            PlayerPrefs.SetFloat(best_time_key, (float)end_game_time);
            PlayerPrefs.Save();
        }
    }
}