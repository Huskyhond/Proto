using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEat : MonoBehaviour {

    [SerializeField] private AudioSource yumyum;
    [SerializeField] private int winScore;
    private int score;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.rotation.eulerAngles, out hit)) {
            Debug.Log(hit);
            if (hit.collider.GetComponent<Player>()) {

            }
        }
	}

    void OnTriggerEnter(Collider collision) {
        eat(collision);
    }

    void eat(Collider collision) {
        var pickable = collision.GetComponent<Pickable>();
        if (!pickable) return;
        if (pickable.Type != Pickable.PickableType.PINK_MUSHROOM) return;

        yumyum.Play();
        Destroy(collision.gameObject);
        score++;
        if (score >= winScore) {
            win();
        }
    }

    void win() {

    }
}
