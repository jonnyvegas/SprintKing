using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] float[] lanes = { -2.5f, 0, 2.5f };
    List<bool> lanesOccupied;
    int fenceIdx = -1;
    int maxNumFences = 3;
    int numFencesToSpawn = 0;
    private void Start()
    {
        lanesOccupied = new List<bool> { false, false, false };

        SpawnFence();
        
    }

    void SpawnFence()
    {
        Vector3 SpawnPos = Vector3.zero;
        
        numFencesToSpawn = Random.Range(1, maxNumFences + 1);
        Debug.Log("fences to spawn " + numFencesToSpawn);
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
                GameObject fence = Instantiate(fencePrefab, SpawnPos, Quaternion.identity, this.transform);
            }
        }
        
        
        
    }
}
