using System.Collections.Generic;
using UnityEngine;
public interface IFenceSpawner
{
    void SpawnFences();
    void InitSpawns();
}

public abstract class FenceSpawnerParent : IFenceSpawner
{
    GameObject fencePrefab;

    public void SetFencePrefabRef(GameObject fencePrefabRef)
    {
        this.fencePrefab = fencePrefabRef;
    }
    public abstract void InitSpawns();
    public void SpawnFences() { }
}

public class FenceManager : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    List<GameObject> spawnedFences = new List<GameObject>();
    int fenceIdx = -1;
    int maxNumFences = 3;
    int numFencesToSpawn = 0;
    GameObject currentFence;
    LaneManager chunkLaneManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnFences()
    {
        Vector3 SpawnPos = Vector3.zero;
        numFencesToSpawn = Random.Range(1, maxNumFences + 1);
        for (int i = 0; i < numFencesToSpawn; i++)
        {
            fenceIdx = Random.Range(0, chunkLaneManager.lanes.Length);
            //Debug.Log(fenceIdx);
            if (!chunkLaneManager.lanesOccupied[fenceIdx])
            {
                chunkLaneManager.lanesOccupied[fenceIdx] = true;
                SpawnPos.x = chunkLaneManager.lanes[fenceIdx];
                SpawnPos.y = transform.position.y;
                SpawnPos.z = transform.position.z;
                currentFence = Instantiate(fencePrefab, SpawnPos, Quaternion.identity, this.transform);
                AddFenceToList(currentFence);
            }
        }
    }

    public void ClearFenceRefs()
    {
        foreach (GameObject fence in spawnedFences)
        {
            Destroy(fence);
        }
        spawnedFences.Clear();
    }

    void AddFenceToList(GameObject fence)
    {
        spawnedFences.Add(fence);
    }

    public void SetLaneManager(LaneManager laneManager)
    {
        this.chunkLaneManager = laneManager;
    }
}
