using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenu : MonoBehaviour {
	private Player _player;
	private bool _active = false;
	// Use this for initialization
	void Start () {
		_player = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleMenu() {
		_active = !_active;
	}

}
