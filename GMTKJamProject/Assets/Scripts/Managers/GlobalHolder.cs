using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalHolder : MonoBehaviour {

    public float currScoreTime = 0f;
    public int currScoreEnergy = 0;
    public int killCount = 0;

    public bool particleLimiting = false;
    public bool hasSeenTutorial = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetScores()
    {
        currScoreEnergy = 0;
        currScoreTime = 0f;
        killCount = 0;
    }
}
