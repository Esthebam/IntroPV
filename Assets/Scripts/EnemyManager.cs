﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

 

    private Animator enemyAnim;
    public float animDelay;

    public GameObject healthBar;
    private float currentHealth; 

    public AudioSource[] deathSounds;
    
    public float enemyHealth;
    public int enemyValue;

    public static bool enemyDead;

    void Start()
    {
        currentHealth = enemyHealth;
        enemyAnim = GetComponent<Animator>();
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bullet")
        {
            currentHealth -= BulletMovement.damage;

            if (!enemyDead)
            {
                float barLenght = currentHealth / enemyHealth;
				if (barLenght < 0) {
					barLenght = 0;
				}
                SetHealthBar(barLenght);
            }
            
            if (currentHealth <= 0)
            {
                deathSounds[Random.Range(0, deathSounds.Length)].Play();
                GetComponent<BoxCollider2D>().enabled = false;
                enemyDead = true;
                enemyAnim.SetBool("isDead", true);
                Destroy(gameObject, animDelay);
				GetComponent<LootDrop>().DropLot();
            }
			enemyDead = false;
        }
    }


    public void SetHealthBar(float enemyHealth)
    {
        healthBar.transform.localScale = new Vector3(enemyHealth, healthBar.transform.localScale.y , healthBar.transform.localScale.z);
    }
}
