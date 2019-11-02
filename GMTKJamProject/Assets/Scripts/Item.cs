using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    float spawnDirection;
    float spawnDistance;
    public Rigidbody2D rb2d;

    //public GameObject generalManager;

    // Use this for initialization
    public void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        spawnDistance = Random.Range(100, 200);
        spawnDirection = Random.Range(0, 360) / 180f * Mathf.PI;

        
        rb2d.AddForce(new Vector2(Mathf.Cos(spawnDirection) * spawnDistance, Mathf.Sin(spawnDirection) * spawnDistance));

        //generalManager = GameObject.Find("GeneralManager");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract void ManageDestroy();
}
