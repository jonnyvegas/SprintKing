using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnedObjects;
    bool spawn = true;
    GameObject spawnedObject;
    private Coroutine spawningCoroutine;
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
            randIdx = Random.Range(0, spawnedObjects.Length - 1);
            spawnedObject = Instantiate(spawnedObjects[randIdx], this.transform);
            //spawnedObject.AddComponent<MoveObjectBackward>();
            yield return new WaitForSeconds(2f);
        }
    }
}
