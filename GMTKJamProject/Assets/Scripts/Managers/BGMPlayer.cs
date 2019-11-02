using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {

    AudioSource audioSource;

    public bool hasIntro; //true if there is an intro portion to be played only once at the beginning of the song, false otherwise
    public float introDuration; //duration between start of the intro and the start of the main bgm. disregard if hasIntro is false
    public AudioClip intro; //audioclip of the intro. disregard if hasIntro is false
    public AudioClip mainBGM; //audioclip of the main body of the song
    public float bgmDuration; //durantion of the main bgm loop
    public float startingOffset; //time it takes song to start

    //note:
    //values related to 'duration' aren't necessarily equal to the audioclips raw duration. 
    //if a given song lasts 125 seconds, but the last 3 seconds are silence or leftover reverb effects, 
    //the songs desired duration value should be around 122 seconds


    //public bool loops;

    float offsetCounter = 0;
    float introCounter = 0;
    float bgmCounter = 0;

    GameObject generalManager;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        generalManager = GameObject.Find("GeneralManager");
    }
	
	// Update is called once per frame
	void Update () {
        

        if (!generalManager.GetComponent<TutorialProcess>().tutorialIsOn) { 
        if (offsetCounter < startingOffset)
        {
            offsetCounter += Time.deltaTime;
        }
        else
        {
                if (hasIntro)
                {
                    if (introCounter == 0)
                    {
                        audioSource.PlayOneShot(intro, 0.6f);
                        introCounter += Time.deltaTime;
                    }
                    else if (introCounter < introDuration)
                    {
                        introCounter += Time.deltaTime;
                    }
                    else
                    {
                        if (bgmCounter == 0)
                        {
                            audioSource.PlayOneShot(mainBGM, 0.6f);
                            bgmCounter += Time.deltaTime;
                        }
                        else if (bgmCounter < bgmDuration)
                        {
                            bgmCounter += Time.deltaTime;
                        }
                        else
                        {
                            bgmCounter = 0;
                        }
                    }

                }
                else
                {
                    if (bgmCounter == 0)
                    {
                        audioSource.PlayOneShot(mainBGM);
                        bgmCounter += Time.deltaTime;
                    }
                    else if (bgmCounter < bgmDuration)
                    {
                        bgmCounter += Time.deltaTime;
                    }
                    else
                    {
                        bgmCounter = 0;

                    }
                }
            }
        }

	}
}
