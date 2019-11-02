using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

    public int coinCount = 100;
    float energyDecayDelay = 1f;
    float energyDecayTimer = 1f;

    GameObject global;
    Text timerText;

    float playTime = 0f;

    // Use this for initialization
    void Start () {
        global = GameObject.Find("GlobalHolder");
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<TutorialProcess>().tutorialIsOn)
        {
            playTime += Time.deltaTime;
            timerText.text = Mathf.FloorToInt(playTime) + " seconds";
            global.GetComponent<GlobalHolder>().currScoreTime = playTime;

            if (energyDecayTimer > 0)
            {
                energyDecayTimer -= Time.deltaTime;
            }
            else
            {
                energyDecayTimer = energyDecayDelay;
                coinCount--;
            }

            energyDecayDelay -= Time.deltaTime / 500f;

            if (coinCount <= 0)
            {
                gameObject.GetComponent<GameOverManager>().TriggerGameOver();
            }

            if (coinCount > global.GetComponent<GlobalHolder>().currScoreEnergy)
            {
                global.GetComponent<GlobalHolder>().currScoreEnergy = coinCount;
            }
        }
	}
}
