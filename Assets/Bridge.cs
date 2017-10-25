using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

	[SerializeField] private GameObject _uiVictory;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.GetComponent<Player>() && transform.position.y < 0f) {
			collision.gameObject.GetComponent<Player>().Respawn();
		}
		else if(collision.gameObject.GetComponent<Player>() && transform.position.y >= 0f) {
			_uiVictory.SetActive(true);
			Time.timeScale = 0f;
		}
		else if(collision.gameObject.GetComponent<Pickable>()) {
			var pickable = collision.gameObject.GetComponent<Pickable>();
			if(pickable.Type == Pickable.PickableType.TUSHROOM || pickable.Type == Pickable.PickableType.MUSHROOM) {
				float y = transform.position.y < 0f ? 0.2f : 0f; 
				transform.position += new Vector3(0f, y, 0f);
				Destroy(collision.gameObject);
			}
			else {
				collision.gameObject.GetComponent<Pickable>().Respawn();
			}
		}
	}

}
