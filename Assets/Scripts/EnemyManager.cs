using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private Animator enemyAnim;
    public float animDelay;

    public int enemyHealth;
    public int enemyValue;

    public static bool enemyDead;

    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bullet")
        {
            //enemyHealth -= BulletMovement.damage;
            if (enemyHealth <= 0)
            {
                enemyDead = true;
                enemyAnim.SetBool("isDead", true);
                Debug.Log(enemyValue);
                Destroy(gameObject, animDelay);
            }
        }
    }
}
