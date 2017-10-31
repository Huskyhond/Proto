using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGiver : MonoBehaviour {

    [SerializeField] private GameObject seed;
    [SerializeField] private int amount = 30;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision) {
        var player = collision.gameObject.GetComponent<Player>();
        if (!player) return;
        if (player.HasPickable) return;
        if (amount > 0) {
            Instantiate(seed, transform.position, transform.rotation);
            amount--;
        }
    }
}
