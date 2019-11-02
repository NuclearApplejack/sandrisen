using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Overload : Item
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

            generalManager.GetComponent<PlayerEffectManager>().overloadTimer += 5f;
            generalManager.GetComponent<ItemManager>().coinCount += 5;
            popupText.GetComponent<Text>().text = "Overload!";
            popupText.GetComponent<Text>().color = UITools.ColorFromRGB(255, 157, 0);
            popupSubtext.GetComponent<Text>().text = "Unlimited dashing for the next few seconds";
            popupSubtext.GetComponent<Text>().color = UITools.ColorFromRGB(255, 157, 0);
            generalManager.GetComponent<UIManager>().popupTimer = 3f;
        }

        Destroy(gameObject);
    }
}
