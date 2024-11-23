using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField player1InputField;
    public TMP_InputField player2InputField;

    public string player1Name = "Joueur 1";
    public string player2Name = "Joueur 2";

    public Dropdown player1TypeDropdown;
    public Dropdown player2TypeDropdown;

    public bool player1IsHuman = true;
    public bool player2IsHuman = true;

    public void OnPlayer1NameSubmitted(string playerName)
    {
        player1Name = string.IsNullOrEmpty(playerName) ? "Joueur 1" : playerName;
        Debug.Log("Nom Joueur 1 : " + player1Name);

        player1InputField.interactable = false;
    }

    public void OnPlayer2NameSubmitted(string playerName)
    {
        player2Name = string.IsNullOrEmpty(playerName) ? "Joueur 2" : playerName;
        Debug.Log("Nom Joueur 2 : " + player2Name);

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

    public void OnPlayer1TypeChanged(int index)
    {
        player1IsHuman = index == 0;  
        Debug.Log("Joueur 1 est " + (player1IsHuman ? "Humain" : "Ordinateur"));
    }

    public void OnPlayer2TypeChanged(int index)
    {
        player2IsHuman = index == 0;
        Debug.Log("Joueur 2 est " + (player2IsHuman ? "Humain" : "Ordinateur"));
    }
}
