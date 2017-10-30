using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {
	public Sprite[] corazones;

	public void cambioVida(int pos) {
		this.GetComponent<Image>().sprite = corazones [pos];
	}


	// Use this for initialization
	void Start () {
		cambioVida (2);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
