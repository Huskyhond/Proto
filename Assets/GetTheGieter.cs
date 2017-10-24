using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTheGieter : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var keycodeOne = KeyCode.I;
		var keycodeTwo = KeyCode.P;
		if(gameObject.transform.parent.name == "Player 1") {
			keycodeOne = KeyCode.E;
			keycodeTwo = KeyCode.T;
		}
		if(Input.GetKeyDown(keycodeOne)) {
			GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.parent = gameObject.transform.parent.parent;
			gameObject.transform.position = gameObject.transform.position - new Vector3(1f, 1f, 1f);
		}
		if(Input.GetKeyDown(keycodeTwo)) {
			GetComponent<Rigidbody>().isKinematic = false;
			if(gameObject.transform.parent.parent.name == "Island 2") {
				gameObject.transform.position = new Vector3(-16.41f, 3f, 0f);
			}
			else {
				gameObject.transform.position = new Vector3(0f, 3f, 0f);
			}
			gameObject.transform.parent = gameObject.transform.parent.parent;
			
		}
	}

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2" || collision.gameObject.name == "Zaad" || collision.gameObject.name == "Gieter") {
			if(collision.GetComponentInChildren<GetTheGieter>() != null || collision.GetComponentInChildren<GetTheZaad>() != null) return;
		}
		GetComponent<Rigidbody>().isKinematic = true;
		gameObject.transform.parent = collision.gameObject.transform;
		gameObject.transform.position = gameObject.transform.position + new Vector3(0f, 1f, 0f);
	}

}
