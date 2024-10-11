using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelManPlant_10 : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<BoxCollider2D>() == null)
        {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            
            boxCollider.offset = new Vector2(0f, -0.003f);

            boxCollider.size = new Vector2(0.09f, 0.098f);
        }
    }
        
    }


