using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	private Transform _p;
	private Vector3 _location;
	private Vector3 _startLocation;
	// Use this for initialization
	void Start () {
		_startLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position == _startLocation && _p == null) {
			var plants = new List<GameObject>();
			foreach(GameObject p in PlantManager.Instance.Plants) {
				if(p.GetComponent<Plant>().IsSeeded) {
					plants.Add(p);
				}
			}
			if(plants.Count == 0) return;
			var plant = plants[Random.Range(0, plants.Count-1)];
			_location = plant.transform.position;
			_p = plant.transform;
		}
		else {
			transform.position = Vector3.MoveTowards(transform.position, _location, 0.1f);
			if(Vector3.Distance(transform.position, _location) < 1f && _p != null) {
				StartCoroutine(StartFeeding());
			}
			if(transform.position == _startLocation) {
				_p = null;
				_location = Vector3.zero;
			}
		}
	}

	IEnumerator StartFeeding() {
		yield return new WaitForSeconds(2f);
		if(Vector3.Distance(transform.position, _location) < 1f && _p != null) {
			_location = _startLocation;
			_p.GetComponent<Plant>().UnSeed();
			_p = null;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		var player = collision.gameObject.GetComponent<Player>();
		if(player) {
			_location = _startLocation;
			_p = null;
		}
	}

}
