using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffectManager : MonoBehaviour {

    public float speedUpTimer = 0;
    public float overloadTimer = 0;
    public float chargefieldTimer = 0;
    public float decoyTimer = 0;
    GameObject guide;
    GameObject damageEffect;
    GameObject actionTaker;
    float particleSpawnTimer = 0f;
    float particleSpawnDelay = 0.05f;

    GameObject blastFlash;
    public float blastFlashTimer = 0f;

    public float damageTimer = 0;

    GameObject generalManager;

	// Use this for initialization
	void Start () {
        guide = GameObject.Find("Guide");
        damageEffect = GameObject.Find("DamageEffect");
        blastFlash = GameObject.Find("BlastFlash");
        actionTaker = GameObject.Find("ActionTaker");
        generalManager = GameObject.Find("GeneralManager");
    }
	
	// Update is called once per frame
	void Update () {
        ManageParticleSpawn();

        if (blastFlashTimer <= 0)
        {
            blastFlashTimer = 0;
        }
        else
        {
            blastFlashTimer -= Time.deltaTime * 255f;
            blastFlash.GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, Mathf.RoundToInt(blastFlashTimer));
        }

		if (speedUpTimer > 0)
        {
            speedUpTimer -= Time.deltaTime;
            guide.GetComponent<Guide>().movementDelay = 0.15f;
        }
        else
        {
            guide.GetComponent<Guide>().movementDelay = 0.3f;
        }


        if (overloadTimer > 0)
        {
            overloadTimer -= Time.deltaTime;
        }

        if (chargefieldTimer > 0)
        {
            chargefieldTimer -= Time.deltaTime;
            
        }
        else
        {
            
        }


        if (decoyTimer > 0)
        {
            decoyTimer -= Time.deltaTime;
        }
        else
        {
            
        }


        if (damageTimer <= 0)
        {
            damageTimer = 0;
        }
        else
        {
            damageTimer -= Time.deltaTime * 200;
        }

        if (generalManager.GetComponent<ItemManager>().coinCount < 20)
        {
            damageEffect.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            damageTimer = 255f;
        }
        else
        {
            damageEffect.GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, Mathf.RoundToInt(damageTimer));
        }
        
    }

    public void TriggerBlast()
    {
        blastFlashTimer = 255f;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (g.transform.position.x >= -1.5f && g.transform.position.x <= 20.5f && g.transform.position.y >= -20.5f && g.transform.position.y <= 1.5f)
            {
                g.GetComponent<GeneralEnemy>().ManageDestroy();
            }
        }


    }

    public void ManageParticleSpawn()
    {
        if (particleSpawnTimer <= 0)
        {
            if (speedUpTimer > 0)
            {
                particleSpawnTimer = particleSpawnDelay;

                int particleCount = 3;
                while (particleCount > 0)
                {
                    particleCount--;

                    GameObject particle = Instantiate(Resources.Load("Prefabs/SpeedUpParticle", typeof(GameObject)) as GameObject);
                    particle.GetComponent<SpeedUpParticle>().maxRange = 350;
                    particle.transform.position = new Vector3(actionTaker.transform.position.x, actionTaker.transform.position.y, 0);
                }
            }

            if (overloadTimer > 0)
            {
                particleSpawnTimer = particleSpawnDelay;

                int particleCount = 3;
                while (particleCount > 0)
                {
                    particleCount--;

                    GameObject particle1 = Instantiate(Resources.Load("Prefabs/OverloadParticle", typeof(GameObject)) as GameObject);
                    particle1.GetComponent<OverloadParticle>().maxRange = 350;
                    particle1.transform.position = new Vector3(actionTaker.transform.position.x, actionTaker.transform.position.y, 0);
                }
            }

            if (decoyTimer > 0)
            {
                particleSpawnTimer = particleSpawnDelay;

                int particleCount = 3;
                while (particleCount > 0)
                {
                    particleCount--;

                    GameObject particle1 = Instantiate(Resources.Load("Prefabs/DecoyParticle", typeof(GameObject)) as GameObject);
                    particle1.GetComponent<DecoyParticle>().maxRange = 350;
                    particle1.transform.position = new Vector3(guide.transform.position.x, guide.transform.position.y, 0);
                }
            }

            if (chargefieldTimer > 0)
            {
                particleSpawnTimer = particleSpawnDelay;

                int particleCount = 3;
                while (particleCount > 0)
                {
                    particleCount--;

                    GameObject particle1 = Instantiate(Resources.Load("Prefabs/ChargefieldParticle", typeof(GameObject)) as GameObject);
                    particle1.GetComponent<ChargefieldParticle>().maxRange = 350;
                    particle1.transform.position = new Vector3(guide.transform.position.x, guide.transform.position.y, 0);
                }
            }
        }
        else
        {
            particleSpawnTimer -= Time.deltaTime;
        }
    }
}
