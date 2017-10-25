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
			foreach(GameObject p in GameObject.FindGameObjectsWithTag("Plant")) {
				if(p.transform.localScale.y >= 0.1f) {
					plants.Add(p);
				}
			}
			var plant = plants[Random.Range(0, plants.Count-1)];
			_location = plant.transform.position;
			_p = plant.transform;
		}
		else {
			transform.position = Vector3.MoveTowards(transform.position, _location, 0.1f);
			if(Vector3.Distance(transform.position, _location) < 1f) {
				_p.localScale = new Vector3(1f, 0.01f, 1f);
				_p.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
				_location = _startLocation;
				_p = null;
			}
			if(transform.position == _startLocation) {
				_p = null;
				_location = Vector3.zero;
			}
		}
	}
}
