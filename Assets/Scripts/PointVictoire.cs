using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointVictoire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BoxCollider2D>() == null)
        {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            
            boxCollider.offset = new Vector2(-0.0009982213f, 0f);

            boxCollider.size = new Vector2(0.06016327f, 0.23f);
        }
    }
        
    }


