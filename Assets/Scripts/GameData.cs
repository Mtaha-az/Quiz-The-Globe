using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CountryData
{
    public string Name;
    public Sprite Flag;
    public bool Guessed; // Whenever the country has been guessed
    public bool BeenQuestioned; // If the country has been selected/questioned before
}

public class GameData : MonoBehaviour
{
    public CountryData[] EuropeanCountryDataSet;
    public CountryData[] AsiaCountryDataSet;
    public CountryData[] AfricaCountryDataSet;
    public CountryData[] NorthAmericaCountryDataSet;
    public CountryData[] SouthAmericaCountryDataSet;
    public CountryData[] OceaniaCountryDataSet;

    
    public CountryData[] CountrySetPerGame; // Active countries in the current game
    [HideInInspector]
    public CountryData[] CountryDataSet; // All available countries

    public static GameData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    public void AssignArrayOfCountry()
    {
        switch (GameSettings.Instance.GetContinentType())
        {
            case GameSettings.EContinentType.E_EUROPE:
                CountryDataSet = new CountryData[EuropeanCountryDataSet.Length]; // Initialize based on European dataset length
                EuropeanCountryDataSet.CopyTo(CountryDataSet, 0); // Copy data from the European dataset
                break;

            case GameSettings.EContinentType.E_ASIA:
                CountryDataSet = new CountryData[AsiaCountryDataSet.Length]; // Initialize based on European dataset length
                AsiaCountryDataSet.CopyTo(CountryDataSet, 0); // Copy data from the European dataset
                break;

            case GameSettings.EContinentType.E_AFRICA:
                CountryDataSet = new CountryData[AfricaCountryDataSet.Length]; // Initialize based on European dataset length
                AfricaCountryDataSet.CopyTo(CountryDataSet, 0); // Copy data from the European dataset
                break;

            case GameSettings.EContinentType.E_NORTH_AMERICA:
                CountryDataSet = new CountryData[NorthAmericaCountryDataSet.Length]; // Initialize based on European dataset length
                NorthAmericaCountryDataSet.CopyTo(CountryDataSet, 0); // Copy data from the European dataset
                break;

            case GameSettings.EContinentType.E_SOUTH_AMERICA:
                CountryDataSet = new CountryData[SouthAmericaCountryDataSet.Length]; // Initialize based on European dataset length
                SouthAmericaCountryDataSet.CopyTo(CountryDataSet, 0); // Copy data from the European dataset
                break;

            case GameSettings.EContinentType.E_OCEANIA:
                CountryDataSet = new CountryData[OceaniaCountryDataSet.Length]; // Initialize based on European dataset length
                OceaniaCountryDataSet.CopyTo(CountryDataSet, 0); // Copy data from the European dataset
                break;
        }
    }
}
