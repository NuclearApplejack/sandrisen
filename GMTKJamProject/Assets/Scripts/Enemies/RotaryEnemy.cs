using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotaryEnemy : GeneralEnemy
{

    bool rotatesClockwise;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        if (Random.Range(1, 3) == 1)
        {
            rotatesClockwise = true;
        }
        else
        {
            rotatesClockwise = false;
        }

        minCoins = 4;
        maxCoins = 7;
    }


    // Update is called once per frame
    new void Update()
    {
        base.Update();


        if (rotatesClockwise)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.Translate(new Vector3(direction.x, direction.y, 0).normalized * speed * 2);
            direction = Quaternion.Euler(0, 0, 90) * direction;
            transform.Translate(new Vector3(direction.x, direction.y, 0).normalized * speed * 2);
            
        }
        else
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.Translate(new Vector3(direction.x, direction.y, 0).normalized * speed * 2);
            direction = Quaternion.Euler(0, 0, -90) * direction;
            transform.Translate(new Vector3(direction.x, direction.y, 0).normalized * speed * 2);
        }
        

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
