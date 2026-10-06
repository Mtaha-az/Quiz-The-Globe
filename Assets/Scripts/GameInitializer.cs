using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    // References to the Time Trial Manager, Survival Mode Manager, and Hybrid Mode Manager
    public GameObject timeTrialManager; // Drag your TimeTrialManager GameObject here in the Inspector
    public GameObject survivalModeManager; // Drag your SurvivalModeManager GameObject here in the Inspector

    void Start()
    {
        // Check which mode was selected in the GameModeMenu and activate the appropriate manager
        if (GameModeMenu.selectedMode == "TimeTrial")
        {
            timeTrialManager.SetActive(true);  // Activate Time Trial mode
            survivalModeManager.SetActive(false); // Deactivate Survival mode
        }
        else if (GameModeMenu.selectedMode == "Survival")
        {
            timeTrialManager.SetActive(false); // Deactivate Time Trial mode
            survivalModeManager.SetActive(true);  // Activate Survival mode
        }
    }
}
