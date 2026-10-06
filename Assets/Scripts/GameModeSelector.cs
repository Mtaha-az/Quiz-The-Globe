using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameModeMenu : MonoBehaviour
{
    public PickMenuCanvasManger SelectDevice;
    // Static field to store the selected mode
    public static string selectedMode;

    // Function to start Time Trial mode
    public void StartTimeTrialMode()
    {
        selectedMode = "TimeTrial"; // Store the selected mode
        if (SelectDevice.isMobile()) 
        {
        SceneManager.LoadScene("Game"); // Load the mobile Game scene
        }
        else
        {
            SceneManager.LoadScene("Game 1"); // Load the web Game scene
        }
    }

    // Function to start Survival mode
    public void StartSurvivalMode()
    {
        selectedMode = "Survival"; // Store the selected mode
        if (SelectDevice.isMobile())
        {
            SceneManager.LoadScene("Game"); // Load the mobile Game scene
        }
        else
        {
            SceneManager.LoadScene("Game 1"); // Load the web Game scene
        }
    }
}
