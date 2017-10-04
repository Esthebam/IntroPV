using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

 

    private Animator enemyAnim;
    public float animDelay;

    public GameObject healthBar;
    private float currentHealth; 
	public GameObject powerUpPrefab;

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
                SetHealthBar(barLenght);
            }
            if (currentHealth <= 0)
            {
                enemyDead = true;
                enemyAnim.SetBool("isDead", true);
                Debug.Log(enemyValue);
                Destroy(gameObject, animDelay);
				Instantiate (powerUpPrefab, enemyAnim.rootPosition, enemyAnim.targetRotation);
            }
        }
    }

    public void SetHealthBar(float enemyHealth)
    {
        healthBar.transform.localScale = new Vector3(enemyHealth, healthBar.transform.localScale.y , healthBar.transform.localScale.z);
    }
}
