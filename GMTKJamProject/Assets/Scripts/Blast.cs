using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Blast : Item {

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
            generalManager.GetComponent<PlayerEffectManager>().TriggerBlast();
            generalManager.GetComponent<ItemManager>().coinCount += 5;
            popupText.GetComponent<Text>().text = "Blast!";
            popupText.GetComponent<Text>().color = UITools.ColorFromRGB(255, 0, 0);
            popupSubtext.GetComponent<Text>().text = "Boom.";
            popupSubtext.GetComponent<Text>().color = UITools.ColorFromRGB(255, 0, 0);
            generalManager.GetComponent<UIManager>().popupTimer = 3f;
        }

        Destroy(gameObject);
        
    }
}
