using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 5f;
    public float speed = 2f;

    private Rigidbody2D myrb2d;

	// Use this for initialization
	void Start () {
        myrb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float direccion = Input.GetAxis("Horizontal");

        myrb2d.AddForce(Vector2.right * speed * direccion);

        if (myrb2d.velocity.x > maxSpeed)
        {
            myrb2d.velocity = new Vector2(maxSpeed, myrb2d.velocity.y);
        }
        if (myrb2d.velocity.x < -maxSpeed)
        {
            myrb2d.velocity = new Vector2(-maxSpeed, myrb2d.velocity.y);
        }
    }
}
