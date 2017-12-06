using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthManager : MonoBehaviour {
    
    Rigidbody2D myRigidbody;
	PlayerController player;
	[SerializeField]
	Slider healthBar;

	[SerializeField]
	Text healthText;

    public float KnockBackX;
    public float KnockBackY;

	public static HealthManager healthManager;

	public AudioSource[] damageSounds;
    public AudioSource fizzSound;
    public AudioSource sawSound;
    public AudioSource spikeSound;

    private Animator playerAnim;
    public float animDelay;

    public float playerHealth;
    public int enemyDamage;
    public static bool playerDead;
	public float maxHealth;
	public CircleCollider2D circulo;
	public int currentLifes;
	
	public bool invincible;


    // Use this for initialization
    void Start () {
		circulo = GameObject.FindObjectOfType<CircleCollider2D> ();
        playerDead = false;
        myRigidbody = GetComponent<Rigidbody2D>();
		player = GetComponent<PlayerController> ();
		healthManager = this;
        playerAnim = GetComponent<Animator>();
		maxHealth = playerHealth;
		healthBar.value = 100;
		currentLifes = 5;
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

    void ChequearAcido(Collider2D col)
    {
        if (col.tag == "Acid")
        {
            fizzSound.Play();
            col.GetComponent<ParticleSystem>().Play();
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + playerHealth.ToString(), Color.red, true);
            playerHealth -= maxHealth;
			healthBar.value = (playerHealth/maxHealth)* 100;
			transform.position = player.checkpoint;
        }

		if (col.gameObject.tag == "TurretBullet") {
			playerHealth -= 10f;
			damageSounds [Random.Range (0, damageSounds.Length)].Play ();
			StartCoroutine ("color");
			healthBar.value = (playerHealth / maxHealth) * 100;
			Knockback (col);
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + "10", Color.red, true);
			CameraShake.Shake(0.25f, 0.75f);
			Destroy(GameObject.FindWithTag("TurretBullet"));
		}

    }

    void ChequearEnemigos(Collider2D col)
    {
        if (col.tag == "Enemigo")
        {
            playerHealth -= enemyDamage;
			damageSounds [Random.Range (0, damageSounds.Length)].Play ();
            StartCoroutine("color");
            invincible = true;
            StartCoroutine("tiempoEspera");
            healthBar.value = (playerHealth / maxHealth) * 100;
            Knockback(col);
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + enemyDamage.ToString(), Color.red, false);
            CameraShake.Shake(0.25f, 0.75f);
        }
        if (col.tag == "EnemigoLegs")
        {
            playerHealth -= enemyDamage;
			damageSounds [Random.Range (0, damageSounds.Length)].Play ();
            StartCoroutine("color");
            invincible = true;
            StartCoroutine("tiempoEspera");
            healthBar.value = (playerHealth / maxHealth) * 100;
            Knockback(col);
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + enemyDamage.ToString(), Color.red, false);
            CameraShake.Shake(0.25f, 0.75f);
        }

		if (col.tag == "Turret" || col.tag == "Turret1" || col.tag == "Turret2")
		{
			//col.GetComponent<ParticleSystem>().Play();
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + playerHealth.ToString(), Color.red, true);
			playerHealth -= maxHealth;
			transform.position = new Vector3(-7, 0, 0);
			CameraShake.Shake (0.50f, 1f);
		}

	
    }

    void ChequearTrampas(Collider2D col)
    {
        if (col.gameObject.tag == "Saw")
        {
            col.GetComponent<ParticleSystem>().Play();
            sawSound.Play();
            playerHealth -= 5.00f;
            StartCoroutine("color");
            invincible = true;
            StartCoroutine("tiempoEspera");
            healthBar.value = (playerHealth / maxHealth) * 100;
            Knockback(col);
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + "5", Color.red, true);
            CameraShake.Shake(0.25f, 0.75f);
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
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + "5", Color.red, true);
            CameraShake.Shake(0.25f, 0.75f);
        }
			
    }

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Boss" && !invincible)
		{
			playerHealth -= 30f;
			damageSounds [Random.Range (0, damageSounds.Length)].Play ();
			StartCoroutine("color");
			invincible = true;
			StartCoroutine("tiempoEspera");
			healthBar.value = (playerHealth / maxHealth) * 100;
			myRigidbody.velocity = new Vector2(-10f, KnockBackY);
			CombatTextManager.Instance.CreateText(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), "-" + 30.ToString(), Color.red, true);
			CameraShake.Shake (0.50f, 1f);
		}
	}

    void OnTriggerStay2D(Collider2D col)
    {
		if (!invincible) 
		{
            ChequearEnemigos(col);
            ChequearTrampas(col);          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChequearAcido(collision);
    }

    IEnumerator tiempoEspera() {
		yield return new WaitForSeconds (1.2f);
		invincible = false;
	}


	IEnumerator color(){
		Color color = player.GetComponent<SpriteRenderer> ().color;

			GetComponent<SpriteRenderer> ().color = Color.red;
			yield return new WaitForSeconds (0.05f);
			GetComponent<SpriteRenderer> ().color = Color.white;
			yield return new WaitForSeconds (0.05f);
			GetComponent<SpriteRenderer> ().color = Color.red;
			yield return new WaitForSeconds (0.05f);
			GetComponent<SpriteRenderer> ().color = Color.white;

		if (player.powerUpActivos > 0) {
			GetComponent<SpriteRenderer> ().color = color;
		}

	}
	void Update()
	{
		if (playerHealth > 0 && currentLifes >= 0) {
			float healthNow= (playerHealth/maxHealth) * 100 ;
			healthText.text = Mathf.RoundToInt(healthNow) + "%";
		}

		if (playerHealth <= 0 && currentLifes > 0) 
		{
			float healthNow= (playerHealth/maxHealth) * 100 ;
			healthText.text = Mathf.RoundToInt(healthNow) + "%";
			currentLifes--;
			playerHealth = maxHealth;
			healthBar.value = (playerHealth/maxHealth)* 100;
			player.SendMessage ("restart");
		}
			
		
		if (playerHealth < 0 && currentLifes == 0) 
		{
			
			currentLifes--;
			healthText.text = 0 + " %";	
			healthBar.value = 0;
			//GetComponent<BoxCollider2D> ().enabled = false;
			circulo.enabled = false;
			playerDead = true;
			playerAnim.SetBool("isDead", true);
			player.SendMessage ("restart");


			//Component[] comps = GetComponents<Component>() as Component[];
			StartCoroutine ("gameOver");
			//foreach(Component comp in comps)
			//{
				//if (comp != gameObject.GetComponent<Transform>())
				//{
					//Destroy(comp,animDelay);
				//}
			//}
	

		}

		Destroy(GameObject.FindWithTag("TurretBullet"), 2f);
	
	}


	IEnumerator gameOver() {
		yield return new WaitForSeconds (2f);
			SceneManager.LoadScene ("GameOver");
	}

}
		

