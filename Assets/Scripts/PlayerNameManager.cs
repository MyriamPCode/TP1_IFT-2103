using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public InputField player1InputField;
    public InputField player2InputField;

    public Text player1ControllerText;
    public Text player1DifficultyText;
    public Text player2DifficultyText;

    public string player1Name = "Joueur 1";
    public string player2Name = "Joueur 2";

    public Dropdown player1TypeDropdown;
    public Dropdown player2TypeDropdown;

    public bool player1IsHuman = true;
    public bool player2IsHuman = true;
    public bool player1IsEasy = true;
    public bool player2IsEasy = true;

    public Dropdown player1KeyboardLayoutDropdown;
    public Dropdown player1DifficultyDropdown;
    public Dropdown player2DifficultyDropdown;

    private void Start()
    {
        player1KeyboardLayoutDropdown.gameObject.SetActive(true);
        player1DifficultyDropdown.gameObject.SetActive(false);
        player2DifficultyDropdown.gameObject.SetActive(false);
        player1ControllerText.gameObject.SetActive(true);
        player1DifficultyText.gameObject.SetActive(false);
        player2DifficultyText.gameObject.SetActive(false);

        player1TypeDropdown.onValueChanged.AddListener(OnPlayer1TypeChanged);
        player2TypeDropdown.onValueChanged.AddListener(OnPlayer2TypeChanged);
    }

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

    private void OnPlayer1TypeChanged(int index)
    {
        player1IsHuman = (index == 0);  // Mettez à jour le booléen
        player1KeyboardLayoutDropdown.gameObject.SetActive(player1IsHuman);
        player1ControllerText.gameObject.SetActive(player1IsHuman);
        player1DifficultyDropdown.gameObject.SetActive(!player1IsHuman);
        player1DifficultyText.gameObject.SetActive(!player1IsHuman);
    }


    private void OnPlayer2TypeChanged(int index)
    {
        player2IsHuman = (index == 0);
        player2DifficultyDropdown.gameObject.SetActive(!player2IsHuman);
        player1DifficultyText.gameObject.SetActive(!player2IsHuman);
    }


    // Fonction pour commencer le jeu, selon les choix de l'utilisateur
    public void StartGame()
    {
        SceneLoader.LoadScene("MainScene");
    }


}
