using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingNumChunks;
    [SerializeField] Transform chunkParent;
    List<GameObject> chunks;
    Vector3 chunkPosition;
    GameObject currentChunk;
    float chunkLength = 10;

    GameObject[] gameObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnChunks();
    }


    

    private void SpawnChunks()
    {
        chunkPosition = transform.position;
        //chunkPosition.z = 0;
        //chunkLength = chunkPrefab.GetComponent<Collider>().bounds.size.z;
        //Debug.Log(chunkPrefab.GetComponent<Collider>().bounds.size);
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
        
    }
}
