using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public bool IsSeeded { get { return _seeded; } }
	private bool _seeded = false;
	[SerializeField] private GameObject _seedlingPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void UnSeed() {
		_seeded = false;
		transform.localScale = new Vector3(1f, 0.01f, 1f);
		GetComponent<Renderer>().material.SetColor("_Color", Color.red);
	}

	public virtual void Seed() {
		_seeded = true;
		transform.localScale = transform.localScale + new Vector3(0f, 0.1f, 0f);
		GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
	}

	public virtual void Grow() {
			transform.localScale += new Vector3(0f, 0.1f, 0f);
			transform.localScale = new Vector3(1f, Mathf.Min(transform.localScale.y, 5f), 1f);
			if(transform.localScale.y >= 5f) {
				StartCoroutine(StartSeedling());
			}
	}

	IEnumerator StartSeedling() {
		yield return new WaitForSeconds(10f);
		if(transform.localScale.y >= 5f) {
			UnSeed();
			Instantiate(_seedlingPrefab, transform.position, Quaternion.identity);
		}
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
