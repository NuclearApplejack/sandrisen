using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionTaker : MonoBehaviour {

    public GridPosition gridPosition = new GridPosition(9, 10);
    GameObject guide;
    GameObject generalManager;
    GameObject dashEffect;

    public float dashTimer = 0;
    public float dashDelay = 5f;
    public bool dashChargeShown = true;

    float dashEffectTimer = 0f;
    float dashParticleTimer = 0f;
    float dashParticleDelay = 0.07f;

    Animator animator;
    AudioSource audioS;

    AudioClip slash1;
    AudioClip slash2;
    AudioClip slash3;

    AudioClip dash1;
    AudioClip dash2;
    AudioClip dash3;

    bool facingRight = false;

    float shootRaycastTimer = 0;
    float shootRaycastMax = 0.1f;

    Direction shootingDirection = Direction.DOWN;

	// Use this for initialization
	void Start () {
        transform.Translate(gridPosition.column, -gridPosition.row, 0);
        guide = GameObject.Find("Guide");
        animator = GetComponent<Animator>();
        generalManager = GameObject.Find("GeneralManager");
        audioS = GetComponent<AudioSource>();
        dashEffect = GameObject.Find("DashEffect");

        slash1 = Resources.Load("SFX/Slash1", typeof(AudioClip)) as AudioClip;
        slash2 = Resources.Load("SFX/Slash2", typeof(AudioClip)) as AudioClip;
        slash3 = Resources.Load("SFX/Slash3", typeof(AudioClip)) as AudioClip;

        dash1 = Resources.Load("SFX/Dash1", typeof(AudioClip)) as AudioClip;
        dash2 = Resources.Load("SFX/Dash2", typeof(AudioClip)) as AudioClip;
        dash3 = Resources.Load("SFX/Dash3", typeof(AudioClip)) as AudioClip;

        GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/Confirm", typeof(AudioClip)) as AudioClip);
    }
	
	// Update is called once per frame
	void Update () {
        if (dashEffectTimer <= 0)
        {
            dashEffectTimer = 0;
        }
        else
        {
            dashEffectTimer -= Time.deltaTime * 255f;
            dashEffect.GetComponent<Image>().color = UITools.ColorFromRGB(255, 255, 255, Mathf.RoundToInt(dashEffectTimer));
        }

        ManageShooting();

        if (guide.GetComponent<Guide>().gridPosition.column > gridPosition.column)
        {
            facingRight = true;
        }
        else if (guide.GetComponent<Guide>().gridPosition.column < gridPosition.column)
        {
            facingRight = false;
        }

        animator.SetBool("FacingRight", facingRight);

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            dashTimer = 0;
            if (!dashChargeShown)
            {
                dashChargeShown = true;
                audioS.PlayOneShot(Resources.Load("SFX/DashCharge", typeof(AudioClip)) as AudioClip);
                generalManager.GetComponent<UIManager>().DisplayDashEffect();
                int particleCount = 15;

                while (particleCount > 0)
                {
                    GameObject particle = Instantiate(Resources.Load("Prefabs/DashParticle", typeof(GameObject)) as GameObject);
                    particle.GetComponent<DashParticle>().maxRange = 200;
                    particle.transform.position = new Vector3(guide.transform.position.x, guide.transform.position.y, 0);
                    particleCount--;
                }

                particleCount = 15;

                while (particleCount > 0)
                {
                    GameObject particle = Instantiate(Resources.Load("Prefabs/DashParticle", typeof(GameObject)) as GameObject);
                    particle.GetComponent<DashParticle>().maxRange = 200;
                    particle.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    particleCount--;
                }
            }

            if (dashParticleTimer <= 0)
            {
                dashParticleTimer = dashParticleDelay;

                GameObject particle1 = Instantiate(Resources.Load("Prefabs/DashParticle", typeof(GameObject)) as GameObject);
                particle1.GetComponent<DashParticle>().maxRange = (Mathf.Sqrt(Mathf.Sqrt(Vector3.Distance(transform.position, guide.transform.position) / 2)) * 20) + 10;
                particle1.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                particle1.GetComponent<DashParticle>().followsSpecificDirection = true;
                particle1.GetComponent<DashParticle>().specificDirection = guide.transform.position - transform.position;

                GameObject particle2 = Instantiate(Resources.Load("Prefabs/DashParticle", typeof(GameObject)) as GameObject);
                particle2.GetComponent<DashParticle>().maxRange = (Mathf.Sqrt(Mathf.Sqrt(Vector3.Distance(transform.position, guide.transform.position) / 2)) * 20) + 10;
                particle2.transform.position = new Vector3(guide.transform.position.x, guide.transform.position.y, 0);
                particle2.GetComponent<DashParticle>().followsSpecificDirection = true;
                particle2.GetComponent<DashParticle>().specificDirection = transform.position - guide.transform.position;
            }
            else
            {
                dashParticleTimer -= Time.deltaTime;
            }
        }
        
        
    }

    public void Shoot(Direction d)
    {

        animator.SetTrigger("Slash");
        shootingDirection = d;
        shootRaycastTimer = shootRaycastMax;

        int r = Random.Range(1, 4);
        if (r == 1)
        {
            audioS.PlayOneShot(slash1);
        }
        else if (r == 2)
        {
            audioS.PlayOneShot(slash2);
        }
        else
        {
            audioS.PlayOneShot(slash3);
        }



        
    }

    public void ManageShooting()
    {
        if (shootRaycastTimer > 0)
        {

            if (shootingDirection == Direction.DOWN)
            {
                RaycastHit2D[] raycast1 = Physics2D.RaycastAll(transform.position + new Vector3(-0.7f, 0, 0), new Vector3(0, -2.5f, 0), 2.5f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast2 = Physics2D.RaycastAll(transform.position + new Vector3(0.7f, 0, 0), new Vector3(0, -2.5f, 0), 2.5f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast3 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0, 0), new Vector3(0, -2.5f, 0), 2.5f, LayerMask.GetMask("Enemies"));

                foreach (RaycastHit2D r in raycast1)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast2)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast3)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }

                animator.SetInteger("SlashDir", 0);


            }
            else if (shootingDirection == Direction.LEFT)
            {
                RaycastHit2D[] raycast1 = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.6f, 0), new Vector3(-2f, 0, 0), 2f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast2 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.6f, 0), new Vector3(-2f, 0, 0), 2f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast3 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0, 0), new Vector3(-2.5f, 0, 0), 2.5f, LayerMask.GetMask("Enemies"));

                foreach (RaycastHit2D r in raycast1)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast2)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast3)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }

                animator.SetInteger("SlashDir", 1);
            }
            else if (shootingDirection == Direction.RIGHT)
            {
                RaycastHit2D[] raycast1 = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.6f, 0), new Vector3(2f, 0, 0), 2f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast2 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.6f, 0), new Vector3(2f, 0, 0), 2f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast3 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0, 0), new Vector3(2.5f, 0, 0), 2.5f, LayerMask.GetMask("Enemies"));

                foreach (RaycastHit2D r in raycast1)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast2)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast3)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }

                animator.SetInteger("SlashDir", 2);
            }
            else
            {
                RaycastHit2D[] raycast1 = Physics2D.RaycastAll(transform.position + new Vector3(-0.7f, 0, 0), new Vector3(0, 2.5f, 0), 2.5f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast2 = Physics2D.RaycastAll(transform.position + new Vector3(0.7f, 0, 0), new Vector3(0, 2.5f, 0), 2.5f, LayerMask.GetMask("Enemies"));
                RaycastHit2D[] raycast3 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0, 0), new Vector3(0, 2.5f, 0), 2.5f, LayerMask.GetMask("Enemies"));

                Debug.DrawRay(transform.position + new Vector3(-0.7f, 0, 0), new Vector3(0, 2f, 0), new Color(1, 1, 1, 1), 10f);
                Debug.DrawRay(transform.position + new Vector3(0.7f, 0, 0), new Vector3(0, 2f, 0), new Color(1, 1, 1, 1), 10f);

                foreach (RaycastHit2D r in raycast1)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast2)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }
                foreach (RaycastHit2D r in raycast3)
                {
                    if (r.collider != null)
                    {
                        r.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                    }
                }

                animator.SetInteger("SlashDir", 3);
            }

            shootRaycastTimer -= Time.deltaTime;
        }

    }

    public void Dash()
    {
        if (dashTimer <= 0 || generalManager.GetComponent<PlayerEffectManager>().overloadTimer > 0)
        {

            dashTimer = 0;
            dashChargeShown = false;
            dashEffectTimer = 255f;

            int r = Random.Range(1, 4);
            if (r == 1)
            {
                audioS.PlayOneShot(dash1);
            }
            else if (r == 2)
            {
                audioS.PlayOneShot(dash2);
            }
            else
            {
                audioS.PlayOneShot(dash3);
            }

            RaycastHit2D[] raycast1 = Physics2D.RaycastAll(transform.position, guide.transform.position - transform.position, Vector2.Distance(transform.position, guide.transform.position));
            RaycastHit2D[] raycast2 = Physics2D.RaycastAll(transform.position + new Vector3(0.5f, 0, 0), guide.transform.position - transform.position + new Vector3(0.5f, 0, 0), Vector2.Distance(transform.position, guide.transform.position));
            RaycastHit2D[] raycast3 = Physics2D.RaycastAll(transform.position + new Vector3(-0.5f, 0, 0), guide.transform.position - transform.position + new Vector3(-0.5f, 0, 0), Vector2.Distance(transform.position, guide.transform.position));
            RaycastHit2D[] raycast4 = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.5f, 0), guide.transform.position - transform.position + new Vector3(0, 0.5f, 0), Vector2.Distance(transform.position, guide.transform.position));
            RaycastHit2D[] raycast5 = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.5f, 0), guide.transform.position - transform.position + new Vector3(0, -0.5f, 0), Vector2.Distance(transform.position, guide.transform.position));



            foreach (RaycastHit2D rch2d in raycast1)
            {

                if (rch2d.transform.gameObject.tag == "Enemy")
                {
                   rch2d.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                }
            }
            foreach (RaycastHit2D rch2d in raycast2)
            {

                if (rch2d.transform.gameObject.tag == "Enemy")
                {
                    rch2d.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                }
            }
            foreach (RaycastHit2D rch2d in raycast3)
            {

                if (rch2d.transform.gameObject.tag == "Enemy")
                {
                    rch2d.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                }
            }
            foreach (RaycastHit2D rch2d in raycast4)
            {

                if (rch2d.transform.gameObject.tag == "Enemy")
                {
                    rch2d.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                }
            }
            foreach (RaycastHit2D rch2d in raycast5)
            {

                if (rch2d.transform.gameObject.tag == "Enemy")
                {
                    rch2d.transform.gameObject.GetComponent<GeneralEnemy>().ManageDestroy();
                }
            }

            int totalParticleCount = Mathf.RoundToInt(Vector2.Distance(transform.position, guide.transform.position) * 10);
            int particleCount = totalParticleCount;
            float particleSpacing = Vector2.Distance(transform.position, guide.transform.position) / totalParticleCount;
            Vector3 target = new Vector3(guide.transform.position.x, guide.transform.position.y, 0);

            GameObject particleSpawner = new GameObject("ParticleSpawner");
            particleSpawner.transform.position = transform.position;

            while (particleCount > 0)
            {
                GameObject particle = Instantiate(Resources.Load("Prefabs/DashParticle", typeof(GameObject)) as GameObject);
                particle.transform.position = particleSpawner.transform.position;
                particleSpawner.transform.position = Vector3.MoveTowards(particleSpawner.transform.position, target, particleSpacing);
                particleCount--;
            }

            particleCount = 20;

            while (particleCount > 0)
            {
                GameObject particle = Instantiate(Resources.Load("Prefabs/DashParticle", typeof(GameObject)) as GameObject);
                particle.GetComponent<DashParticle>().maxRange = 300;
                particle.transform.position = new Vector3(guide.transform.position.x, guide.transform.position.y, 0);
                particleCount--;
            }

            Destroy(particleSpawner);

            gridPosition.row = guide.GetComponent<Guide>().gridPosition.row;
            gridPosition.column = guide.GetComponent<Guide>().gridPosition.column;
            transform.Translate(new Vector3(gridPosition.column, -gridPosition.row, -2) - transform.position);

            dashTimer = dashDelay;
        }
            
        
        
    }
}
