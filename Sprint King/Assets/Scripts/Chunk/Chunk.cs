using UnityEngine;
using System.Collections.Generic;

public interface IFenceSpawner
{
    void SpawnFence();
}

public class Chunk : MonoBehaviour, IFenceSpawner
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] float[] lanes = { -2.5f, 0, 2.5f };
    List<bool> lanesOccupied;
    List<GameObject> spawnedFences = new List<GameObject>();
    int fenceIdx = -1;
    int maxNumFences = 3;
    int numFencesToSpawn = 0;
    GameObject currentFence;
    private void Start()
    {
        lanesOccupied = new List<bool> { false, false, false };

        SpawnFence();
        
    }

    public void SpawnFence()
    {
        ClearLaneRefs();
        ClearFenceRefs();

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

    void AddFenceToList(GameObject fence)
    {
        spawnedFences.Add(fence);
    }
}
