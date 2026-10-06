using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class GameSettings : MonoBehaviour
{
    // Dictionary to hold scene names for each continent type
    private Dictionary<EContinentType, string> _SceneName = new Dictionary<EContinentType, string>();

    // Enum representing different continents
    public enum EContinentType
    {
        E_NOT_SET = 0,
        E_EUROPE,
        E_ASIA,
        E_AFRICA,
        E_NORTH_AMERICA,
        E_SOUTH_AMERICA,
        E_OCEANIA
    }

    // Enum representing different game modes
    public enum EGameMode
    {
        E_NONE = 0,
        E_TIME_TRIAL,
        E_SURVIVAL // Add other modes if needed
    }

    // Private fields to track current continent and game mode
    private EContinentType _Continent;
    private EGameMode _GameMode; // Track the game mode
    private string _ContinentName;

    // Singleton instance
    public static GameSettings Instance;

    // Awake method to ensure only one instance of GameSettings exists
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this); // Prevent destruction on scene load
            Instance = this;
        }
        else
        {
            Destroy(this); // Destroy duplicate instances
        }
    }

    // Start method to initialize continent data
    void Start()
    {
        SetSceneNameAndType();
        _Continent = EContinentType.E_NOT_SET; // Default to not set
        _GameMode = EGameMode.E_NONE; // Default to no game mode selected
    }

    // Method to map continents to scene names
    private void SetSceneNameAndType()
    {
        _SceneName.Add(EContinentType.E_EUROPE, "Game");
        _SceneName.Add(EContinentType.E_ASIA, "Game");
        _SceneName.Add(EContinentType.E_AFRICA, "Game");
        _SceneName.Add(EContinentType.E_NORTH_AMERICA, "Game");
        _SceneName.Add(EContinentType.E_SOUTH_AMERICA, "Game");
        _SceneName.Add(EContinentType.E_OCEANIA, "Game");
    }

    // Set the current continent type
    public void SetContinentType(EContinentType type)
    {
        _Continent = type;
    }

    // Get the current continent type
    public EContinentType GetContinentType()
    {
        return _Continent;
    }

    // Set the continent name and update the continent type accordingly
    public void SetContinentName(string Name)
    {
        SetContinentType(GetContinentTypeFromString(Name));
        _ContinentName = Name;
    }

    // Convert a string to an EContinentType enum value
    private EContinentType GetContinentTypeFromString(string type)
    {
        switch (type.ToUpper()) // Ensure case-insensitive comparison
        {
            case "EUROPE": return EContinentType.E_EUROPE;
            case "ASIA": return EContinentType.E_ASIA;
            case "AFRICA": return EContinentType.E_AFRICA;
            case "NORTHAMERICA": return EContinentType.E_NORTH_AMERICA;
            case "SOUTHAMERICA": return EContinentType.E_SOUTH_AMERICA;
            case "OCEANIA": return EContinentType.E_OCEANIA;
            default: return EContinentType.E_NOT_SET;
        }
    }

    // Set the game mode
    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }

    // Get the current game mode
    public EGameMode GetGameMode()
    {
        return _GameMode;
    }

    // Load the scene for the selected continent and game mode
    public void LoadSelectedContinentScene()
    {
        if (_Continent != EContinentType.E_NOT_SET)
        {
            string sceneName = _SceneName[_Continent];
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName); // Load the scene based on selected continent
            }
            else
            {
                Debug.LogError("Scene name for the selected continent is not set!");
            }
        }
        else
        {
            Debug.LogError("Continent type is not set. Cannot load the scene.");
        }
    }

    // Public method to load the game mode scene based on the selected continent and game mode
    public void LoadGameMode()
    {
        if (_GameMode == EGameMode.E_NONE)
        {
            Debug.LogError("No game mode selected!");
            return;
        }

        LoadSelectedContinentScene(); // Load the scene based on selected continent
    }

    // Example methods for selecting game modes via UI buttons

    // Select Time Trial Mode
    public void SelectTimeTrialMode()
    {
        SetGameMode(EGameMode.E_TIME_TRIAL);
        LoadGameMode(); // Load the scene for Time Trial mode
    }

    // Select Survival Mode
    public void SelectSurvivalMode()
    {
        SetGameMode(EGameMode.E_SURVIVAL);
        LoadGameMode(); // Load the scene for Survival mode
    }

    public string GetSelectedContinentName()
    {
        return _ContinentName; // Return the stored continent name
    }
}
