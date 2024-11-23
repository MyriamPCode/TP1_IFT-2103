using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasJoueur1Manager : MonoBehaviour
{
    private static CanvasJoueur1Manager instance;

    private void Awake()
    {
        if (FindObjectsOfType<CanvasJoueur1Manager>().Length == 1)
        {
            // Si c'est la premi�re instance, la rendre persistante entre les sc�nes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si une instance existe d�j�, d�truire cette nouvelle instance
            Destroy(gameObject);
        }
    }
}
