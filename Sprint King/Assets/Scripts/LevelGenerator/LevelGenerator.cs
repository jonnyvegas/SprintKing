using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingNumChunks;
    [SerializeField] Transform chunkParent;
    List<GameObject> chunks = new List<GameObject>();
    Vector3 chunkPosition;
    GameObject currentChunk;
    float chunkLength = 10;
    [SerializeField] float killOffset = 10f;
    float moveSpeed = 10f;
    float minMoveSpeed = 2f;
    float previousSpeed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnChunks();
        InitializeChunkList();
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
            currentChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
            chunks.Add(currentChunk);
            currentChunk.AddComponent<MoveObjectBackward>();
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
        currentChunk.transform.position = chunkPosition;
        if(currentChunk.TryGetComponent(out IChunk chunkInterface))
        {
            chunkInterface.InitSpawns();
        }
        chunks.Add(currentChunk);
    }

    public void SetSpeed(float speed)
    {
        previousSpeed = moveSpeed;
        this.moveSpeed = speed;
        Debug.Log("Previous speed " + previousSpeed + " new speed " + this.moveSpeed);
        foreach(GameObject chunk in chunks)
        {
            if(chunk.TryGetComponent(out MoveObjectBackward moveObjectBackward))
            {
                moveObjectBackward.SetSpeed(moveSpeed);
                if (speed < minMoveSpeed)
                {
                    moveObjectBackward.SetSpeed(minMoveSpeed);
                }
            }
        }
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Mathf.Clamp(Physics.gravity.z - (previousSpeed - speed), -18.8f, -9.8f));
        cameraController.ChangeCameraFOV(speed - previousSpeed);
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}

