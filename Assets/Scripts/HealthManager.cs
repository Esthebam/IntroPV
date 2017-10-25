using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    
    Rigidbody2D myRigidbody;

	[SerializeField]
	Slider healthBar;

	[SerializeField]
	Text healthText;

    public float KnockBackX;
    public float KnockBackY;

	public static HealthManager healthManager;


    private Animator playerAnim;
    public float animDelay;

    public float playerHealth;
    public int enemyDamage;
	public GameObject player;
    public static bool playerDead;
	



	float maxHealth = 100;


	public bool invincible;

    // Use this for initialization
    void Start () {
        playerDead = false;
        myRigidbody = GetComponent<Rigidbody2D>();
		healthManager = this;
        playerAnim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");
		healthBar.value = maxHealth;
		
    }
		

    void OnTriggerEnter2D(Collider2D col)
    {
		if (!invincible) 
		{
			if(col.tag == "Enemigo")
			{
				playerHealth -= enemyDamage;
					StartCoroutine ("color");
					invincible = true;
					StartCoroutine ("tiempoEspera");
				healthBar.value = playerHealth;
					if (col.GetComponent<SpriteRenderer>().flipX == false)
					{
						myRigidbody.velocity = new Vector2(-KnockBackX, KnockBackY);
					}
					else
					{
						myRigidbody.velocity = new Vector2(KnockBackX, KnockBackY);
		

                }

				CameraShake.Shake (0.25f, 0.75f);
             
			}


            if (col.tag == "EnemigoLegs")
            {
                playerHealth -= enemyDamage;
                StartCoroutine("color");
                invincible = true;
                StartCoroutine("tiempoEspera");
                healthBar.value = playerHealth;
                if (myRigidbody.GetComponent<SpriteRenderer>().flipX == false)
                {
                    myRigidbody.velocity = new Vector2(-KnockBackX, KnockBackY);
                }
                else
                {
                    myRigidbody.velocity = new Vector2(KnockBackX, KnockBackY);


                }

				CameraShake.Shake (0.25f, 0.75f);
            }

				

            if (col.gameObject.tag == "Saw") 
			{
				playerHealth -= 5.00f;
				StartCoroutine ("color");
				invincible = true;
				StartCoroutine ("tiempoEspera");
				healthBar.value = playerHealth;
				if (myRigidbody.GetComponent<SpriteRenderer>().flipX == false)
				{
					myRigidbody.velocity = new Vector2(-KnockBackX, KnockBackY);
				}
				else
				{
					myRigidbody.velocity = new Vector2(KnockBackX, KnockBackY);


				}

				CameraShake.Shake (0.25f, 0.75f);
			}
		}

    }
	/*
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemigo")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
*/
	IEnumerator tiempoEspera() {
		yield return new WaitForSeconds (1.2f);
		invincible = false;
	}

	IEnumerator color(){
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}


	void Update()
	{
		healthText.text = playerHealth.ToString () + " %";

		if (playerHealth <= 0) 
		{
			healthText.text = 0 + " %";	
		GetComponent<BoxCollider2D>().enabled = false;
		playerDead = true;
		playerAnim.SetBool("isDead", true);
		Component[] comps = GetComponents<Component>() as Component[];
		foreach(Component comp in comps)
		{
			if (comp != gameObject.GetComponent<Transform>())
			{
				Destroy(comp,animDelay);
			}
			}
		}
	}
}
		

