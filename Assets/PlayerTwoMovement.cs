using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMovement : MonoBehaviour {
	public bool HasItem = false;
	public float speed = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			GetComponent<Rigidbody>().velocity = Vector3.left * speed;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			GetComponent<Rigidbody>().velocity = Vector3.right * speed;
		}	
		if (Input.GetKey(KeyCode.UpArrow))
		{
			GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			GetComponent<Rigidbody>().velocity = Vector3.back * speed;
		}	
	}

}
