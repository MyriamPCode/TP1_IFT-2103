using UnityEngine;

public class PointVictoire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LogicManager logicManager = FindObjectOfType<LogicManager>();
            if (logicManager != null)
            {
                logicManager.Victory();
            }
            else
            {
                Debug.LogWarning("LogicManager introuvable !");
            }
        }
    }
}
