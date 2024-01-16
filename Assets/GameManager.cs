using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform playerRespawnPoint;
    public int playerHealth = 10;
    public int damage = 10; // Player's health
    public TextMeshProUGUI healthText;
    public brut Brut;
    private Vector3 initialPlayerPosition;
    public TimeManager timeManager;
    public GameObject gameOver;


    private void Start()
    {
        initialPlayerPosition = player.transform.position;
        UpdateHealthText();
        gameOver.SetActive(false);
        
    }

    public void RespawnPlayer()
    {
        player.transform.position = playerRespawnPoint.position;
    }

    public void ApplyDamage()
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
           HandleGameOver();
        }

        UpdateHealthText(); // Update the health display
    }

    // Helper function to update the health display
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {playerHealth}";
            //Debug.Log("Health updated: " + playerHealth);
        }
    }


    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // You can use any key for resuming the game
        {
            if(timeManager.IsGamePaused){
            timeManager.ResumeGame();
           

            }
            else{
                
                timeManager.PauseGame();
            }
        }
        // if(playerHealth <= 0){
        //     timeManager.PauseGame();
        // }
    }

     public void HandleGameOver()
    {
        gameOver.SetActive(true);

        Time.timeScale = 0f;
        timeManager.IsGamePaused = true;
        //SceneManager.LoadScene("GameOverScreen");
    }
}
