using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : MonoBehaviour {

	private GameObject _p;
	private Vector3 _location;
	private Vector3 _startLocation;
    public static List<GameObject> mushrooms;
	// Use this for initialization
	void Start () {
		_startLocation = transform.position;
        mushrooms = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position == _startLocation && !mushrooms.Contains(_p)) {
			if(mushrooms.Count == 0) return;
			var mushroom = mushrooms[Random.Range(0, mushrooms.Count-1)];
			_location = mushroom.transform.position;
			_p = mushroom.gameObject;
		}
		else {
			transform.position = Vector3.MoveTowards(transform.position, _location, 0.1f);
			if(Vector3.Distance(transform.position, _location) < 1f && mushrooms.Contains(_p)) {
				StartCoroutine(StartFeeding());
			}
			if(transform.position == _startLocation) {
                mushrooms.Remove(_p);
                _location = Vector3.zero;
			}
		}
	}

	IEnumerator StartFeeding() {
		yield return new WaitForSeconds(2f);
        if (Vector3.Distance(transform.position, _location) < 1f && mushrooms.Contains(_p)) {
            _location = _startLocation;
            mushrooms.Remove(_p);
            Destroy(_p.gameObject);
        }
        else {
            _p = null;
            _location = _startLocation;
        }
	}
	
	void OnCollisionEnter(Collision collision) {
		var player = collision.gameObject.GetComponent<Player>();
		if(player) {
			_location = _startLocation;
            mushrooms.Remove(_p);
        }
	}

}
