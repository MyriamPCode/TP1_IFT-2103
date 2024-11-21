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

    // Méthode qui s'exécute lorsque le joueur 1 soumet son nom
    public void OnPlayer1NameSubmitted(string playerName)
    {
        player1Name = string.IsNullOrEmpty(playerName) ? "Joueur 1" : playerName;
        Debug.Log("Nom Joueur 1 : " + player1Name);

        // Désactiver le champ de saisie du joueur 1
        player1InputField.interactable = false;
    }

    // Méthode qui s'exécute lorsque le joueur 2 soumet son nom
    public void OnPlayer2NameSubmitted(string playerName)
    {
        player2Name = string.IsNullOrEmpty(playerName) ? "Joueur 2" : playerName;
        Debug.Log("Nom Joueur 2 : " + player2Name);

        // Désactiver le champ de saisie du joueur 2
        player2InputField.interactable = false;
    }

    // Méthode pour récupérer le nom du joueur 1
    public string getPlayer1Name()
    {
        return player1Name;
    }

    // Méthode pour récupérer le nom du joueur 2
    public string getPlayer2Name()
    {
        return player2Name;
    }

    // Méthode pour mettre à jour le type du joueur 1 (Humain ou Ordinateur)
    public void OnPlayer1TypeChanged(int index)
    {
        player1IsHuman = index == 0;  // 0 correspond à Humain, 1 à Ordinateur
        Debug.Log("Joueur 1 est " + (player1IsHuman ? "Humain" : "Ordinateur"));
    }

    // Méthode pour mettre à jour le type du joueur 2
    public void OnPlayer2TypeChanged(int index)
    {
        player2IsHuman = index == 0;  // 0 correspond à Humain, 1 à Ordinateur
        Debug.Log("Joueur 2 est " + (player2IsHuman ? "Humain" : "Ordinateur"));
    }
}
