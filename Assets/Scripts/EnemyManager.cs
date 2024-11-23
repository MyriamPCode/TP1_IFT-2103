using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance; 
    private int enemyCount = 2; 
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); 
    }

    public void EnemyDestroyed()
    {
        enemyCount--;
        if (enemyCount == 1)
        {
            Debug.Log("One enemy left. Speeding up the other enemy!");
            NotifyRemainingEnemy();
        }
    }

    private void NotifyRemainingEnemy()
    {
        foreach (EnemyMovement enemy in FindObjectsOfType<EnemyMovement>())
        {
            enemy.SwitchToFastState();
        }
    }
}
