using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	[SerializeField] private GameObject[] _enemiesPositions;
	public GameObject[] Enemies;

	public static EnemyManager Instance;

	// Use this for initialization
	void Start () {
		if(Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void GameStart() {
		foreach(GameObject enemyPosition in _enemiesPositions)
			PhotonNetwork.Instantiate("Bird", enemyPosition.transform.position, Quaternion.identity, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
