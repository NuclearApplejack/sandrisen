using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegularEnemy : GeneralEnemy {


	// Use this for initialization
	new void Start () {
        base.Start();
        speed = Random.Range(0.02f, 0.05f);
        minCoins = 3;
        maxCoins = 6;
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
