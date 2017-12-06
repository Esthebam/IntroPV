using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

	public float points;
	public Text pointsText;
	public Text inputText;

	void Update() {
		pointsText.text = ("Score: " + Mathf.FloorToInt(points));
	}
}
