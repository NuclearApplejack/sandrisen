using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    new Camera camera;
    
    GameObject actionTaker;
    GameObject guide;
    float targetSize;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        actionTaker = GameObject.Find("ActionTaker");
        guide = GameObject.Find("Guide");
        targetSize = 4;
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 midpoint = (actionTaker.transform.position + guide.transform.position) / 2;
        midpoint = (actionTaker.transform.position + midpoint) / 2;
        //midpoint = (actionTaker.transform.position + midpoint) / 2;
        midpoint = new Vector3(midpoint.x, midpoint.y, -10);
        
        Vector3 currPoint = new Vector3((midpoint.x - transform.position.x) / 10f, (midpoint.y - transform.position.y) / 10f, 0);
        transform.position += currPoint;

        int rowDiff = Mathf.Abs(guide.GetComponent<Guide>().gridPosition.row - actionTaker.GetComponent<ActionTaker>().gridPosition.row);
        int colDiff = Mathf.Abs(guide.GetComponent<Guide>().gridPosition.column - actionTaker.GetComponent<ActionTaker>().gridPosition.column);
        if (rowDiff > colDiff)
        {
            targetSize = (rowDiff / 1.5f) + 3f;
        }
        else
        {
            targetSize = (colDiff / 1.5f) + 3f;
        }
        if (targetSize < 5.5f)
        {
            targetSize = 5.5f;
        }

        camera.orthographicSize += (targetSize - camera.orthographicSize) / 10f;
	}
}
