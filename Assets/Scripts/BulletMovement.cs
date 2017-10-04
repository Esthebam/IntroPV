using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    private Rigidbody2D myRigidbody2D;

    public GameObject player;
    private Transform playerTrans;
    public float bulletSpeed;
    public float bulletLife;

    public static int damage;
    public int damageRef;

    void Awake()
    {
        damage = damageRef;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
    }

    // Use this for initialization
    void Start()
    {
        if (playerTrans.localScale.x > 0)
        {
            // El player mira a la derecha
            myRigidbody2D.velocity = new Vector2(bulletSpeed, myRigidbody2D.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // El player mira a la izquierda
            myRigidbody2D.velocity = new Vector2(-bulletSpeed, myRigidbody2D.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Piso" || col.tag == "Enemigo")
        {
            GetComponent<ParticleSystem>().Play(); // Que haga el efecto de partículas
            GetComponent<SpriteRenderer>().enabled = false; // Que deje de verse la bala
            GetComponent<CircleCollider2D>().enabled = false; // Que deshabilite el collider para que no mate un enemigo atrás de otro
        }
    }
}