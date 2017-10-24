using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour 
{

	float sawSpeed = 300;

	void Update ()
	{
		transform.Rotate (0, 0, sawSpeed * Time.deltaTime);
	}
}
