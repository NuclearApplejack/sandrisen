using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialProcess : MonoBehaviour {

    public bool tutorialIsOn = true;
    Text text;
    public int tutorialStep = 0;
    GameObject global;

	// Use this for initialization
	void Start () {
        text = GameObject.Find("TutorialText").GetComponent<Text>();
        global = GameObject.Find("GlobalHolder");

        if (global.GetComponent<GlobalHolder>().hasSeenTutorial)
        {
            tutorialIsOn = false;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (tutorialIsOn)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                tutorialStep++;
            }

            if (tutorialStep == 0)
            {
                text.text = "Welcome to Sandrisen. Press Enter to continue.";
            }
            else if (tutorialStep == 1)
            {
                text.text = "Move your sentinel with the WASD keys or the arrow keys. This will also cause you to do an attack.";
            }
            else if (tutorialStep == 2)
            {
                text.text = "Press Spacebar or K to dash to your sentinels position. This will hit enemies on the way.";
            }
            else if (tutorialStep == 3)
            {
                text.text = "Destroying enemy sentinels will cause them to drop energy, which your sentinel can then pick up. If you run out of energy, you die.";
            }
            else if (tutorialStep == 4)
            {
                text.text = "Your supply of energy constantly decreases, and will plummet if you get hit by enemy sentinels.";
            }
            else if (tutorialStep == 5)
            {
                text.text = "Enemy sentinels will sometimes drop power ups, which can also be picked up by your sentinel. Power ups offer various effects, as well giving some extra energy.";
            }
            else if (tutorialStep == 6)
            {
                text.text = "Try to survive as long as you can. Good luck!";
            }
            else
            {
                text.text = "";
                tutorialIsOn = false;
                global.GetComponent<GlobalHolder>().hasSeenTutorial = true;
            }
        }
    }
}
