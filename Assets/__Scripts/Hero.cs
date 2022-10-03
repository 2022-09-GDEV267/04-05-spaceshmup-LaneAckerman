using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    static public Hero S; // Singleton

    [Header("Set in Inspector")]
    // These fields control the movement of the ship

    [Header("Set Dynamically")]






	void Start()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void AbsorbPowerUp(GameObject go)
    {

    }

    public float shieldLevel
    {
        get;set;
    }

}
