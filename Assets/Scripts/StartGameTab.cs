using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameTab : MonoBehaviour
{
    [Header("References")] public TMP_Dropdown difficultyDropdown;
    public Button startGameButton;

    public virtual void Start()
    {
        // Get all values of the enum and convert them to strings
        List<string> difficulties =
            (from DifficultyEnum value in Enum.GetValues(typeof(DifficultyEnum)) select value.ToString()).ToList();
        
        difficultyDropdown.AddOptions(difficulties);
        
        startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        // Get the selected difficulty
        DifficultyEnum difficulty = (DifficultyEnum) Enum.Parse(typeof(DifficultyEnum), difficultyDropdown.options[difficultyDropdown.value].text);
        
        // Start the game
        GameManager.Instance.StartGame(difficulty);
    }
}