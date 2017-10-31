using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlow : MonoBehaviour {

    [SerializeField] private AudioSource blow;
    [SerializeField] private float power = 100f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision) {
        var player = collision.GetComponent<Player>();
        if (player) {
            blow.Play();
        }
    }

    void OnTriggerStay(Collider collision) {
        var player = collision.GetComponent<Player>();
        if (!player) return;
        if (player.playerId == 1) {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(-power, 0, 0), ForceMode.Force);
        }
        if (player.playerId == 2) {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(power, 0, 0), ForceMode.Force);
        }
    }

    void OnTriggerExit() {
        blow.Stop();
    }
}
