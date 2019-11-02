using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public bool gameOver = false;
    

    //Sprite[]

    

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        if (gameOver == true)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Item"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Particle"))
            {
                Destroy(g);
            }
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Item"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Particle"))
            {
                Destroy(g);
            }
            SceneManager.LoadScene(0);
        }

        

        
	}

    public void TriggerGameOver()
    {
        gameOver = true;
    }
}
