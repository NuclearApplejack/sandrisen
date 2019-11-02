using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadParticle : MonoBehaviour
{

    float spawnDirection;
    float spawnDistance;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRend;

    public float maxRange = 100;

    float lifespan;

    // Use this for initialization
    void Start()
    {
        spawnDistance = Random.Range(0, maxRange);
        spawnDirection = (Random.Range(0, 4) * Mathf.PI / 2) + Mathf.PI / 4;
        spriteRend = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        lifespan = Random.Range(2f, 3f);

        GameObject.Find("GeneralManager").GetComponent<ParticleLimiter>().particles.Add(gameObject);

        rb2d.AddForce(new Vector2(Mathf.Cos(spawnDirection) * spawnDistance, Mathf.Sin(spawnDirection) * spawnDistance));

        if (Random.Range(1, 3) == 1)
        {
            spriteRend.sprite = Resources.Load("Sprites/Environment/OverloadParticle10", typeof(Sprite)) as Sprite;
        }
        else
        {
            spriteRend.sprite = Resources.Load("Sprites/Environment/OverloadParticle15", typeof(Sprite)) as Sprite;
        }


        if (Random.Range(1, 3) == 1)
        {
            spriteRend.flipX = true;
        }

        if (Random.Range(1, 3) == 1)
        {
            spriteRend.flipY = true;
        }

        int angle = Random.Range(0, 360);
        transform.Rotate(Vector3.forward * angle);


    }

    // Update is called once per frame
    void Update()
    {
        if (lifespan < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifespan -= Time.deltaTime;
            transform.localScale -= new Vector3(0.003f, 0.003f, 0);
        }
    }
}
