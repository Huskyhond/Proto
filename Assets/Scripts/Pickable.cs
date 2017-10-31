using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Photon.MonoBehaviour {

	public enum PickableType { SEED, WATER_CONTAINER, MUSHROOM, TUSHROOM }
	public PickableType Type;

	private Vector3 _startLocation;
	private Vector3 _wantedPosition;
	// Use this for initialization
	void Start () {
		_startLocation = _wantedPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, _wantedPosition) < 0.1f)
			transform.position = Vector3.MoveTowards(transform.position, _wantedPosition, 0.5f);
		if(transform.position.y < -3f) {
			Respawn();
		}
	}

	void OnPhotonSerializeView(PhotonStream	stream,PhotonMessageInfo info) {
		if(stream.isReading) {
			Debug.Log((int)stream.ReceiveNext());
			//_wantedPosition = position;
		}
		if(stream.isWriting) {
			stream.SendNext(transform.position.x);
		}
	}

	public void Respawn() {
		transform.position = _startLocation;	
	}

	public void Drop(Player player = null) {
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().velocity = transform.forward + Vector3.up * 5f;
		transform.parent = null;
	}

	public void Teleport(Player player = null) {
		GetComponent<Rigidbody>().isKinematic = false;
		transform.parent = null;
		if(Type == PickableType.MUSHROOM || Type == PickableType.TUSHROOM) {
			if(player.playerId == 1)
				GetComponent<Rigidbody>().velocity = new Vector3(4f, 4f, 0f);
			else
				GetComponent<Rigidbody>().velocity = new Vector3(-4f, 4f, 0f);
		}
		else {
			if(player.playerId == 1)
				GetComponent<Rigidbody>().velocity = new Vector3(8f, 8f, 0f);
			else
				GetComponent<Rigidbody>().velocity = new Vector3(-8f, 8f, 0f);
		}
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
