using System.Collections;
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
	public static EnemyManager enemyManager;
	private gameMaster gm;
	private BossHealth bh;

    void Start()
    {
        currentHealth = enemyHealth;
        enemyAnim = GetComponent<Animator>();
		enemyManager = this;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bullet")
        {
            currentHealth -= BulletMovement.damage;
			if (BulletMovement.damage <= 5) {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y, transform.position.z), BulletMovement.damage.ToString (), Color.white, false);
			} else {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.red, true);
			}

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
				gm.points += 5;
            }
			enemyDead = false;
        }
    }


    public void SetHealthBar(float enemyHealth)
    {
        healthBar.transform.localScale = new Vector3(enemyHealth, healthBar.transform.localScale.y , healthBar.transform.localScale.z);
    }
}
