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
            Destroy(gameObject); // 防止重复实例
    }

    // 调用此方法减少敌人数量
    public void EnemyDestroyed()
    {
        enemyCount--;
        if (enemyCount == 1) // 只剩一个敌人时
        {
            Debug.Log("One enemy left. Speeding up the other enemy!");
            NotifyRemainingEnemy();
        }
    }

    // 通知剩下的敌人切换状态
    private void NotifyRemainingEnemy()
    {
        foreach (EnemyMovement enemy in FindObjectsOfType<EnemyMovement>())
        {
            enemy.SwitchToFastState();
        }
    }
}
