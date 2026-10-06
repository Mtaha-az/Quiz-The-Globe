using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeTrialManager : MonoBehaviour
{
    private FlagManager flagManager; // Reference to the FlagManager
    private float countdownTime = 10f; // Start with 10 seconds
    private bool gameOver = false;

    public Image[] lifelineImages; // Array to hold references to lifeline images (from the scene)

    private int correctAnswers = 0; // Track correct answers
    private int wrongAnswers = 0; // Track wrong answers

    void Start()
    {
        // Find the FlagManager in the scene to access TimerText
        flagManager = FindObjectOfType<FlagManager>();

        // Hide lifeline images in Time Trial Mode
        HideLifelineImages();

        // Ensure TimerText is visible in Time Trial Mode
        if (flagManager != null)
        {
            flagManager.TimerText.gameObject.SetActive(true); // Show timer in Time Trial Mode
        }

        StartCoroutine(CountdownTimer()); // Start countdown timer for Time Trial Mode
    }

    // Countdown timer logic for Time Trial Mode
    IEnumerator CountdownTimer()
    {
        while (countdownTime > 0 && !gameOver)
        {
            yield return new WaitForSeconds(1f);
            countdownTime -= 1f; // Decrease time by 1 second

            if (flagManager != null)
            {
                flagManager.TimerText.text = countdownTime.ToString("F2"); // Update timer text in FlagManager
            }

            if (countdownTime <= 0)
            {
                HandleGameOver(); // Trigger game over when the timer hits zero
            }
        }
    }

    // Update the timer based on the correctness of the answer in Time Trial Mode
    // TimeTrialManager.cs
    public void UpdateTimer(bool correctAnswer) // Changed from private to public
    {
        if (gameOver) return; // Don't update if the game is over

        if (correctAnswer)
        {
            correctAnswers++; // Increment correct answer count
            countdownTime += 4f; // Add 4 seconds for a correct answer

            // Check if all questions have been answered
            CurrentGameData gameData = FindObjectOfType<CurrentGameData>();
            if (gameData.GetNumberOfFlagsLeft() == 0)
            {
                HandleGameOver(); // Trigger game over if all questions are answered
            }
        }
        else
        {
            wrongAnswers++; // Increment wrong answer count
            countdownTime -= 2f; // Subtract 2 seconds for a wrong answer
            if (countdownTime < 0) countdownTime = 0; // Ensure countdown doesn't go below zero
        }

        // Update the timer text in FlagManager
        if (flagManager != null)
        {
            flagManager.TimerText.text = countdownTime.ToString("F2");
        }
    }

    // Game over logic for Time Trial Mode
    private void HandleGameOver()
    {
        if (gameOver) return; // Prevent multiple game over executions
        gameOver = true;

        if (flagManager != null)
        {
            flagManager.TimerText.text = "0.00"; // Set the timer to 0
        }

        // Show the game over panel in FlagManager
        flagManager.HandleGameOver(false);

        // Update the correct and incorrect answer counts in the Game Over panel's text
        flagManager.CorrectAnswersText.text = "Correct Answers: " + correctAnswers;
        flagManager.IncorrectAnswersText.text = "Wrong Answers: " + wrongAnswers;

        // Additional game-over logic (e.g., stopping gameplay)
        Debug.Log("Time Trial Mode: Game Over!");
    }

    // Hide the lifeline images when Time Trial Mode is selected
    private void HideLifelineImages()
    {
        if (lifelineImages != null && lifelineImages.Length > 0)
        {
            foreach (Image lifeline in lifelineImages)
            {
                lifeline.gameObject.SetActive(false); // Hide each lifeline image
            }
        }
    }
}
