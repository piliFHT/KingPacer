using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float originalTimeScale;
    private float startTime; // Variable to store the start time
    private float elapsedTime; // Variable to store the elapsed time
    public GameObject pauseCan;
    public bool IsGamePaused;

    private void Awake()
    {
        originalTimeScale = Time.timeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseCan.SetActive(false);
        startTime = Time.time; // Initialize the start time
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGamePaused)
        {
            elapsedTime = Time.time - startTime;
        }
        UpdateTimeText();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseCan.SetActive(true);
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseCan.SetActive(false);
        IsGamePaused = false;

        if (IsGamePaused)
        {
            startTime += Time.realtimeSinceStartup - Time.time;
        }
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

            string timeString = string.Format(
                "{0:D2}:{1:D2}.{2:D3}",
                minutes,
                seconds,
                milliseconds
            );

            timeText.text = $"Time: {timeString}";
        }
    }
}
