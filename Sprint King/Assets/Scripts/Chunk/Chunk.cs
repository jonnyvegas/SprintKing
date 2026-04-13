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
    //LevelGenerator levelGenerator;
    //Scoreboard scoreboard;
    void Awake()
    {
        InitManagers();
    }
    private void Start()
    {
        chunkLaneManager.lanesOccupied = new List<bool> { false, false, false };
    }

    public void InitSpawns()
    {
        //InitSpawns(levelGenerator, scoreboard);
    }
    public void InitSpawns(LevelGenerator levelGen, Scoreboard scoreboard)
    {
        //this.levelGenerator = levelGen;
        //this.scoreboard = scoreboard;
        InitManagers();
        ClearLaneRefs();
        fenceManagerRef.ClearFenceRefs();
        pickupManagerRef.ClearPickups();
        fenceManagerRef.SpawnFences();
        pickupManagerRef.SpawnCoins(scoreboard);
        pickupManagerRef.SpawnApples(levelGen);
    }

    public void InitManagers()
    {
        if (TryGetComponent(out FenceManager fenceManager))
        {
            this.fenceManagerRef = fenceManager;
        }
        if (TryGetComponent(out PickupManager pickupManager))
        {
            this.pickupManagerRef = pickupManager;
        }
        chunkLaneManager = new ChunkLaneManager();
        chunkLaneManager.lanes = new float[] { -2.5f, 0, 2.5f };
        chunkLaneManager.lanesOccupied = new List<bool>() { false, false, false };
        pickupManagerRef.SetChunkLaneManager(chunkLaneManager);
        fenceManagerRef.SetLaneManager(chunkLaneManager);
    }
    

    void ClearLaneRefs()
    {
        for (int j = 0; j < chunkLaneManager.lanesOccupied.Count; j++)
        {
            chunkLaneManager.lanesOccupied[j] = false;
        }
    }
}
