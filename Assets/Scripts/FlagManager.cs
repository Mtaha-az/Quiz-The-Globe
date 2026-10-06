using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{
    public GameObject[] FlagsObjects; // Array of four flag GameObjects
    public Text DisplayedText; // UI Text for displaying flag names
    public Text TimerText; // UI Text for displaying the timer (used in Time Trial mode)
    public GameObject GameOverPanel; // The panel that shows correct/incorrect answers
    public Text CorrectAnswersText; // Text to display correct answers count
    public Text IncorrectAnswersText; // Text to display incorrect answers count

    private int NumberOfFlagsObjects = 0;
    public bool gameOver = false; // Track game over status

    private int correctAnswers = 0; // Track correct answers
    private int incorrectAnswers = 0; // Track incorrect answers
    private CurrentGameData m_GameData;
    private bool IsFirstRun = false;

    // Reference to ScoreManager to update the questioned flags count
    private ScoreManager scoreManager;

    void Start()
    {
        NumberOfFlagsObjects = FlagsObjects.Length; // Initialize number of flag objects
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>(); // Reference to CurrentGameData
        scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager in the scene
        IsFirstRun = true;

        GameOverPanel.SetActive(false); // Hide the panel initially
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFirstRun)
        {
            FirstRun();
        }

        // If the game is over, stop further updates
        if (gameOver)
        {
            return;
        }
    }

    void FirstRun()
    {
        AssignFlags();
        StartCoroutine(LoopRotation(90f)); // Start rotation coroutine
        UpdateDisplayedText();
        IsFirstRun = false; // Set first run to false after assigning flags
    }

    public void LoadNextGame()
    {
        if (gameOver) return; // Prevent loading the next game if it's over

        StartCoroutine(LoopRotation(90f)); // Start rotation coroutine for next game
        m_GameData.GetNewCountries(); // Get new countries
        UpdateDisplayedText(); // Update the displayed text with the new flag name

        // Increment the questioned flags count every time a new set of flags is loaded
        scoreManager.IncrementQuestionedFlags();
    }

    // This method is called from TimeTrialManager or SurvivalModeManager to update the score and display Game Over
    public void UpdateGameOverState(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
        {
            correctAnswers++; // Increment correct answer count
        }
        else
        {
            incorrectAnswers++; // Increment incorrect answer count
        }

        // Call the score incrementer only if the game is not over
        if (!gameOver)
        {
            scoreManager.IncrementQuestionedFlags();
        }
    }

    public void HandleGameOver(bool isCorrectAnswer)
    {
        // Update the score for the last question
        UpdateGameOverState(isCorrectAnswer);

        gameOver = true;

        // Save the best score for the current mode and continent
        string continent = GameSettings.Instance.GetSelectedContinentName();
        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.E_TIME_TRIAL)
        {
            ProgressManager.SaveTimeTrialBestScore(continent, correctAnswers);
        }
        else if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.E_SURVIVAL)
        {
            ProgressManager.SaveSurvivalBestScore(continent, correctAnswers);
        }

        // Display the game over panel
        GameOverPanel.SetActive(true);

        // Update the text with correct and incorrect answers
        CorrectAnswersText.text = "Correct Answers: " + correctAnswers;
        IncorrectAnswersText.text = "Incorrect Answers: " + incorrectAnswers;

        Debug.Log("Game Over");
    }

    // Assign flags to the flag objects
    public void AssignFlags()
    {
        if (gameOver) return; // Prevent assigning flags if the game is over

        int FinalFlagPosition = (int)Random.Range(0, NumberOfFlagsObjects); // Random flag position

        switch (FinalFlagPosition)
        {
            case 0:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetThirdFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[3].GetComponent<Flag>().SetFlagIndex(m_GameData.GetThirdFlagIndex());
                break;
            case 1:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetThirdFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[3].GetComponent<Flag>().SetFlagIndex(m_GameData.GetThirdFlagIndex());
                break;
            case 2:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetThirdFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[3].GetComponent<Flag>().SetFlagIndex(m_GameData.GetThirdFlagIndex());
                break;
            case 3:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetThirdFlagIndex());
                FlagsObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetThirdFlagIndex());
                FlagsObjects[3].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                break;
        }
    }

    // Looping rotation for flags
    IEnumerator LoopRotation(float angle)
    {
        float dir = 1f;
        float rotSpeed = 90.0f;
        float startAngle = angle;
        bool assigned = false;

        while (angle > 0)
        {
            float step = Time.deltaTime * rotSpeed;
            for (int i = 0; i < FlagsObjects.Length; i++)
            {
                FlagsObjects[i].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            }

            if (angle <= (startAngle / 3) && !assigned)
            {
                AssignFlags();
                assigned = true;
            }

            angle -= 2;
            yield return null;
        }

        // Reset rotations after the loop
        for (int i = 0; i < FlagsObjects.Length; i++)
        {
            FlagsObjects[i].GetComponent<Transform>().rotation = Quaternion.identity;
        }
    }

    void UpdateDisplayedText()
    {
        string finalFlagName = m_GameData.GetFlagName(m_GameData.GetFinalFlagIndex());
        DisplayedText.text = finalFlagName; // Display the final flag name
    }
}
