using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour {
    
    Rigidbody2D myRigidbody;

	[SerializeField]
	Slider healthBar;

	[SerializeField]
	Text healthText;

    public float KnockBackX;
    public float KnockBackY;

	public static HealthManager healthManager;

    public AudioSource fizzSound;
    public AudioSource sawSound;
    public AudioSource spikeSound;

    private Animator playerAnim;
    public float animDelay;

    public float playerHealth;
    public int enemyDamage;
    public static bool playerDead;
	public float maxHealth;
	public Vidas vidas;
	public int vida = 2;
	
	public bool invincible;



    // Use this for initialization
    void Start () {
		vidas = GameObject.FindObjectOfType<Vidas> ();
        playerDead = false;
        myRigidbody = GetComponent<Rigidbody2D>();
		healthManager = this;
        playerAnim = GetComponent<Animator>();
		//player = GameObject.FindGameObjectWithTag ("Player");
		maxHealth = playerHealth;
		healthBar.value = 100;	
    }


    void Knockback(Collider2D col)
    {
        if (col.GetComponent<SpriteRenderer>().flipX == false)
        {
            myRigidbody.velocity = new Vector2(-KnockBackX, KnockBackY);
        }
        else
        {
            myRigidbody.velocity = new Vector2(KnockBackX, KnockBackY);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Acid")
        {
            fizzSound.Play();
            col.GetComponent<ParticleSystem>().Play();
            playerHealth -= maxHealth;
            healthBar.value = playerHealth;
            playerDead = true;
        }
		if (!invincible) 
		{
			if(col.tag == "Enemigo")
			{
				playerHealth -= enemyDamage;
				StartCoroutine ("color");
				invincible = true;
				StartCoroutine ("tiempoEspera");
				healthBar.value = (playerHealth/maxHealth)* 100;
                Knockback(col);
                CameraShake.Shake (0.25f, 0.75f); 
            }

            if (col.tag == "EnemigoLegs")
            {
                playerHealth -= enemyDamage;
                StartCoroutine("color");
                invincible = true;
                StartCoroutine("tiempoEspera");
				healthBar.value = (playerHealth/maxHealth)* 100;
                Knockback(col);
                CameraShake.Shake (0.25f, 0.75f);
            }

            if (col.gameObject.tag == "Saw") 
			{
                col.GetComponent<ParticleSystem>().Play();
                sawSound.Play();
                playerHealth -= 5.00f;
				StartCoroutine ("color");
				invincible = true;
				StartCoroutine ("tiempoEspera");
				healthBar.value = (playerHealth/maxHealth)* 100;
                Knockback(col);
                CameraShake.Shake (0.25f, 0.75f);
			}

            if (col.gameObject.tag == "Spike")
            {
                col.GetComponent<ParticleSystem>().Play();
                spikeSound.Play();
                playerHealth -= 5.00f;
                StartCoroutine("color");
                invincible = true;
                StartCoroutine("tiempoEspera");
                healthBar.value = (playerHealth / maxHealth) * 100;
                Knockback(col);
                CameraShake.Shake(0.25f, 0.75f);
            }
        }

    }

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
		healthText.text = (playerHealth/maxHealth) * 100 + " %";

		if (playerHealth <= 0 && vida > 0) 
		{
			vida--;
			vidas.cambioVida(vida);
			playerHealth = 13;
			healthBar.value = (playerHealth/maxHealth)* 100;
		}
			
		
		if (playerHealth <= 0 && vida == 0) 
		{
			vidas.cambioVida (vida--);
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
	
			//SceneManager.LoadScene ("GameOver");
		}
	
	}
		
}
		

