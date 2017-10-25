﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerKeys {
	public KeyCode DropItemKey;
	public KeyCode TeleportItemKey;
	public KeyCode MoveUpKey, MoveDownKey, MoveRightKey, MoveLeftKey;
}

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
	public int playerId = 1;
	public PlayerKeys Inputs { get { return _inputs; } }
	public bool HasPickable { get { return _pickable != null; } }
	public Pickable Pickable { get { return _pickable; } }
	[SerializeField] private PlayerKeys _inputs = new PlayerKeys();
	[Range(1, 100)] [SerializeField] private float _movementSpeed = 10f;
	private Pickable _pickable;
	private Rigidbody _rb;
	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 direction) {
		_rb.velocity += direction * _movementSpeed;
	}
	
	public void Pickup(Pickable pickable) {
		_pickable = pickable;
		_pickable.GetComponent<Rigidbody>().isKinematic = true;
		_pickable.gameObject.transform.parent = transform;
		_pickable.transform.position = transform.position + (Vector3.up * 2f);
	}
	
	// Update is called once per frame
	void Update () {
		_rb.velocity = Vector3.zero;
		if(Input.GetKey(_inputs.MoveUpKey))
			Move(Vector3.forward);
		if(Input.GetKey(_inputs.MoveDownKey))
			Move(Vector3.back);
		if(Input.GetKey(_inputs.MoveLeftKey))
			Move(Vector3.left);
		if(Input.GetKey(_inputs.MoveRightKey))
			Move(Vector3.right);
		if(Input.GetKeyDown(_inputs.DropItemKey)) {
			if(HasPickable) _pickable.Drop();
			_pickable = null;
		}
		if(Input.GetKeyDown(_inputs.TeleportItemKey)) {
			if(HasPickable) _pickable.Teleport(this);
			_pickable = null;
		}
	}
}