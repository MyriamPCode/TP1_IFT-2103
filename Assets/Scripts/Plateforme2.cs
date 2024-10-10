using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BoxCollider2D>() == null)
        {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            
            boxCollider.offset = new Vector2(0f, -0.036f);

            boxCollider.size = new Vector2(0.26f, 0.58f);
        }
    }
        
    }


