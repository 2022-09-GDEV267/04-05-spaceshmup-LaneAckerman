using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]
    bool placeholder1; // here to keep VS from freaking out - DELETE IT

    [Header("Set Dynamically: Enemy")]
    bool placeholder2; // here to keep VS from freaking out - DELETE IT

    private void Awake()
    {

    }

    // This is a property: A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {

        }
    }

    public virtual void Move()
    {

    }

    private void OnCollisionEnter(Collision coll)
    {
 
    }

    void ShowDamage()
    {

    }

    void UnShowDamage()
    {

    }
}
