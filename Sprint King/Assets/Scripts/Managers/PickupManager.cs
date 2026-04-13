using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    float pickupSpawnRate = 0.5f;
    float pickupOffset = 0.1f;
    List<GameObject> pickups = new List<GameObject>();
    LaneManager chunkLaneManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCoins(Scoreboard scoreboard)
    {
        float randVal = -1f;
        int numSpawns = -1;
        Vector3 spawnPos = Vector3.zero;
        for (int i = 0; i < chunkLaneManager.lanesOccupied.Count; i++)
        {
            if (!chunkLaneManager.lanesOccupied[i])
            {
                randVal = Random.value;
                // Spawn a pickup.
                if (randVal < pickupSpawnRate)
                {
                    spawnPos.x = chunkLaneManager.lanes[i];
                    spawnPos.y = transform.position.y + pickupOffset;
                    spawnPos.z = transform.position.z;
                    chunkLaneManager.lanesOccupied[i] = true;
                    // actually, spawn 5!
                    // exclusive when ints, inclusive when float.
                    numSpawns = Random.Range(1, 6);
                    float amtToAdd = 2f;
                    for (int j = 0; j < numSpawns; j++)
                    {
                        // Spawn one in the middle....
                        CoinPickup currentPickup = SpawnPickup(spawnPos, coinPrefab).GetComponent<CoinPickup>();
                        currentPickup.Init(scoreboard);
                        spawnPos.z += amtToAdd;
                    }
                }
            }
        }
    }

    private GameObject SpawnPickup(Vector3 pos, GameObject prefab)
    {
        GameObject currentPickup = Instantiate(prefab, pos, Quaternion.identity, this.transform);
        pickups.Add(currentPickup);
        return currentPickup;
    }

    public void SpawnApples(LevelGenerator levelGen)
    {
        float randVal = -1f;
        Vector3 spawnPos = Vector3.zero;
        for (int i = 0; i < chunkLaneManager.lanesOccupied.Count; i++)
        {
            if (!chunkLaneManager.lanesOccupied[i])
            {

                randVal = Random.value;
                // Spawn a pickup.
                if (randVal < pickupSpawnRate)
                {
                    spawnPos.x = chunkLaneManager.lanes[i];
                    spawnPos.y = transform.position.y + pickupOffset;
                    spawnPos.z = transform.position.z;
                    ApplePickup apple = SpawnPickup(spawnPos, applePrefab).GetComponent<ApplePickup>();
                    apple.Init(levelGen);
                }
            }
        }
    }

    public void ClearPickups()
    {
        foreach (GameObject pickup in pickups)
        {
            Destroy(pickup);
        }
        pickups.Clear();
    }

    public void SetChunkLaneManager(LaneManager chunkLaneManager)
    {
        this.chunkLaneManager = chunkLaneManager;
    }    
}
