using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

	public enum PickableType { SEED, WATER_CONTAINER }
	public PickableType Type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Drop(Player player = null) {
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().velocity = transform.forward + Vector3.up * 5f;
		transform.parent = null;
	}

	public void Teleport(Player player = null) {
		GetComponent<Rigidbody>().isKinematic = false;
		transform.parent = null;
		if(player.playerId == 1)
			GetComponent<Rigidbody>().velocity = new Vector3(8f, 8f, 0f);
		else
			GetComponent<Rigidbody>().velocity = new Vector3(-8f, 8f, 0f);
	}

	void OnCollisionEnter(Collision collision) {
		var player = collision.gameObject.GetComponent<Player>();
		if(player) {
			if(!player.HasPickable) {
				player.Pickup(this);
			}
		}
	}

}
