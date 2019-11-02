using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {

    public GridPosition gridPosition = new GridPosition(9, 9);
    public GameObject actiontaker;

    public bool facingRight = true;

    public float movementDelay = 0.2f;
    public float movementTimer = 0;

    Animator animator;

	// Use this for initialization
	void Start () {
        transform.Translate(gridPosition.column, -gridPosition.row, 0);
        actiontaker = GameObject.Find("ActionTaker");
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K))
        {
            actiontaker.GetComponent<ActionTaker>().Dash();
        }
        else if (movementTimer <= 0)
        {
            movementTimer = 0;
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                gridPosition.column += 1;
                

                movementTimer = movementDelay;

                actiontaker.GetComponent<ActionTaker>().Shoot(Direction.RIGHT);

                facingRight = true;

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                gridPosition.column -= 1;
                
                movementTimer = movementDelay;

                actiontaker.GetComponent<ActionTaker>().Shoot(Direction.LEFT);

                facingRight = false;

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                gridPosition.row -= 1;
                
                movementTimer = movementDelay;

                actiontaker.GetComponent<ActionTaker>().Shoot(Direction.UP);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                gridPosition.row += 1;
                
                movementTimer = movementDelay;

                actiontaker.GetComponent<ActionTaker>().Shoot(Direction.DOWN);
            }

            if (gridPosition.row < 0)
            {
                gridPosition.row = 0;
            }
            if (gridPosition.row > 19)
            {
                gridPosition.row = 19;
            }
            if (gridPosition.column < 0)
            {
                gridPosition.column = 0;
            }
            if (gridPosition.column > 19)
            {
                gridPosition.column = 19;
            }

            transform.Translate(new Vector3(gridPosition.column, -gridPosition.row, -3) - transform.position);

        }
        else
        {
            movementTimer -= Time.deltaTime;
            if (movementTimer <= 0)
            {
                movementTimer = 0;
            }
        }

        animator.SetBool("FacingRight", facingRight);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Item")
        {
            c.gameObject.GetComponent<Item>().ManageDestroy();
        }
    }
}
