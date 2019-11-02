using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour {

    List<Sprite> sprites = new List<Sprite>();
    GameObject image;
    GameObject goText;
    GameObject blink;

    float blinkTimer = 255f;
    float deathAnimTimer = 0f;
    float textAppearTimer = 0f;

    // Use this for initialization
    void Start () {
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death1", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death2", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death3", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death4", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death5", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death6", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Sprites/Player/Guardian/Guardian Death7", typeof(Sprite)) as Sprite);

        image = GameObject.Find("Guardian");
        goText = GameObject.Find("Text");
        blink = GameObject.Find("White");

        GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/Death", typeof(AudioClip)) as AudioClip);

        goText.GetComponent<Text>().text = "YOU DIED" + "\n" + "You survived for " + Mathf.FloorToInt(GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().currScoreTime) + " seconds."
                                           + "\n" + "Your energy peaked at " + GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().currScoreEnergy + "." + "\n" +
                                           "You destroyed " + GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().killCount + " enemy sentinels." + "\n" +
                                           "Your final score was " +
                                           Mathf.RoundToInt(((GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().currScoreTime - 30) * GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().currScoreEnergy * GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().killCount / 10) / 10)
                                           + "." + "\n" +
                                           "Press Enter to try again." + "\n" + "Press Escape to return to the main menu.";
    }
	
	// Update is called once per frame
	void Update () {
        blinkTimer -= Time.deltaTime * 255f;
        if (blinkTimer < 0)
        {
            blinkTimer = 0;
        }

        blink.GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, Mathf.RoundToInt(blinkTimer));

        if (deathAnimTimer >= 1.5f)
        {
            deathAnimTimer = 1.5f;
            image.GetComponent<Image>().sprite = sprites[6];
            textAppearTimer += Time.deltaTime * 100f;
            if(Input.GetKey(KeyCode.Return))
            {
                GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().ResetScores();
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().ResetScores();
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            deathAnimTimer += Time.deltaTime;
            image.GetComponent<Image>().sprite = sprites[Mathf.FloorToInt(deathAnimTimer * 4f)];
        }

        if (textAppearTimer > 255f)
        {
            textAppearTimer = 255f;
        }

        goText.GetComponent<Text>().color = UITools.ColorFromRGB(255, 255, 255, Mathf.RoundToInt(textAppearTimer));
    }
}
