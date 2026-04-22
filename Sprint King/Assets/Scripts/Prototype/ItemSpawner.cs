using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnedObjects;
    bool spawn = true;
    GameObject spawnedObject;
    GameObject currentObject;
    private Coroutine spawningCoroutine;
    [SerializeField] float xRange = 5f;
    [SerializeField] float minTime = 2f;
    [SerializeField] float maxTime = 5f;
    Vector3 spawnPos = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawningCoroutine = StartCoroutine(SpawnItems());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnItems()
    {
        int randIdx = -1;
        while (spawn)
        {
            randIdx = Random.Range(0, spawnedObjects.Length);
            spawnedObject = spawnedObjects[randIdx];
            spawnPos = this.transform.position;
            spawnPos.x = Random.Range(-xRange, xRange);
            //Debug.Log(spawnPos.x);
            spawnedObject = Instantiate(spawnedObject, spawnPos, Random.rotation, this.transform);
            //spawnedObject.AddComponent<MoveObjectBackward>();
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

    public void ShortenSpawnTimes()
    {
        StopCoroutine(spawningCoroutine);
        minTime = 0.5f;
        maxTime = 2f;
        spawningCoroutine = StartCoroutine(SpawnItems());
    }

    public void ResetSpawnTimes()
    {
        StopCoroutine(spawningCoroutine);
        minTime = 2f;
        maxTime = 5f;
        spawningCoroutine = StartCoroutine(SpawnItems());
    }
}
