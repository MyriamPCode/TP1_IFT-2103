using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public InputField player1InputField;
    public InputField player2InputField;

    public Dropdown player1KeyboardLayoutDropdown;

    private string defaultPlayer1Name = "Joueur 1";
    private string defaultPlayer2Name = "Joueur 2";



    private void Start()
    {
        player1KeyboardLayoutDropdown.gameObject.SetActive(true);

        player1InputField.text = PlayerPrefs.GetString("PlayerName1", defaultPlayer1Name);
        player2InputField.text = PlayerPrefs.GetString("PlayerName2", defaultPlayer2Name);
    }

    public void OnPlayer1NameSubmitted(string playerName)
    {
        string validatedName = string.IsNullOrEmpty(playerName) ? defaultPlayer1Name : playerName;
        PlayerPrefs.SetString("PlayerName1", validatedName);
        player1InputField.interactable = false;
    }

    public void OnPlayer2NameSubmitted(string playerName)
    {
        string validatedName = string.IsNullOrEmpty(playerName) ? defaultPlayer2Name : playerName;
        PlayerPrefs.SetString("PlayerName2", validatedName);
        player2InputField.interactable = false;
    }

    public string GetPlayerName(int playerIndex)
    {
        if (playerIndex == 1)
            return PlayerPrefs.GetString("PlayerName1", "Joueur 1");
        else if (playerIndex == 2)
            return PlayerPrefs.GetString("PlayerName2", "Joueur 2");
        else
            return "Joueur Inconnu";
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(player1InputField.text))
            PlayerPrefs.SetString("PlayerName1", defaultPlayer1Name);
        if (string.IsNullOrEmpty(player2InputField.text))
            PlayerPrefs.SetString("PlayerName2", defaultPlayer2Name);

        SceneLoader.LoadScene("MainScene");
    }


}
