using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public interface IChunk
{
    void InitSpawns();
}

public interface ILanes
{
    void ClearLaneRefs();
}

public abstract class LaneManager : ILanes
{
    public float[] lanes { get; set; }
    public List<bool> lanesOccupied { get; set; }
    public abstract void ClearLaneRefs();

}

public class ChunkLaneManager : LaneManager
{
   public override void ClearLaneRefs()
   {
       for(int i = 0; i < lanesOccupied.Count; i++)
        {
            lanesOccupied[i] = false;
        }
       lanesOccupied.Clear();
   }
}

public class Chunk : MonoBehaviour, IChunk
{
    FenceManager fenceManagerRef;
    PickupManager pickupManagerRef;
    LaneManager chunkLaneManager;
    //[SerializeField] float[] lanes = { -2.5f, 0, 2.5f };

    void Awake()
    {
        if(this.TryGetComponent(out FenceManager fenceManager))
        {
            fenceManagerRef = fenceManager;
        }
        if(this.TryGetComponent(out PickupManager pickupManager))
        {
            pickupManagerRef = pickupManager;
        }
        chunkLaneManager = new ChunkLaneManager();
        chunkLaneManager.lanes = new float[] { -2.5f, 0, 2.5f };
        chunkLaneManager.lanesOccupied = new List<bool>() { false, false, false };
        pickupManagerRef.SetChunkLaneManager(chunkLaneManager);
        fenceManager.SetLaneManager(chunkLaneManager);
    }
    private void Start()
    {
        chunkLaneManager.lanesOccupied = new List<bool> { false, false, false };

        InitSpawns();
        
    }

    public void InitSpawns()
    {
        pickupManagerRef.ClearPickups();
        ClearLaneRefs();
        fenceManagerRef.ClearFenceRefs();
        fenceManagerRef.SpawnFences();
        pickupManagerRef.SpawnCoins();
        pickupManagerRef.SpawnApples();
        
    }

    void ClearLaneRefs()
    {
        for (int j = 0; j < chunkLaneManager.lanesOccupied.Count; j++)
        {
            chunkLaneManager.lanesOccupied[j] = false;
        }
    }
}
