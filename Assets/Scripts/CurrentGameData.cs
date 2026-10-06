using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGameData : MonoBehaviour
{
    [HideInInspector]
    public int FirstFlagIndex = 0;
    public int SecondFlagIndex;
    public int ThirdFlagIndex; // New flag index
    public int FinalFlagIndex;

    private int PrevFinalFlagIndex;
    private int CountriesPerGame = 60; // How many countries should be in one game.
    private bool GameFinished = false;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GameData.Instance.AssignArrayOfCountry();

        if (CountriesPerGame >= GameData.Instance.CountryDataSet.Length)
            CountriesPerGame = GameData.Instance.CountryDataSet.Length;

        GameData.Instance.CountrySetPerGame = new CountryData[CountriesPerGame]; // Fix: Use CountryData directly

        GameFinished = false;

        PickCountriesForGame();
        GetNewCountries();
    }

    void Update()
    {
        // Update logic if needed.
    }

    public void PickCountriesForGame()
    {
        int PickedCountryNumber = 0;

        for (int Index = 0; Index < GameData.Instance.CountryDataSet.Length; Index++)
        {
            if (PickedCountryNumber >= CountriesPerGame)
                break;
            else
            {
                if (GameData.Instance.CountryDataSet[Index].Guessed == false)
                {
                    GameData.Instance.CountrySetPerGame[PickedCountryNumber] = GameData.Instance.CountryDataSet[Index];
                    PickedCountryNumber++;
                }
            }
        }

        if (PickedCountryNumber < CountriesPerGame - 1)
        {
            // If we do not have enough countries, select random ones which were guessed.
            for (int Index = 0; Index < GameData.Instance.CountryDataSet.Length; Index++)
            {
                if (PickedCountryNumber >= CountriesPerGame)
                    break;
                else
                {
                    if (GameData.Instance.CountryDataSet[Index].Guessed == true)
                    {
                        GameData.Instance.CountrySetPerGame[PickedCountryNumber] = GameData.Instance.CountryDataSet[Index];
                        PickedCountryNumber++;
                    }
                }
            }
        }
    }

    // CurrentGameData.cs
    public void GetNewCountries()
    {
        PrevFinalFlagIndex = FinalFlagIndex;

        // If there are no flags left to question, trigger game over
        if (GetNumberOfFlagsLeft() == 0)
        {
            FlagManager flagManager = FindObjectOfType<FlagManager>(); // Reference to FlagManager
            flagManager.HandleGameOver(true); // Add the score for the last question and trigger game over
            return; // Exit early since no more flags are left
        }

        // Get a list of unasked countries
        List<int> unaskedCountries = new List<int>();
        List<int> previouslyAskedCountries = new List<int>(); // Store previously used flags

        for (int i = 0; i < GameData.Instance.CountrySetPerGame.Length; i++)
        {
            if (!GameData.Instance.CountrySetPerGame[i].BeenQuestioned)
            {
                unaskedCountries.Add(i);
            }
            else
            {
                previouslyAskedCountries.Add(i);
            }
        }

        // Shuffle previously asked flags if needed
        previouslyAskedCountries = ShuffleList(previouslyAskedCountries);

        // Proceed with the game even if there are fewer than 4 countries left
        int remainingCount = unaskedCountries.Count;

        // Pick the final flag from the remaining unasked countries, or from the previously asked ones if needed
        if (remainingCount > 0)
        {
            FinalFlagIndex = unaskedCountries[Random.Range(0, remainingCount)];
            unaskedCountries.Remove(FinalFlagIndex); // Remove the final flag
        }
        else
        {
            FinalFlagIndex = previouslyAskedCountries[Random.Range(0, previouslyAskedCountries.Count)];
            previouslyAskedCountries.Remove(FinalFlagIndex); // Remove the final flag from previous list
        }

        // Pick other flags, ensuring no duplication within the same question
        if (remainingCount > 1)
        {
            FirstFlagIndex = unaskedCountries[Random.Range(0, unaskedCountries.Count)];
            unaskedCountries.Remove(FirstFlagIndex); // Remove the first flag
        }
        else if (previouslyAskedCountries.Count > 0)
        {
            FirstFlagIndex = previouslyAskedCountries[Random.Range(0, previouslyAskedCountries.Count)];
            previouslyAskedCountries.Remove(FirstFlagIndex); // Remove from previous list
        }

        if (remainingCount > 2)
        {
            SecondFlagIndex = unaskedCountries[Random.Range(0, unaskedCountries.Count)];
            unaskedCountries.Remove(SecondFlagIndex); // Remove the second flag
        }
        else if (previouslyAskedCountries.Count > 0)
        {
            SecondFlagIndex = previouslyAskedCountries[Random.Range(0, previouslyAskedCountries.Count)];
            previouslyAskedCountries.Remove(SecondFlagIndex); // Remove from previous list
        }

        if (remainingCount > 3)
        {
            ThirdFlagIndex = unaskedCountries[Random.Range(0, unaskedCountries.Count)];
            unaskedCountries.Remove(ThirdFlagIndex); // Remove the third flag
        }
        else if (previouslyAskedCountries.Count > 0)
        {
            ThirdFlagIndex = previouslyAskedCountries[Random.Range(0, previouslyAskedCountries.Count)];
            previouslyAskedCountries.Remove(ThirdFlagIndex); // Remove from previous list
        }

        // Mark the final flag as "questioned"
        GameData.Instance.CountrySetPerGame[FinalFlagIndex].BeenQuestioned = true;
    }

    // Shuffle list function to randomize the order of previously asked flags
    private List<int> ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    // Method to check if all flags have been questioned
    public int GetNumberOfFlagsLeft()
    {
        int left = 0;

        for (int Index = 0; Index < GameData.Instance.CountrySetPerGame.Length; Index++)
        {
            if (GameData.Instance.CountrySetPerGame[Index].BeenQuestioned == false)
                left++;
        }

        return left;
    }

    public string GetFlagName(int index)
    {
        index = Mathf.Clamp(index, 0, GameData.Instance.CountrySetPerGame.Length - 1);
        return GameData.Instance.CountrySetPerGame[index].Name;
    }

    public int GetFlagNameLength(int FlagIndex)
    {
        return GameData.Instance.CountrySetPerGame[FlagIndex].Name.Length;
    }

    public int GetFirstFlagIndex()
    {
        return FirstFlagIndex;
    }

    public int GetSecondFlagIndex()
    {
        return SecondFlagIndex;
    }

    public int GetThirdFlagIndex()
    {
        return ThirdFlagIndex;
    }

    public int GetFinalFlagIndex()
    {
        return FinalFlagIndex;
    }

    public void SetGuessed()
    {
        GameData.Instance.CountrySetPerGame[FinalFlagIndex].Guessed = true;
    }

    public Sprite GetFlagSpriteIndex(int FlagIndex)
    {
        FlagIndex = Mathf.Clamp(FlagIndex, 0, GameData.Instance.CountrySetPerGame.Length - 1);
        return GameData.Instance.CountrySetPerGame[FlagIndex].Flag;
    }
}
