using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEat : MonoBehaviour {

    [SerializeField] private AudioSource yumyum;
    [SerializeField] private int winScore = 5;
    private int score;
    [SerializeField] private GameObject _uiVictory;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
        _uiVictory.SetActive(true);
        Time.timeScale = 0f;
    }
}
