﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Decoy : Item
{

    GameObject generalManager;
    GameObject popupText;
    GameObject popupSubtext;

    float lifespan = 7f;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        generalManager = GameObject.Find("GeneralManager");
        popupText = GameObject.Find("PopupText");
        popupSubtext = GameObject.Find("PopupSubtext");
    }


    // Update is called once per frame
    void Update()
    {
        if (lifespan <= 0 || SceneManager.GetActiveScene().name != "MainScene")
        {
            Destroy(gameObject);
        }
        else
        {
            lifespan -= Time.deltaTime;
        }
    }


    public override void ManageDestroy()
    {
        if (lifespan > 0)
        {
            GameObject.Find("ActionTaker").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/PowerUp", typeof(AudioClip)) as AudioClip);

            generalManager.GetComponent<PlayerEffectManager>().decoyTimer = 5f;
            generalManager.GetComponent<ItemManager>().coinCount += 5;
            popupText.GetComponent<Text>().text = "Decoy!";
            popupText.GetComponent<Text>().color = UITools.ColorFromRGB(0, 255, 102);
            popupSubtext.GetComponent<Text>().text = "Enemy sentinels will follow your sentinel for the next few seconds";
            popupSubtext.GetComponent<Text>().color = UITools.ColorFromRGB(0, 255, 102);
            generalManager.GetComponent<UIManager>().popupTimer = 3f;
        }

        Destroy(gameObject);
    }
}
