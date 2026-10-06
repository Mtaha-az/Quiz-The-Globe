using UnityEngine;
using UnityEngine.UI;

public class SurvivalModeManager : MonoBehaviour
{
    public Image[] LifelineImages; // Array to hold the 3 lifeline images
    private FlagManager flagManager; // Reference to the FlagManager (for accessing game over and correct/incorrect answer display)

    private int lifelinesRemaining = 3; // Start with 3 lifelines
    private int correctAnswers = 0; // Track correct answers
    private int incorrectAnswers = 0; // Track incorrect answers
    private bool gameOver = false; // Track if the game is over

    void Start()
    {
        // Check if this is Survival Mode, otherwise disable this script
        if (GameModeMenu.selectedMode != "Survival")
        {
            this.enabled = false;  // Disable the script entirely if it's not Survival Mode
            return;
        }

        // Find the FlagManager in the scene to handle game over and score management
        flagManager = FindObjectOfType<FlagManager>();

        // Hide TimerText in Survival Mode (if FlagManager has it)
        if (flagManager != null && flagManager.TimerText != null)
        {
            flagManager.TimerText.gameObject.SetActive(false); // Hide timer text in Survival Mode
        }

        ShowLifelineImages(); // Show lifelines (used in Survival Mode)
    }

    // Show the lifeline images in Survival Mode
    private void ShowLifelineImages()
    {
        foreach (Image lifeline in LifelineImages)
        {
            lifeline.gameObject.SetActive(true); // Show lifeline images in Survival Mode
        }
    }

    // Update survival mode logic based on the correctness of the answer
    // SurvivalModeManager.cs
    public void UpdateSurvivalMode(bool correctAnswer)
    {
        if (gameOver) return; // Don't update if the game is over

        if (correctAnswer)
        {
            correctAnswers++; // Increment correct answer count

            // Check if all questions have been answered
            CurrentGameData gameData = FindObjectOfType<CurrentGameData>(); // Access CurrentGameData
            if (gameData.GetNumberOfFlagsLeft() == 0)
            {
                HandleGameOver(true); // Trigger game over if all questions are answered
            }
        }
        else
        {
            incorrectAnswers++; // Increment wrong answer count
            LoseLifeline(); // Lose a lifeline for a wrong answer
        }

        // If the game is over after losing all lifelines
        if (gameOver)
        {
            HandleGameOver(correctAnswer); // Pass the last question's answer (whether it was correct or not)
        }
    }

    // Logic to handle losing a lifeline
    private void LoseLifeline()
    {
        lifelinesRemaining--; // Decrease remaining lifelines

        // Log a message indicating a lifeline was lost and how many are remaining
        Debug.Log("Lifeline lost. Remaining lifelines: " + lifelinesRemaining);

        UpdateLifelinesUI(); // Update the lifeline images

        if (lifelinesRemaining <= 0)
        {
            gameOver = true; // Set game over if no lifelines remain
            Debug.Log("Game Over: All lifelines used."); // Log that the game is over because all lifelines are used
        }
    }

    // Update the lifeline images to reflect the current number of lifelines remaining
    private void UpdateLifelinesUI()
    {
        for (int i = 0; i < LifelineImages.Length; i++)
        {
            if (i < lifelinesRemaining)
            {
                LifelineImages[i].enabled = true; // Show active lifeline
            }
            else
            {
                LifelineImages[i].enabled = false; // Hide used lifeline
            }
        }
    }

    // Handle game over logic for Survival Mode
    private void HandleGameOver(bool isCorrectAnswer)
    {
        gameOver = true;

        // Log a message indicating the game is over and display the score
        Debug.Log("Survival Mode: Game Over! Correct: " + correctAnswers + ", Incorrect: " + incorrectAnswers);

        // Call FlagManager's HandleGameOver to display GameOverPanel
        if (flagManager != null)
        {
            flagManager.HandleGameOver(isCorrectAnswer); // Pass whether the last answer was correct or incorrect
            // Update the correct and incorrect answers on the Game Over Panel
            flagManager.CorrectAnswersText.text = "Correct Answers: " + correctAnswers;
            flagManager.IncorrectAnswersText.text = "Incorrect Answers: " + incorrectAnswers;
        }
    }
}
