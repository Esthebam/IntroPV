using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifes : MonoBehaviour {

	public Sprite[] HeartSprites;
	public Image HeartUI;
	public HealthManager player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		HeartUI.sprite = HeartSprites[player.currentLifes];
	}
}
