using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour {

    public GameObject startPoint;
    public GameObject endPoint;

    public float enemySpeed;

    private bool isGoingRight;

	// Use this for initialization
	void Start () {
		if (!isGoingRight)
        {
            transform.position = startPoint.transform.position;
        }
        else
        {
            transform.position = endPoint.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!EnemyManager.enemyDead)
        {
            if (!isGoingRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, enemySpeed * Time.deltaTime);
                if (transform.position == endPoint.transform.position)
                {
                    isGoingRight = true;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, enemySpeed * Time.deltaTime);
                if (transform.position == startPoint.transform.position)
                {
                    isGoingRight = false;
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
	}


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
    }

}
