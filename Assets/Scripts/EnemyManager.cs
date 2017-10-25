using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

 

    private Animator enemyAnim;
    public float animDelay;

    public GameObject healthBar;
    private float currentHealth; 
	public GameObject powerUpPrefab;
	public GameObject powerUpVidaPrefab;
	public GameObject powerUpDmgPrefab;
	public Transform enemy;


    public float enemyHealth;
    public int enemyValue;

    public static bool enemyDead;

    void Start()
    {
        currentHealth = enemyHealth;
        enemyAnim = GetComponent<Animator>();
		enemy = GetComponent<Transform> ();
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
				GetComponent<BoxCollider2D>().enabled = false;
                enemyDead = true;
                enemyAnim.SetBool("isDead", true);
                Destroy(gameObject, animDelay);
				Instantiate (powerUpPrefab, new Vector3(enemy.position.x + 3, enemy.position.y + 1, enemy.position.z), enemyAnim.targetRotation);
				Instantiate (powerUpVidaPrefab, new Vector3(enemy.position.x, enemy.position.y + 1, enemy.position.z), enemyAnim.targetRotation);
				Instantiate (powerUpDmgPrefab, new Vector3(enemy.position.x + 1, enemy.position.y + 1, enemy.position.z), enemyAnim.targetRotation);
            }
			enemyDead = false;
        }
    }


    public void SetHealthBar(float enemyHealth)
    {
        healthBar.transform.localScale = new Vector3(enemyHealth, healthBar.transform.localScale.y , healthBar.transform.localScale.z);
    }
}
