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
            // Si c'est la première instance, la rendre persistante entre les scènes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si une instance existe déjà, détruire cette nouvelle instance
            Destroy(gameObject);
        }
    }
}
