using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : Item {
    GameObject generalManager;
    GameObject actionTaker;
    float lifespan = 5f;
    GameObject guide;
    //Rigidbody2D rb2d;
    public float attractRange = 1.5f;

    // Use this for initialization
    new void Start () {
        base.Start();
        
        generalManager = GameObject.Find("GeneralManager");
        actionTaker = GameObject.Find("ActionTaker");
        guide = GameObject.Find("Guide");
        rb2d = GetComponent<Rigidbody2D>();
    }

	
	// Update is called once per frame
	void Update () {
		if (lifespan <= 0 || SceneManager.GetActiveScene().name != "MainScene")
        {
            ManageDestroy();
        }
        else
        {
            lifespan -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, guide.transform.position) <= attractRange)
        {
            rb2d.AddForce((guide.transform.position - transform.position) * 10f);
        }

        if (generalManager.GetComponent<PlayerEffectManager>().chargefieldTimer > 0)
        {
            attractRange = 3f;
        }
        else
        {
            attractRange = 1.5f;
        }
	}

    public override void ManageDestroy()
    {
        if (lifespan > 0)
        {
            generalManager.GetComponent<ItemManager>().coinCount++;
            //actionTaker.GetComponent<ActionTaker>().dashTimer -= 1f;

            GameObject.Find("ActionTaker").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/Energy", typeof(AudioClip)) as AudioClip);
        }

        Destroy(gameObject);
    }
}
