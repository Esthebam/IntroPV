using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    Rigidbody2D myRigidbody;

    public float KnockBackX;
    public float KnockBackY;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemigo")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (col.GetComponent<SpriteRenderer>().flipX == false)
            {
            myRigidbody.velocity = new Vector2(-KnockBackX, KnockBackY);
            }
            else
            {
            myRigidbody.velocity = new Vector2(KnockBackX, KnockBackY);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemigo")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
