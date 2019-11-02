using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BabySwarmEnemy : GeneralEnemy
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
        minCoins = 1;
        maxCoins = 1;

        speed = 0.03f;
    }

    // Update is called once per frame
    
    new void Update()
    {
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
