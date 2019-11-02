using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLimiter : MonoBehaviour {

    public bool limiting = false;

    public List<GameObject> particles = new List<GameObject>();

    // Use this for initialization
    void Start () {
        limiting = GameObject.Find("GlobalHolder").GetComponent<GlobalHolder>().particleLimiting;
	}
	
	// Update is called once per frame
	void Update () {
		if (limiting)
        {
            while (particles.Count > 100)
            {
                Destroy(particles[0]);
                particles.RemoveAt(0);
            }
        }
	}
}
