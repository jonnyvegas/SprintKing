using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] Scoreboard scoreboard;

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
    

    private void SpawnChunks()
    {
        chunkPosition = Vector3.zero;
        for (int i = 0; i < startingNumChunks; i++)
        {
            // or you can add 10 here, either way works.
            chunkPosition.z = CalculateSpawnPosition(i);
            currentChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent).GetComponent<Chunk>();
            chunks.Add(currentChunk.gameObject);
            currentChunk.gameObject.AddComponent<MoveObjectBackward>();
            currentChunk.InitSpawns(this, scoreboard);// fenceManagerRef, pickupManagerRef, chunkLaneManager);
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
            if (chunks[i].transform.position.z < Camera.main.transform.position.z - killOffset)
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
        Debug.Log("chunk z pos" + chunkPosition.z);
        currentChunk.transform.position = chunkPosition;
        if(currentChunk.TryGetComponent(out Chunk chunk))
        {
            chunk.InitSpawns(this, scoreboard);
        }
        chunks.Add(currentChunk);
    }

    public void SetSpeed(float speed)
    {
        previousSpeed = moveSpeed;
        this.moveSpeed = speed;
        float deltaMoveSpeed = moveSpeed - previousSpeed;
        deltaMoveSpeed = Mathf.Clamp(deltaMoveSpeed, minMoveSpeed, maxMoveSpeed);
        //Debug.Log("Previous speed " + previousSpeed + " new speed " + this.moveSpeed);
        foreach(GameObject chunk in chunks)
        {
            if(chunk.TryGetComponent(out MoveObjectBackward moveObjectBackward))
            {
                if (speed != previousSpeed)
                {
                    moveObjectBackward.SetSpeed(moveSpeed);
                }
            }
        }
        if (speed != previousSpeed)
        {
            float newGravityZ = Physics.gravity.z + deltaMoveSpeed;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(speed - previousSpeed);
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}

