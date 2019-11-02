using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    float blackTimer = 255f;
    GameObject black;

    AudioSource audioS;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        DontDestroyOnLoad(GameObject.Find("GlobalHolder"));
        black = GameObject.Find("Black");

        if (GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().particleLimiting == false)
        {
            GameObject.Find("PLCheck").GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, 0);
        }
        else
        {
            GameObject.Find("PLCheck").GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, 255);
        }

        audioS = GetComponent<AudioSource>();
        audioS.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (blackTimer <= 0)
        {
            blackTimer = 0;
        }
        else
        {
            blackTimer -= Time.deltaTime * 255f;
            black.GetComponent<Image>().color = UITools.ColorFromRGB(0, 0, 0, Mathf.RoundToInt(blackTimer));
        }

		if (Input.GetKey(KeyCode.Space))
        {
            
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().particleLimiting == false)
            {
                GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().particleLimiting = true;
                GameObject.Find("PLCheck").GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, 255);
                audioS.PlayOneShot(Resources.Load("SFX/Confirm", typeof(AudioClip)) as AudioClip);
            }
            else
            {
                GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().particleLimiting = false;
                GameObject.Find("PLCheck").GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, 0);
                audioS.PlayOneShot(Resources.Load("SFX/Confirm", typeof(AudioClip)) as AudioClip);
            }
        }
    }
}
