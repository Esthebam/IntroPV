using System.Collections;
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

	public int powerUpActivos;

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
	public GameObject powerUpSaltoPrefab;
	public GameObject sierraPrefab;
	public GameObject powerUpVelPrefab;
	public Text text;

	public AudioSource[] shootingSounds;
	public AudioSource powerUpSaltoSound;
	public AudioSource powerUpDañoSound;
	public AudioSource powerUpVelocidadSound;
	public AudioSource powerUpVidaSound;
	public AudioClip grito;
	public AudioClip surprise;
	public AudioClip risa;

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
	private bool tieneInstanciadoSalto;
	private bool tieneInstanciadoVel;
	public gameMaster gm;
	private float startTime = 200;
	private float tiempoRestante;
	public Vector3 checkpoint;




	// Use this for initialization
	void Start()
	{
		//CombatTextManager.Instance;
		myRigidbody2D = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		estaDisparando = true;
		powerUpDmgBar.value = 0;
		powerUpSaltoBar.value = 0;
		powerUpVelBar.value = 0;
		powerUpVidaBar.value = 0;
		tieneInstanciadoSalto = false;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	
	
	}

	// Update is called once per frame
	void Update()
	{
		tiempoRestante = startTime - Time.time;

		if (Input.GetKeyDown (KeyCode.Space)) {
			CombatTextManager.Instance.CreateText(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), "Hola", Color.red, false);
		}

		if (Input.GetKeyDown (KeyCode.V)) {
			CombatTextManager.Instance.CreateText(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), "Hola", Color.red, true);
		}

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
			powerUpActivos++;
			powerUpSaltoSound.Play();
			powerUpSaltoBar.transform.parent.gameObject.SetActive(true);
			saltoTimer = 5;
			fuerzaSalto = 15f;
			GetComponent<SpriteRenderer> ().color = Color.yellow;
			Destroy (col.gameObject);
			if (coroutineSalto != null)
			{
				StopCoroutine(coroutineSalto);
				powerUpActivos--;
			}
			tieneInstanciadoSalto = false;
			coroutineSalto = StartCoroutine ("salto");

		}

		if (col.gameObject.tag == "PowerUpVida") {
			powerUpActivos++;
			powerUpVidaSound.Play();
			powerUpVidaBar.transform.parent.gameObject.SetActive(true);
			vidaTimer = 5;
			HealthManager.healthManager.invincible = true;
			GetComponent<SpriteRenderer> ().color = Color.green;
			Destroy (col.gameObject);
			if (coroutineInvencible != null)
			{
				StopCoroutine(coroutineInvencible);
				powerUpActivos--;
			}		
			coroutineInvencible = StartCoroutine ("vida"); 
		}

		if (col.gameObject.tag == "PowerUpVel") {
			powerUpActivos++;
			powerUpVelocidadSound.Play();
			powerUpVelBar.transform.parent.gameObject.SetActive(true);
			velTimer = 5;
			maxSpeed = 6;
			GetComponent<SpriteRenderer> ().color = Color.blue;
			estaDisparando = false;
			Destroy (col.gameObject);
			if (coroutineVel != null)
			{
				StopCoroutine(coroutineVel);
				powerUpActivos--;
			}		
			coroutineVel = StartCoroutine ("vel");
		}

		if (col.gameObject.tag == "PowerUpDmg") {
			powerUpActivos++;
			powerUpDañoSound.Play();
			powerUpDmgBar.transform.parent.gameObject.SetActive(true);
			dmgTimer = 5;
			bulletPrefab.GetComponent<BulletMovement>().damageRef = 20;
			GetComponent<SpriteRenderer> ().color = Color.gray;
			Destroy (col.gameObject);
			if (coroutineDanio != null)
			{
				StopCoroutine(coroutineDanio);
				powerUpActivos--;
			}
			coroutineDanio = StartCoroutine ("dmg");
		}

	}

	private void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "InstanciadorPowerUp") {
			if (!tieneInstanciadoSalto) {
				tieneInstanciadoSalto = true;
				col.enabled = false;
				Instantiate (powerUpSaltoPrefab, new Vector3 (col.transform.position.x + 0.5f, col.transform.position.y + 8f, col.transform.position.z), Quaternion.identity);
				text.enabled = true;
				text.text = ("Acercate al borde para agarrar la habilidad de salto doble y poder llegar a la plataforma movil");
				StartCoroutine ("timer");
			}
		}

		if (col.tag == "InstanciadorSierra") {
			col.enabled = false;
			AudioSource.PlayClipAtPoint (surprise, transform.position);
			Instantiate (sierraPrefab, new Vector3 (col.transform.position.x - 3f, col.transform.position.y, col.transform.position.z), Quaternion.identity);
		}

		if (col.tag == "InstanciadorPowerUpVel") {
			if (!tieneInstanciadoVel) {
				tieneInstanciadoVel = true;
				col.enabled = false;
				Instantiate (powerUpVelPrefab, new Vector3 (col.transform.position.x + 1.5f, col.transform.position.y + 4f, col.transform.position.z), Quaternion.identity);
			}
		}

		if (col.tag == "InstanciadorPowerUpSalto") {
			if (!tieneInstanciadoSalto && maxSpeed > 3) {
				tieneInstanciadoSalto = true;
				col.enabled = false;
				Instantiate (powerUpSaltoPrefab, new Vector3 (col.transform.position.x + 3f, col.transform.position.y + 5f, col.transform.position.z), Quaternion.identity);
			}
		}

		if (col.tag == "Grito") {
			if (maxSpeed > 3) {
				AudioSource.PlayClipAtPoint (grito, transform.position);
			}
		}

		if (col.tag == "Limitador") {
			AudioSource.PlayClipAtPoint (risa, transform.position);
			transform.position = new Vector3(138.4f, -0.2940886f, 0);
			text.enabled = true;
			text.text = ("Por favor quedate en la plataforma");
			StartCoroutine ("timer");
		}

		if (col.tag == "Limitador2") {
			transform.position = new Vector3 (138.4f, -0.2940886f, 0);
		}
			
		if (col.tag == "ColliderPlataforma") {
			text.enabled = true;
			text.text = ("Al momento de caer no pares de correr para poder lograr alcanzar la siguiente plataforma");
			StartCoroutine ("timer");
		}

		if (col.tag == "PlataformaNivel") {
			text.text = ("Bajando al siguiente nivel...");
			StartCoroutine ("timer2");
		}

		if (col.tag == "ColliderEspera") {
			text.text = ("Espera la plataforma que te llevara al siguiente nivel");
		}

		if (col.tag == "ColliderNivel1") {
			gm.points += Mathf.FloorToInt (tiempoRestante) /2 ;
			Destroy (GameObject.FindGameObjectWithTag ("ColliderNivel1"));

		}

		if (col.tag == "Checkpoint") {
			checkpoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		}
	}


	IEnumerator salto() {
		yield return new WaitForSeconds (5);
		powerUpSaltoBar.transform.parent.gameObject.SetActive(false);
		powerUpSaltoBar.value = 0;
		saltoTimer = 0;
		fuerzaSalto = 9.25f;
		powerUpActivos--;
		GameObject.FindWithTag ("InstanciadorPowerUp").GetComponent<Collider2D> ().enabled = true;
		ChequearSalida();
	}

	IEnumerator vida() {
		yield return new WaitForSeconds (5);
		powerUpVidaBar.transform.parent.gameObject.SetActive(false);
		powerUpVidaBar.value = 0;
		vidaTimer = 0;
		HealthManager.healthManager.invincible = false;
		powerUpActivos--;
		ChequearSalida();
	}

	IEnumerator vel()
	{
		yield return new WaitForSeconds(5);
		powerUpVelBar.transform.parent.gameObject.SetActive(false);
		powerUpVelBar.value = 0;
		velTimer = 0;
		maxSpeed = 3;
		estaDisparando = true;
		powerUpActivos--;
		tieneInstanciadoVel = false;
		GameObject.FindWithTag ("InstanciadorPowerUpVel").GetComponent<Collider2D> ().enabled = true;
		ChequearSalida();
	}

	IEnumerator dmg()
	{
		yield return new WaitForSeconds(5);
		powerUpDmgBar.transform.parent.gameObject.SetActive(false);
		powerUpDmgBar.value = 0;
		dmgTimer = 0;
		bulletPrefab.GetComponent<BulletMovement>().damageRef = 5;

		ChequearSalida();
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

	IEnumerator timer() {
		yield return new WaitForSeconds (5);
		text.enabled = false;
	}

	IEnumerator timer2() {
		yield return new WaitForSeconds (3);
		text.enabled = false;
	}



	void ChequearSalida()
	{
		if (powerUpActivos == 0)
		{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	void restart(){
		estaDisparando = true;
		powerUpDmgBar.transform.parent.gameObject.SetActive(false);
		powerUpSaltoBar.transform.parent.gameObject.SetActive(false);
		powerUpVelBar.transform.parent.gameObject.SetActive(false);
		powerUpVidaBar.transform.parent.gameObject.SetActive(false);
		fuerzaSalto = 9.25f;
		dmgTimer=0;
		saltoTimer=0;
		velTimer=0;
		vidaTimer=0;


	}


}