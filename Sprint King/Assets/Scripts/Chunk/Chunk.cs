using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public interface IFenceSpawner
{
    void SpawnFences();
    void InitSpawns();
}

public abstract class FenceSpawnerChild : IFenceSpawner
{
    GameObject fencePrefab;

    public void SetFencePrefabRef(GameObject fencePrefabRef)
    {
        this.fencePrefab = fencePrefabRef;
    }
    public abstract void InitSpawns();
    public void SpawnFences() { }
}

public class Chunk : MonoBehaviour, IFenceSpawner
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] float[] lanes = { -2.5f, 0, 2.5f };
    float pickupSpawnRate = 0.5f;
    float pickupOffset = 0.1f;
    List<bool> lanesOccupied;
    List<GameObject> spawnedFences = new List<GameObject>();
    List<GameObject> pickups = new List<GameObject>();
    int fenceIdx = -1;
    int maxNumFences = 3;
    int numFencesToSpawn = 0;
    GameObject currentFence;
    private void Start()
    {
        lanesOccupied = new List<bool> { false, false, false };

        InitSpawns();
        
    }

    public void InitSpawns()
    {
        ClearLaneRefs();
        ClearFenceRefs();
        ClearPickups();
        SpawnFences();
        SpawnPickups();
    }

    public void SpawnFences()
    {
        Vector3 SpawnPos = Vector3.zero;
        numFencesToSpawn = Random.Range(1, maxNumFences + 1);
        for(int i = 0; i < numFencesToSpawn; i++)
        {
            fenceIdx = Random.Range(0, lanes.Length);
            Debug.Log(fenceIdx);
            if (!lanesOccupied[fenceIdx])
            {
                lanesOccupied[fenceIdx] = true;
                SpawnPos.x = lanes[fenceIdx];
                SpawnPos.y = transform.position.y;
                SpawnPos.z = transform.position.z;
                currentFence = Instantiate(fencePrefab, SpawnPos, Quaternion.identity, this.transform);
                AddFenceToList(currentFence);
            }
        }
    }

    public void SpawnPickups()
    {
        float randVal = -1f;
        int numSpawns = -1;
        Vector3 spawnPos = Vector3.zero;
        for (int i = 0; i < lanesOccupied.Count; i++)
        {
            if (!lanesOccupied[i])
            {
                randVal = Random.value;
                // Spawn a pickup.
                if(randVal < pickupSpawnRate)
                {
                    spawnPos.x = lanes[i];
                    spawnPos.y = transform.position.y + pickupOffset;
                    spawnPos.z = transform.position.z;
                    lanesOccupied[i] = true;
                    // actually, spawn 5!
                    // exclusive when ints, inclusive when float.
                    numSpawns = Random.Range(1, 6);
                    float amtToAdd = 2f;
                    for (int j = 0; j < numSpawns; j++)
                    { 
                        // Spawn one in the middle....
                        pickups.Add(Instantiate(pickupPrefab, spawnPos, Quaternion.identity, this.transform));
                        
                        spawnPos.z += amtToAdd;
                        //// even number, add to the current lane position.
                        //if(j % 2 == 0)
                        //{
                        //    spawnPos.x = lanes[i] + amtToAdd * ((j / 2) + 1);
                        //}
                        //// odd, subtract from current lane position.
                        //else
                        //{
                        //    spawnPos.x = lanes[i] - amtToAdd * ((j / 2) + 1);
                        //}
                    }
                }
            }
        }
    }

    void ClearFenceRefs()
    {
        foreach (GameObject fence in spawnedFences)
        {
            Destroy(fence);
        }
        spawnedFences.Clear();
    }

    void ClearLaneRefs()
    {
        for (int j = 0; j < lanesOccupied.Count; j++)
        {
            lanesOccupied[j] = false;
        }
    }

    void ClearPickups()
    {
        foreach(GameObject pickup in pickups)
        {
            Destroy(pickup);
        }
        pickups.Clear();
    }

    void AddFenceToList(GameObject fence)
    {
        spawnedFences.Add(fence);
    }
}
