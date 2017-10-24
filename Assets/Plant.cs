using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision) {
		if(collision.GetComponentInChildren<GetTheZaad>() != null && transform.localScale.y < 0.1f) {
			transform.localScale = new Vector3(1f, 0.1f, 1f);
			GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
		}
	}

	void OnTriggerStay(Collider collision) {
		if(transform.localScale.y < 0.1f) return;
		if(collision.GetComponentInChildren<GetTheGieter>() != null) {
			transform.localScale += new Vector3(0f, 0.1f, 0f);
			transform.localScale = new Vector3(1f, Mathf.Min(transform.localScale.y, 5f), 1f);
		}
	}
}
