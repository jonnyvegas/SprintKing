using UnityEngine;
using System.Collections.Generic;

public interface ILevelGenerator
{
    public abstract void SetSpeed(float speed);
    public abstract float GetSpeed();
}

public class LevelGenerator : MonoBehaviour, ILevelGenerator
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] Scoreboard scoreboard;
    [SerializeField] TimeManager timeManager;
    public TimeManager TimeManager => timeManager;

    [SerializeField] ItemSpawner spawner;

    [Header("Level Settings")]
    [Tooltip("Starting number of chunks.")]
    [SerializeField] int startingNumChunks;
    [SerializeField] float killOffset = 10f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22.81f;
    [SerializeField] float maxGravityZ = -2.81f;
    List<GameObject> chunks = new List<GameObject>();
    Vector3 chunkPosition;
    Chunk currentChunk;

    float chunkLength = 10;
    float previousSpeed = 0f;
    int chunksSpawned = 0;
    
    // How often should we spawn a checkpoint?
    [SerializeField] int checkpointInterval = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitManagers();
        SpawnChunks();
        InitializeChunkList();
    }

    void InitManagers()
    {

    }

    private void InitializeChunkList()
    {
        //chunks = new List<GameObject>();
    }


    private GameObject SpawnChunk()
    {
        GameObject chunkToSpawn;
        chunksSpawned++;
        if (chunksSpawned % checkpointInterval != 0)
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }
        else
        {
            chunkToSpawn = checkpointPrefab;
        }    
        currentChunk = Instantiate(chunkToSpawn, chunkPosition, Quaternion.identity, chunkParent).GetComponent<Chunk>();
        chunks.Add(currentChunk.gameObject);
        MoveObjectBackward mob = currentChunk.gameObject.AddComponent<MoveObjectBackward>();
        mob.SetSpeed(moveSpeed);
        currentChunk.InitSpawns(this, scoreboard);
        return currentChunk.gameObject;
    }

    private void SpawnChunks()
    {
        chunkPosition = Vector3.zero;
        for (int i = 0; i < startingNumChunks; i++)
        {
            // or you can add 10 here, either way works.
            chunkPosition.z = CalculateSpawnPosition(i);
            SpawnChunk();
            // fenceManagerRef, pickupManagerRef, chunkLaneManager);
        }
    }

    private float CalculateSpawnPosition(int i)
    {
        return i * chunkLength;
    }


    // Update is called once per frame
    void Update()
    {
        CheckChunksPos();
    }

    void CheckChunksPos()
    {
        for(int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i] && chunks[i].transform.position.z < Camera.main.transform.position.z - killOffset)
            {
                RemoveChunkAndReAdd(i);
            }
        }
    }

    void RemoveChunkAndReAdd(int chunkIdx)
    {
        GameObject currentChunk = chunks[chunkIdx];
        // Remove chunk and place at front.
        chunks.Remove(chunks[chunkIdx]);

        
        // Move it to the front by checking how many chunks we have and the starting number
        // since we will be removing one, we can add it back to the end.
        chunkPosition = currentChunk.transform.position;
        chunkPosition.z = chunkLength * (startingNumChunks - 1);
        //Debug.Log("chunk z pos" + chunkPosition.z);
        Destroy(currentChunk.gameObject);
        GameObject newChunk = SpawnChunk();
        newChunk.transform.position = chunkPosition;
        if(newChunk.TryGetComponent(out Chunk chunk))
        {
            chunk.InitSpawns(this, scoreboard);
        }
        chunks.Add(newChunk);
    }

    private void ChangeGravity(float deltaMoveSpeed, float speed)
    {
        float newGravityZ = Physics.gravity.z + deltaMoveSpeed;
        newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
        cameraController.ChangeCameraFOV(speed - previousSpeed);
    }

    public void SetSpeed(float speed)
    {
        previousSpeed = moveSpeed;
        this.moveSpeed = speed;
        float deltaMoveSpeed = moveSpeed - previousSpeed;
        //deltaMoveSpeed = Mathf.Clamp(deltaMoveSpeed, minMoveSpeed, maxMoveSpeed);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
        //Debug.Log("Previous speed " + previousSpeed + " new speed " + this.moveSpeed);
        foreach(GameObject chunk in chunks)
        {
            if(chunk && chunk.TryGetComponent(out MoveObjectBackward moveObjectBackward))
            {
                if (speed != previousSpeed)
                {
                    moveObjectBackward.SetSpeed(moveSpeed);
                }
            }
        }
        if (speed != previousSpeed)
        {
            ChangeGravity(deltaMoveSpeed, speed);

        }
        if(speed > 8)
        {
            spawner.ShortenSpawnTimes();
        }
        else
        {
            spawner.ResetSpawnTimes();
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    private void SetCheckpointInterval(int newInterval)
    {
        this.checkpointInterval = newInterval;
    }


}

