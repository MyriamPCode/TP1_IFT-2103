using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public InputField player1InputField;
    public InputField player2InputField;

    public Dropdown player1KeyboardLayoutDropdown;

    public string player1Name = "Joueur 1";
    public string player2Name = "Joueur 2";


    private void Start()
    {
        player1KeyboardLayoutDropdown.gameObject.SetActive(true);
    }

    public void OnPlayer1NameSubmitted(string playerName)
    {
        player1Name = string.IsNullOrEmpty(playerName) ? "Joueur 1" : playerName;
        PlayerPrefs.SetString("PlayerName1", player1Name);

        player1InputField.interactable = false;
    }

    public void OnPlayer2NameSubmitted(string playerName)
    {
        player2Name = string.IsNullOrEmpty(playerName) ? "Joueur 2" : playerName;
        PlayerPrefs.SetString("PlayerName2", player2Name);

        player2InputField.interactable = false;
    }

    public string getPlayer1Name()
    {
        return player1Name;
    }

    public string getPlayer2Name()
    {
        return player2Name;
    }

    public void StartGame()
    {
        SceneLoader.LoadScene("MainScene");
    }


}
