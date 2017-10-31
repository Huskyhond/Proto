using UnityEngine;
using System.Collections;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform[] SpawnPositions;
    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate;   // set in inspector

    public void OnJoinedRoom()
    {
        Debug.Log("I joined room");
        if (this.PrefabsToInstantiate != null)
        {
            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = this.SpawnPositions[PhotonNetwork.playerList.Length-1].position;
                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;
                
                GameObject player = PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
                player.GetComponentInChildren<Camera>().enabled = true;
                player.GetComponentInChildren<AudioListener>().enabled = true;
                player.GetComponentInChildren<Player>().IsLocal = true;
            }
        }
        StartCoroutine(TagPlayers());
    }

    IEnumerator TagPlayers(bool inversed = false) {
        while(GameObject.FindGameObjectsWithTag("Player").Length < 2)
            yield return new WaitForEndOfFrame();
            int i = 0;
        var list = GameObject.FindGameObjectsWithTag("Player");
        if(inversed) System.Array.Reverse(list);
        foreach(var player in list) {
            player.GetComponent<Player>().playerId = i+1;
            i++;
        }
    }

    public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer) {
        Debug.Log("Player connected");
        StartCoroutine(TagPlayers(true));
    }

}
