using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject controlsCan;


    void Start(){
        controlsCan.SetActive(false);
    }

    // int n = 0;
    public void RestartGame()
    {
        // n++;
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LevSel1(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    public void PlayGame(){
        SceneManager.LoadScene("LevelSelect");
    }
    public void ExitGame(){
        Application.Quit();
        Debug.Log("Game is Closing!");
    }
    public void Level1(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelOne");
    }
    public void ControlsOpen(){
        controlsCan.SetActive(true);
    }
    public void ControlsClose(){
        controlsCan.SetActive(false);
    }
}
