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
            int i = PhotonNetwork.playerList.Length-1;
            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = this.SpawnPositions[i].position;
                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;
                
                GameObject player = PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
                player.GetComponentInChildren<Camera>().enabled = true;
                player.GetComponentInChildren<AudioListener>().enabled = true;
                player.GetComponentInChildren<Player>().IsLocal = true;
                i++;
            }
        }
    }
}
