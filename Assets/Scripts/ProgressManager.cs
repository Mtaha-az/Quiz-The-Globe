using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    private const string TimeTrialPrefix = "TimeTrial_";
    private const string SurvivalPrefix = "Survival_";

    // Get the best score for a given continent in Time Trial mode
    public static int GetTimeTrialBestScore(string continent)
    {
        return PlayerPrefs.GetInt(TimeTrialPrefix + continent, 0); // Default value 0
    }

    // Get the best score for a given continent in Survival mode
    public static int GetSurvivalBestScore(string continent)
    {
        return PlayerPrefs.GetInt(SurvivalPrefix + continent, 0); // Default value 0
    }

    // Save the best score for Time Trial mode
    public static void SaveTimeTrialBestScore(string continent, int score)
    {
        int currentBest = GetTimeTrialBestScore(continent);
        if (score > currentBest)
        {
            PlayerPrefs.SetInt(TimeTrialPrefix + continent, score);
            PlayerPrefs.Save(); // Ensure the data is saved
        }
    }

    // Save the best score for Survival mode
    public static void SaveSurvivalBestScore(string continent, int score)
    {
        int currentBest = GetSurvivalBestScore(continent);
        if (score > currentBest)
        {
            PlayerPrefs.SetInt(SurvivalPrefix + continent, score);
            PlayerPrefs.Save(); // Ensure the data is saved
        }
    }
}
