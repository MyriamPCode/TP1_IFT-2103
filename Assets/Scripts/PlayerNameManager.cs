using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public InputField player1InputField;
    public InputField player2InputField;

    // Variables pour stocker les noms des joueurs
    public string player1Name = "Joueur 1";
    public string player2Name = "Joueur 2";

    public Dropdown player1TypeDropdown;
    public Dropdown player2TypeDropdown;

    public bool player1IsHuman = true; // Par défaut, Joueur 1 est humain
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

    // Optionnel : méthode pour récupérer les noms des joueurs
    public (string, string) GetPlayerNames()
    {
        return (player1Name, player2Name);
    }

    public void OnPlayer1TypeChanged(int index)
    {
        player1IsHuman = index == 0;  // Index 0 correspond à Humain, Index 1 à Ordinateur
        Debug.Log("Joueur 1 est " + (player1IsHuman ? "Humain" : "Ordinateur"));
    }

    // Méthode pour mettre à jour le type du joueur 2 (Humain ou Ordinateur)
    public void OnPlayer2TypeChanged(int index)
    {
        player2IsHuman = index == 0;  // Index 0 correspond à Humain, Index 1 à Ordinateur
        Debug.Log("Joueur 2 est " + (player2IsHuman ? "Humain" : "Ordinateur"));
    }
}
