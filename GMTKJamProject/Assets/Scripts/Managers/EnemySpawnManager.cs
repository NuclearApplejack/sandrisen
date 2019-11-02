using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour {

    float spawnTimer = 0;
    float spawnDelay = 3f;

    float rotarySpawnTimer = 5f;
    float rotarySpawnDelay = 3.5f;

    float swarmSpawnTimer = 10f;
    float swarmSpawnDelay = 10f;

    float spawnIncreaseTimer = 20f;
    float spawnIncreaseDelay = 20f;

    GameObject generalManager;
    GameObject popupText;
    GameObject popupSubtext;

    public float powerupSpawnChance = 36f;

    // Use this for initialization
    void Start () {
        generalManager = GameObject.Find("GeneralManager");
        popupText = GameObject.Find("PopupText");
        popupSubtext = GameObject.Find("PopupSubtext");
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<TutorialProcess>().tutorialIsOn)
        {
            if (powerupSpawnChance >= 1001f)
            {
                powerupSpawnChance = 1001f;
            }
            else
            {
                powerupSpawnChance += Time.deltaTime / 2f;
            }

            if (spawnIncreaseTimer <= 0)
            {
                if (spawnDelay > 0.05f)
                {
                    spawnDelay /= 1.5f;
                    rotarySpawnDelay /= 1.3f;
                    swarmSpawnDelay /= 1.2f;
                    popupText.GetComponent<Text>().text = "Watch out!";
                    popupText.GetComponent<Text>().color = UITools.ColorFromRGB(120, 100, 100);
                    popupSubtext.GetComponent<Text>().text = "Enemy spawn rates increased";
                    popupSubtext.GetComponent<Text>().color = UITools.ColorFromRGB(120, 100, 100);
                    generalManager.GetComponent<UIManager>().popupTimer = 3f;
                }
                spawnIncreaseTimer = spawnIncreaseDelay;
            }
            else
            {
                spawnIncreaseTimer -= Time.deltaTime;
            }

            #region regular enemy
            if (spawnTimer <= 0)
            {
                GameObject enemy = Instantiate(Resources.Load("Prefabs/RegularEnemy", typeof(GameObject)) as GameObject);
                int spawnSide = Random.Range(0, 4);
                if (spawnSide == 0)
                {
                    enemy.transform.position = new Vector3(Random.Range(-20f, 40f), 20, -1);
                }
                else if (spawnSide == 1)
                {
                    enemy.transform.position = new Vector3(Random.Range(-20f, 40f), -40, -1);
                }
                else if (spawnSide == 2)
                {
                    enemy.transform.position = new Vector3(-20, Random.Range(-40f, 20f), -1);
                }
                else
                {
                    enemy.transform.position = new Vector3(40, Random.Range(-40f, 20f), -1);
                }

                enemy.GetComponent<GeneralEnemy>().powerupChance = Mathf.RoundToInt(powerupSpawnChance);

                spawnTimer = spawnDelay;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }

            
            #endregion

            #region rotary enemy
            if (rotarySpawnTimer <= 0)
            {
                GameObject enemy = Instantiate(Resources.Load("Prefabs/RotaryEnemy", typeof(GameObject)) as GameObject);
                int spawnSide = Random.Range(0, 4);
                if (spawnSide == 0)
                {
                    enemy.transform.position = new Vector3(Random.Range(-20f, 40f), 20, -1);
                }
                else if (spawnSide == 1)
                {
                    enemy.transform.position = new Vector3(Random.Range(-20f, 40f), -40, -1);
                }
                else if (spawnSide == 2)
                {
                    enemy.transform.position = new Vector3(-20, Random.Range(-40f, 20f), -1);
                }
                else
                {
                    enemy.transform.position = new Vector3(40, Random.Range(-40f, 20f), -1);
                }

                enemy.GetComponent<GeneralEnemy>().powerupChance = Mathf.RoundToInt(powerupSpawnChance);

                rotarySpawnTimer = rotarySpawnDelay;
            }
            else
            {
                rotarySpawnTimer -= Time.deltaTime;
            }

            #endregion

            #region swarm enemy
            if (swarmSpawnTimer <= 0)
            {
                GameObject enemy = Instantiate(Resources.Load("Prefabs/SwarmEnemy", typeof(GameObject)) as GameObject);
                int spawnSide = Random.Range(0, 4);
                if (spawnSide == 0)
                {
                    enemy.transform.position = new Vector3(Random.Range(-20f, 40f), 20, -1);
                }
                else if (spawnSide == 1)
                {
                    enemy.transform.position = new Vector3(Random.Range(-20f, 40f), -40, -1);
                }
                else if (spawnSide == 2)
                {
                    enemy.transform.position = new Vector3(-20, Random.Range(-40f, 20f), -1);
                }
                else
                {
                    enemy.transform.position = new Vector3(40, Random.Range(-40f, 20f), -1);
                }

                enemy.GetComponent<GeneralEnemy>().powerupChance = Mathf.RoundToInt(powerupSpawnChance);

                swarmSpawnTimer = swarmSpawnDelay;
            }
            else
            {
                swarmSpawnTimer -= Time.deltaTime;
            }


            #endregion
        }
    }

    
}
