using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour {
	public GameObject[] Plants { get { return _plants; } }
	// Use this for initialization
	[SerializeField] private GameObject[] _plants;
	public static PlantManager Instance;
	void Start () {
		if(Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
