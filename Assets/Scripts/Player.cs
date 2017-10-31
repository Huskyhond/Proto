using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerKeys {
	public KeyCode DropItemKey;
	public KeyCode TeleportItemKey;
	public KeyCode CraftMenuKey;
	public KeyCode MoveUpKey, MoveDownKey, MoveRightKey, MoveLeftKey;
}

[RequireComponent(typeof(Rigidbody))]
public class Player : Photon.MonoBehaviour {
	public int playerId = 1;
	public PlayerKeys Inputs { get { return _inputs; } }
	public bool HasPickable { get { return _pickable != null; } }
	public Pickable Pickable { get { return _pickable; } }
	[SerializeField] private PlayerKeys _inputs = new PlayerKeys();
	[Range(1, 100)] [SerializeField] private float _movementSpeed = 10f;
	private CraftMenu _craftMenu;
	private bool _onFloor = true;
	public bool IsLocal = false;
	private Pickable _pickable;
	private Rigidbody _rb;
	private Vector3 _startLocation;
	private Vector3 _wantedPosition;
	// Use this for initialization
	void Start () {
		_craftMenu = GetComponentInChildren<CraftMenu>();
		_startLocation = transform.position;
		_rb = GetComponent<Rigidbody>();
	}

	void OnPhotonSerializeView(PhotonStream	stream,PhotonMessageInfo info) {
		if(stream.isReading) {
			Vector3 position = (Vector3) stream.ReceiveNext();
			_wantedPosition = position;
		}
		if(stream.isWriting) {
			stream.SendNext(transform.position);
		}
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

	public void Respawn() {
		transform.position = _startLocation;	
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsLocal && _wantedPosition != null) {
			transform.position = Vector3.MoveTowards(transform.position, _wantedPosition, 0.5f);
		}
		if(!IsLocal) return;
		if(_onFloor) {
			_rb.velocity = Vector3.zero;
			if(Input.GetKey(_inputs.MoveUpKey) || Input.GetAxis("AxisY" + playerId) < -0.1f)
				Move(Vector3.forward);
			if(Input.GetKey(_inputs.MoveDownKey) || Input.GetAxis("AxisY" + playerId) > 0.1f)
				Move(Vector3.back);
			if(Input.GetKey(_inputs.MoveLeftKey) || Input.GetAxis("AxisX" + playerId) < -0.1f)
				Move(Vector3.left);
			if(Input.GetKey(_inputs.MoveRightKey) || Input.GetAxis("AxisX" + playerId) > 0.1f)
				Move(Vector3.right);
			if(Input.GetKeyDown(_inputs.DropItemKey) || Input.GetButtonDown("Release" + playerId)) {
				if(HasPickable) _pickable.Drop();
				_pickable = null;
			}
			if(Input.GetKeyDown(_inputs.CraftMenuKey) || Input.GetButtonDown("Fire" + playerId)) {
				_craftMenu.ToggleMenu();
			}
			if(Input.GetKeyDown(_inputs.TeleportItemKey) || Input.GetButtonDown("Throw" + playerId)) {
				if(HasPickable) _pickable.Teleport(this);
				_pickable = null;
			}
		}
		if(transform.position.y < -3f) {
			Respawn();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.layer == LayerMask.NameToLayer("Island")) {
			//_onFloor = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		if(collision.gameObject.layer == LayerMask.NameToLayer("Island")) {
			//_onFloor = false;
		}
	}

}
