﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Slider powerUpDmgBar;

    [SerializeField]
    Slider powerUpSaltoBar;

    [SerializeField]
    Slider powerUpVelBar;

    [SerializeField]
    Slider powerUpVidaBar;

    public float dmgTimer;
    public float saltoTimer;
    public float velTimer;
    public float vidaTimer;

    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool tocandoPiso;
    public float fuerzaSalto = 6.5f;
    public bool endPoint;

    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    public AudioSource[] shootingSounds;
    public AudioSource powerUpSaltoSound;
    public AudioSource powerUpDañoSound;
    public AudioSource powerUpVelocidadSound;
    public AudioSource powerUpVidaSound;

    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private bool jump;
    private bool seAgacha;
	public bool estaDisparando;
	private Coroutine coroutineSalto;
	private Coroutine coroutineVel; 
	private Coroutine coroutineDanio;
	private Coroutine coroutineInvencible;
	public float coolDown;
	public bool enCoolDown;


    // Use this for initialization
    void Start()
    {

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
		estaDisparando = true;
        powerUpDmgBar.value = 0;
        powerUpSaltoBar.value = 0;
        powerUpVelBar.value = 0;
        powerUpVidaBar.value = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if(HealthManager.playerDead == false)
        {
            Movement();
            PlayerShooting();
        }

        if (saltoTimer > 0)
        {
            saltoTimer -= Time.deltaTime;
            powerUpSaltoBar.value = saltoTimer;
        }
        if (vidaTimer > 0)
        {
            vidaTimer -= Time.deltaTime;
            powerUpVidaBar.value = vidaTimer;
        }
        if (velTimer > 0)
        {
            velTimer -= Time.deltaTime;
            powerUpVelBar.value = velTimer;
        }
        if (dmgTimer > 0)
        {
            dmgTimer -= Time.deltaTime;
            powerUpDmgBar.value = dmgTimer;
        }


    }

    private void Movement()
    {
        // Hacemos que cambie el sprite pero comprobando siempre contra un valor positivo (0.1).
        // Por eso usamos Abs (valor absoluto)

        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody2D.velocity.x));
        myAnimator.SetBool("TocandoPiso", tocandoPiso);
        myAnimator.SetBool("SeAgacha", seAgacha);
	

		if (Input.GetKey(KeyCode.DownArrow) && tocandoPiso && !Pausa.pauseEnabled)
        {
            seAgacha = true;
        }
        else
        {
            seAgacha = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && tocandoPiso)
        {
            jump = true;
        }

        float direccion = Input.GetAxis("Horizontal");


        myRigidbody2D.AddForce(Vector2.right * speed * direccion);

        // Clamp toma un valor y le aplica un filtro (un valor mínimo y un valor máximo)
        float limiteVelocidad = Mathf.Clamp(myRigidbody2D.velocity.x, -maxSpeed, maxSpeed);
        myRigidbody2D.velocity = new Vector2(limiteVelocidad, myRigidbody2D.velocity.y);

        if (direccion > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (direccion < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (jump)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0); // Para que cancele la velocidad vertical y no se produzcan "saltos dobles"
            myRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void FixedUpdate()
    {
		
        Vector3 fixedVelocity = myRigidbody2D.velocity;
        fixedVelocity.x *= 0.75f;

        if (tocandoPiso)
        {
            myRigidbody2D.velocity = fixedVelocity;
        }

        // Lo de arriba es para solucionar que no se mueva siempre, ya que pusimos
        // que las plataformas no tengan fricción.

        
    }


    public void PlayerShooting()
	{
		
		if (Input.GetMouseButtonDown (0) && !Pausa.pauseEnabled && !enCoolDown) 
		{
			enCoolDown = true;
			myAnimator.SetBool ("Disparo", true);
			Instantiate (bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
			shootingSounds [Random.Range (0, shootingSounds.Length)].Play ();

			if (estaDisparando) {
				speed = 25f;
				maxSpeed = 0.5f;
				StartCoroutine ("slow");
			}
				
			StartCoroutine ("cd");

		} else if (Input.GetMouseButtonUp (0)) {
			myAnimator.SetBool ("Disparo", false);
		}

	
	
    }
    /*
    private void OnBecameInvisible()
    // Sólo para las pruebas
    {
        transform.position = new Vector3(-7, 0, 0);
    }
    */

	private void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "PowerUpSalto") {
            powerUpSaltoSound.Play();
            powerUpSaltoBar.transform.parent.gameObject.SetActive(true);
            saltoTimer = 5;
			fuerzaSalto = 15f;
			GetComponent<SpriteRenderer> ().color = Color.yellow;
			Destroy (col.gameObject);
			if (coroutineSalto != null)
				StopCoroutine (coroutineSalto);
			coroutineSalto = StartCoroutine ("tiempoEspera");
		}

		if (col.gameObject.tag == "PowerUpVida") {
            powerUpVidaSound.Play();
            powerUpVidaBar.transform.parent.gameObject.SetActive(true);
            vidaTimer = 5;
            HealthManager.healthManager.invincible = true;
			GetComponent<SpriteRenderer> ().color = Color.green;
			Destroy (col.gameObject);
			if (coroutineInvencible != null)
				StopCoroutine (coroutineInvencible);
			coroutineInvencible = StartCoroutine ("vida"); 
		}

		if (col.gameObject.tag == "PowerUpVel") {
            powerUpVelocidadSound.Play();
            powerUpVelBar.transform.parent.gameObject.SetActive(true);
            velTimer = 5;
            maxSpeed = 6;
			GetComponent<SpriteRenderer> ().color = Color.blue;
			estaDisparando = false;
			Destroy (col.gameObject);
			if (coroutineVel != null)
				StopCoroutine (coroutineVel);
			coroutineVel = StartCoroutine ("vel");
		}

		if (col.gameObject.tag == "PowerUpDmg") {
            powerUpDañoSound.Play();
            powerUpDmgBar.transform.parent.gameObject.SetActive(true);
            dmgTimer = 5;
            bulletPrefab.GetComponent<BulletMovement>().damageRef = 20;
            GetComponent<SpriteRenderer> ().color = Color.gray;
			Destroy (col.gameObject);
			if (coroutineDanio != null)
				StopCoroutine (coroutineDanio);
			coroutineDanio = StartCoroutine ("dmg");
		}

	}


	IEnumerator tiempoEspera() {
		yield return new WaitForSeconds (5);
        powerUpSaltoBar.transform.parent.gameObject.SetActive(false);
        powerUpSaltoBar.value = 0;
        saltoTimer = 0;
        fuerzaSalto = 9.25f;
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator vida() {
		yield return new WaitForSeconds (5);
        powerUpVidaBar.transform.parent.gameObject.SetActive(false);
        powerUpVidaBar.value = 0;
        vidaTimer = 0;
        HealthManager.healthManager.invincible = false;
		GetComponent<SpriteRenderer> ().color = Color.white;
	}

	IEnumerator slow() {
		yield return new WaitForSeconds (0.25f);
		speed = 75f; 
		maxSpeed = 3f;
	}

	IEnumerator cd() {
		yield return new WaitForSeconds (coolDown);
		enCoolDown = false;
	}

	IEnumerator vel() {
		yield return new WaitForSeconds(5);
        powerUpVelBar.transform.parent.gameObject.SetActive(false);
        powerUpVelBar.value = 0;
        velTimer = 0;
        maxSpeed = 3;
		estaDisparando = true;
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator dmg() {
		yield return new WaitForSeconds (5);
        powerUpDmgBar.transform.parent.gameObject.SetActive(false);
        powerUpDmgBar.value = 0;
        dmgTimer = 0;
        bulletPrefab.GetComponent<BulletMovement>().damageRef = 5;
        GetComponent<SpriteRenderer>().color = Color.white;
	}



		
		

}