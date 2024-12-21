using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private int enemyCount = 2; 
    public AudioClip victoryMusic; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDestroyed()
    {
        enemyCount--;

        if (enemyCount == 1)
        {
            Debug.Log("One enemy left. Speeding up the other enemy!");
            NotifyRemainingEnemy();
        }
        else if (enemyCount == 0)
        {
            Debug.Log("All enemies defeated. Switching to victory music.");
            PlayVictoryMusic();
        }
    }

    private void NotifyRemainingEnemy()
    {
        foreach (EnemyMovement enemy in FindObjectsOfType<EnemyMovement>())
        {
            enemy.SwitchToFastState();
        }
    }

    private void PlayVictoryMusic()
    {
        if (BackgroundMusicManager.instance != null)
        {
            BackgroundMusicManager.instance.CrossfadeToNewMusic(victoryMusic, 1f);
        }
        else
        {
            Debug.LogWarning("BackgroundMusicManager instance not found. Cannot switch music.");
        }
    }
}
