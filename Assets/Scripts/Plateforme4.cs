using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme4 : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<BoxCollider2D>() == null)
        {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            
            boxCollider.offset = new Vector2(0f, -0.03f);

            boxCollider.size = new Vector2(0.465f, 0.6f);
        }
    }
        
    }


