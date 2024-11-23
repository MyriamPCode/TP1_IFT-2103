using UnityEngine;

[System.Serializable]
public class PlayerConfiguration
{
    public string name;
    public bool isHuman;
    public KeyboardLayout layout;
    public DifficultyLevel difficulty;
}

public enum DifficultyLevel
{
    Easy,
    Hard
}