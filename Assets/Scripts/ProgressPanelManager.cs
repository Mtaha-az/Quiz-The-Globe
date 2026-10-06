using UnityEngine;
using UnityEngine.UI;

public class ProgressPanelManager : MonoBehaviour
{
    public Text timeTrialBestEurope;
    public Text survivalBestEurope;
    public Text timeTrialBestAsia;
    public Text survivalBestAsia;
    public Text timeTrialBestAfrica;
    public Text survivalBestAfrica;
    public Text timeTrialBestNorthAmerica;
    public Text survivalBestNorthAmerica;
    public Text timeTrialBestSouthAmerica;
    public Text survivalBestSouthAmerica;
    public Text timeTrialBestOceania;
    public Text survivalBestOceania;

    void Start()
    {
        UpdateBestScores(); // Display the best scores on panel
    }

    private void UpdateBestScores()
    {
        // Update Time Trial best scores
        timeTrialBestEurope.text = "Time Trial Best: " + ProgressManager.GetTimeTrialBestScore("EUROPE");
        timeTrialBestAsia.text = "Time Trial Best: " + ProgressManager.GetTimeTrialBestScore("ASIA");
        timeTrialBestAfrica.text = "Time Trial Best: " + ProgressManager.GetTimeTrialBestScore("AFRICA");
        timeTrialBestNorthAmerica.text = "Time Trial Best: " + ProgressManager.GetTimeTrialBestScore("NORTHAMERICA");
        timeTrialBestSouthAmerica.text = "Time Trial Best: " + ProgressManager.GetTimeTrialBestScore("SOUTHAMERICA");
        timeTrialBestOceania.text = "Time Trial Best: " + ProgressManager.GetTimeTrialBestScore("OCEANIA");

        // Update Survival best scores
        survivalBestEurope.text = "Survival Best: " + ProgressManager.GetSurvivalBestScore("EUROPE");
        survivalBestAsia.text = "Survival Best: " + ProgressManager.GetSurvivalBestScore("ASIA");
        survivalBestAfrica.text = "Survival Best: " + ProgressManager.GetSurvivalBestScore("AFRICA");
        survivalBestNorthAmerica.text = "Survival Best: " + ProgressManager.GetSurvivalBestScore("NORTHAMERICA");
        survivalBestSouthAmerica.text = "Survival Best: " + ProgressManager.GetSurvivalBestScore("SOUTHAMERICA");
        survivalBestOceania.text = "Survival Best: " + ProgressManager.GetSurvivalBestScore("OCEANIA");
    }
}
