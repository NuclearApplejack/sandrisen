using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwarmEnemy : GeneralEnemy {


    // Use this for initialization
    new void Start () {
        base.Start();
        minCoins = 1;
        maxCoins = 5;

        speed = 0.03f;

        
        
        int spawnCounter = Random.Range(4, 8);
        while (spawnCounter > 0)
        {
            spawnCounter--;
            GameObject enemy = Instantiate(Resources.Load("Prefabs/BabySwarmEnemy", typeof(GameObject)) as GameObject);
            Vector3 spawnPos = transform.position;
            spawnPos += (Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(1, 0, 0));
            enemy.transform.position = spawnPos;
            

        }
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();

        Vector3 direction = target.transform.position - transform.position;
        transform.Translate(new Vector3(direction.x, direction.y, 0).normalized * speed);

        if (target.transform.position.x > transform.position.x)
        {
            animator.SetBool("FacingRight", true);
        }
        else if (target.transform.position.x < transform.position.x)
        {
            animator.SetBool("FacingRight", false);
        }

        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            Destroy(gameObject);
        }

    }
}
