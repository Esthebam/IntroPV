using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLegManager : MonoBehaviour
{
	private Rigidbody2D myRigidbody;

    private Animator enemyAnim;
    public float animDelay;

    public GameObject healthBar;
    private float currentHealth;
    
    public Transform enemy;

    public float enemyHealth;
    public int enemyValue;

    public static bool enemyDead;
	private gameMaster gm;

    void Start()
    {
        currentHealth = enemyHealth;
		enemyAnim = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
		myRigidbody=  GetComponent<Rigidbody2D> ();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            currentHealth -= BulletMovement.damage;
			if (BulletMovement.damage <= 5) {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.white, false);
			} else {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.red, true);
			}

            if (!enemyDead)
            {
                float barLenght = currentHealth / enemyHealth;
                SetHealthBar(barLenght);
            }

            if (currentHealth <= 0)
            {
				//EnemyManager.enemyManager.deathSounds[Random.Range(0, EnemyManager.enemyManager.deathSounds.Length)].Play();
                GetComponent<BoxCollider2D>().enabled = false;
                transform.GetChild(1).gameObject.SetActive(false);
                enemyDead = true;
                enemyAnim.SetBool("isDead", true);
				myRigidbody.gravityScale = 0;
                GetComponent<LootDrop>().DropLot();
                Destroy(gameObject, animDelay);
				gm.points += 5;
                
            }
            enemyDead = false;
        }
    }


    public void SetHealthBar(float enemyHealth)
    {
        healthBar.transform.localScale = new Vector3(enemyHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
