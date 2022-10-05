using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Set In Inspector")]
    private BoundsCheck bndCheck;

    [Header("Set Dynamically")]
    bool placeholder2; // here to keep VS from freaking out - DELETE IT


    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    private void Update()
    {

        if (bndCheck.offUp)
        {                                                

            Destroy(gameObject);

        }

    }

    
}
