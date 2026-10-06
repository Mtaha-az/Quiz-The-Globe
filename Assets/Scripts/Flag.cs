using System.Collections;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private FlagManager m_FlagManager;
    private bool LoadNewGame = false;
    private bool ButtonPressed = false;

    private Checkbox m_Checkbox;
    private CurrentGameData m_GameData;
    [HideInInspector]
    public int FlagIndex = 0;

    void Start()
    {
        // Finding the FlagManager attached to the Main Camera and getting the reference to it
        m_FlagManager = GameObject.Find("Main Camera").GetComponent<FlagManager>();
        m_Checkbox = GameObject.Find("Checkbox").GetComponent<Checkbox>() as Checkbox;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
    }

    void Update()
    {
        // If a new game should be loaded, call the LoadNextGame method from the FlagManager
        if (LoadNewGame == true && ButtonPressed == false)
        {
            if (m_Checkbox.AnimationCompleted())
            {
                m_FlagManager.LoadNextGame();
                LoadNewGame = false;
                m_Checkbox.Clear();
            }
        }
    }

    private void OnMouseDown()
    {
        if (m_FlagManager.gameOver) return; // Exit early if the game is over

        if (ButtonPressed == false)
        {
            bool correctAnswer = false;

            // Check if the clicked flag is the correct one
            if (FlagIndex == m_GameData.GetFinalFlagIndex())
            {
                m_Checkbox.Correct();
                correctAnswer = true;
            }
            else
            {
                m_Checkbox.Wrong();
            }

            // Time Trial Mode: Update the countdown timer
            TimeTrialManager timeTrialManager = FindObjectOfType<TimeTrialManager>();
            if (timeTrialManager != null)
            {
                timeTrialManager.UpdateTimer(correctAnswer);
            }

            // Survival Mode: Handle the lifeline logic
            SurvivalModeManager survivalModeManager = FindObjectOfType<SurvivalModeManager>();
            if (survivalModeManager != null)
            {
                survivalModeManager.UpdateSurvivalMode(correctAnswer);
            }

            LoadNewGame = true; // Load the next game
        }

        StartCoroutine(Sleep());
    }

    public void SetFlagIndex(int index)
    {
        FlagIndex = index;
    }

    IEnumerator Sleep()
    {
        ButtonPressed = true;
        yield return new WaitForSeconds(1.0f);
        ButtonPressed = false;
    }
}
