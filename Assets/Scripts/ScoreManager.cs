using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text questionedFlagsText; // UI Text to display questioned flags (e.g., "0/51")

    private int questionedFlags = 0; // Counter for how many flags have been questioned
    private int totalFlags = 0; // Total number of flags for the selected continent

    private string selectedContinent; // Holds the currently selected continent

    // Flag totals for each continent
    private readonly int europeFlags = 51;
    private readonly int asiaFlags = 51;
    private readonly int africaFlags = 54;
    private readonly int northAmericaFlags = 29;
    private readonly int southAmericaFlags = 13;
    private readonly int oceaniaFlags = 27;

    void Start()
    {
        // Retrieve the selected continent name from GameSettings
        selectedContinent = GameSettings.Instance.GetSelectedContinentName();

        // Set the total flags based on the selected continent
        SetTotalFlagsForContinent(selectedContinent);

        // Initialize the score display
        UpdateQuestionedFlagsText();
    }

    // Set the total flags based on the selected continent
    private void SetTotalFlagsForContinent(string continent)
    {
        switch (continent.ToUpper()) // Ensure case-insensitive comparison
        {
            case "EUROPE":
                totalFlags = europeFlags;
                break;
            case "ASIA":
                totalFlags = asiaFlags;
                break;
            case "AFRICA":
                totalFlags = africaFlags;
                break;
            case "NORTHAMERICA":
                totalFlags = northAmericaFlags;
                break;
            case "SOUTHAMERICA":
                totalFlags = southAmericaFlags;
                break;
            case "OCEANIA":
                totalFlags = oceaniaFlags;
                break;
            default:
                Debug.LogError("Invalid continent selected.");
                totalFlags = 0;
                break;
        }
    }

    // Method to update the count of questioned flags
    public void IncrementQuestionedFlags()
    {
        questionedFlags++;
        UpdateQuestionedFlagsText();
    }

    // Update the UI Text to show the current score of questioned flags
    private void UpdateQuestionedFlagsText()
    {
        questionedFlagsText.text = questionedFlags + "/" + totalFlags; // Format: "QuestionedFlags/TotalFlags"
    }
    public void BackButtonForGameToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
