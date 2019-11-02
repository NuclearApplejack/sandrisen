using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    GameObject generalManager;
    GameObject coinText;
    GameObject guide;
    GameObject actionTaker;

    GameObject slashBar;
    GameObject dashBar;
    GameObject popupText;
    GameObject popupSubtext;

    GameObject dashCharge;
    List<Sprite> sprites = new List<Sprite>();
    float dashChargeTimer = 33f;

    public float popupTimer = 0;

	// Use this for initialization
	void Start () {
        coinText = GameObject.Find("CoinCountText");
        generalManager = GameObject.Find("GeneralManager");

        slashBar = GameObject.Find("SlashBar");
        dashBar = GameObject.Find("DashBar");

        guide = GameObject.Find("Guide");
        actionTaker = GameObject.Find("ActionTaker");

        popupText = GameObject.Find("PopupText");
        popupSubtext = GameObject.Find("PopupSubtext");

        dashCharge = GameObject.Find("DashCharge");

        #region list populating
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0001", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0002", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0003", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0004", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0005", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0006", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0007", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0008", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0009", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0010", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0011", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0012", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0013", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0014", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0015", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0016", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0017", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0018", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0019", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0020", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0021", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0022", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0023", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0024", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0025", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0026", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0027", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0028", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0029", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0030", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0031", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0032", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0033", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Environment/Dash Charge Effect/DashChargeEffect0034", typeof(Sprite)) as Sprite);
        #endregion

    }

    // Update is called once per frame
    void Update () {
        coinText.GetComponent<Text>().text = "Energy: " + generalManager.GetComponent<ItemManager>().coinCount;

        slashBar.GetComponent<RectTransform>().sizeDelta = new Vector2(500 - (500 * guide.GetComponent<Guide>().movementTimer / guide.GetComponent<Guide>().movementDelay), 40);
        dashBar.GetComponent<RectTransform>().sizeDelta = new Vector2(500 - (500 * actionTaker.GetComponent<ActionTaker>().dashTimer / actionTaker.GetComponent<ActionTaker>().dashDelay), 40);

        if (popupTimer > 0)
        {
            popupTimer -= Time.deltaTime;
        }
        else
        {
            popupText.GetComponent<Text>().text = "";
            popupSubtext.GetComponent<Text>().text = "";
        }

        if (generalManager.GetComponent<PlayerEffectManager>().speedUpTimer > 0)
        {
            slashBar.GetComponent<Image>().color = UITools.ColorFromRGB(178, 0, 255);
        }
        else
        {
            slashBar.GetComponent<Image>().color = UITools.ColorFromRGB(66, 150, 196);
        }

        if (generalManager.GetComponent<PlayerEffectManager>().overloadTimer > 0)
        {
            dashBar.GetComponent<Image>().color = UITools.ColorFromRGB(255, 157, 0);
            dashBar.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 40);
        }
        else
        {
            dashBar.GetComponent<Image>().color = UITools.ColorFromRGB(66, 150, 196);
        }

        
        dashCharge.GetComponent<Image>().sprite = sprites[Mathf.Min(Mathf.FloorToInt(dashChargeTimer), 33)];
        
        if (dashChargeTimer >= 34)
        {
            dashCharge.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            dashChargeTimer += Time.deltaTime * 50;
            dashCharge.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void DisplayDashEffect()
    {
        dashChargeTimer = 0f;
    }
}
