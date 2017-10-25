using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public bool IsSeeded { get { return _seeded; } }
	public bool _seeded = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Seed() {
		_seeded = true;
		transform.localScale = transform.localScale + new Vector3(0f, 0.1f, 0f);
		GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
	}

	public virtual void Grow() {
			transform.localScale += new Vector3(0f, 0.1f, 0f);
			transform.localScale = new Vector3(1f, Mathf.Min(transform.localScale.y, 5f), 1f);
	}

	void OnTriggerEnter(Collider collision) {
		var player = collision.GetComponent<Player>();
		if(!player) return;
		if(
			player.HasPickable == true && 
			player.Pickable.Type == Pickable.PickableType.SEED &&
			!IsSeeded) {
			Seed();
		}
	}

	void OnTriggerStay(Collider collision) {
		if(!IsSeeded) return;
		var player = collision.GetComponent<Player>();
		if(player &&
		   player.HasPickable &&
		   player.Pickable.Type == Pickable.PickableType.WATER_CONTAINER) {
		   Grow();
		}
	}
}
