﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour {

    float spawnDirection;
    float spawnDistance;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRend;

    public float maxRange = 50;

    float lifespan;

    public bool followsSpecificDirection = false;
    public Vector2 specificDirection = new Vector2(0, 0);

    // Use this for initialization
    void Start () {
        spawnDistance = Random.Range(0, maxRange);
        spawnDirection = Random.Range(0, 360) / 180f * Mathf.PI;
        spriteRend = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        lifespan = Random.Range(2f, 3f);

        GameObject.Find("GeneralManager").GetComponent<ParticleLimiter>().particles.Add(gameObject);

        if (followsSpecificDirection)
        {
            rb2d.AddForce(specificDirection * spawnDistance);
        }
        else
        {
            rb2d.AddForce(new Vector2(Mathf.Cos(spawnDirection) * spawnDistance, Mathf.Sin(spawnDirection) * spawnDistance));
        }

        if (Random.Range(1, 3) == 1) {
            spriteRend.sprite = Resources.Load("Sprites/Environment/Particle10", typeof(Sprite)) as Sprite;
        }
        else
        {
            spriteRend.sprite = Resources.Load("Sprites/Environment/Particle15", typeof(Sprite)) as Sprite;
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
	void Update () {
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
