using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralEnemy : MonoBehaviour
{
    public bool isShuttingDown = false;

    
    public GameObject target;
    public GameObject actionTaker;
    public GameObject guide;
    public float speed = 0.03f;
    public Animator animator;
    public AudioSource audioS;
    public AudioClip damage;
    public AudioClip death1;
    public AudioClip death2;
    public bool hitPlayer = false;
    public bool isDead = false;

    public int powerupChance = 31;

    public int minCoins;
    public int maxCoins;


    public GameObject generalManager;

    // Use this for initialization
    public void Start()
    {
        target = GameObject.Find("ActionTaker");
        actionTaker = GameObject.Find("ActionTaker");
        guide = GameObject.Find("Guide");
        animator = GetComponent<Animator>();

        generalManager = GameObject.Find("GeneralManager");

        audioS = target.GetComponent<AudioSource>();
        damage = Resources.Load("SFX/Damage1", typeof(AudioClip)) as AudioClip;
        death1 = Resources.Load("SFX/EnemyDie1", typeof(AudioClip)) as AudioClip;
        death2 = Resources.Load("SFX/EnemyDie2", typeof(AudioClip)) as AudioClip;
    }


    // Update is called once per frame
    public void Update()
    {
        if (generalManager.GetComponent<PlayerEffectManager>().decoyTimer > 0)
        {
            target = guide;
        }
        else
        {
            target = actionTaker;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "ActionTaker" || (c.gameObject.name == "Guide" && GridPosition.ComparePos(guide.GetComponent<Guide>().gridPosition, actionTaker.GetComponent<ActionTaker>().gridPosition)))
        {
            hitPlayer = true;
            audioS.PlayOneShot(damage);
            generalManager.GetComponent<PlayerEffectManager>().damageTimer = 255f;

            ManageDestroy();

        }
    }

    public void ManageDestroy()
    {
        if (!isDead)
        {
            isDead = true;

            if (hitPlayer)
            {
                generalManager.GetComponent<ItemManager>().coinCount -= 10;
            }
            else
            {
                if (generalManager.GetComponent<ItemManager>().coinCount > 0)
                {
                    GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().killCount++;
                }

                int coinAmount = Random.Range(minCoins, maxCoins + 1);
                while (coinAmount > 0)
                {
                    coinAmount--;
                    GameObject coin = Instantiate(Resources.Load("Prefabs/Coin", typeof(GameObject)) as GameObject);
                    coin.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                }

                #region powerup spawns
                int spawnsSpeedUp = Random.Range(1, powerupChance);
                if (spawnsSpeedUp == 1)
                {
                    GameObject speedUp = Instantiate(Resources.Load("Prefabs/SpeedUp", typeof(GameObject)) as GameObject);
                    speedUp.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                }

                int spawnsOverload = Random.Range(1, powerupChance);
                if (spawnsOverload == 1)
                {
                    GameObject overload = Instantiate(Resources.Load("Prefabs/Overload", typeof(GameObject)) as GameObject);
                    overload.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                }

                int spawnsChargefield = Random.Range(1, powerupChance);
                if (spawnsChargefield == 1)
                {
                    GameObject overload = Instantiate(Resources.Load("Prefabs/Chargefield", typeof(GameObject)) as GameObject);
                    overload.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                }

                int spawnsDecoy = Random.Range(1, powerupChance);
                if (spawnsDecoy == 1)
                {
                    GameObject overload = Instantiate(Resources.Load("Prefabs/Decoy", typeof(GameObject)) as GameObject);
                    overload.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                }

                int spawnsBlast = Random.Range(1, powerupChance);
                if (spawnsBlast == 1)
                {
                    GameObject blast = Instantiate(Resources.Load("Prefabs/Blast", typeof(GameObject)) as GameObject);
                    blast.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                }
                #endregion
            }

            //--

            int particleCount = 20;

            while (particleCount > 0)
            {
                GameObject particle = Instantiate(Resources.Load("Prefabs/EParticle", typeof(GameObject)) as GameObject);
                particle.GetComponent<EParticle>().maxRange = 200;
                particle.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                particleCount--;
            }

            int r = Random.Range(1, 3);
            if (r == 1)
            {
                audioS.PlayOneShot(death1);
            }
            else
            {
                audioS.PlayOneShot(death2);
            }

            Destroy(gameObject);
        }
    }

}
