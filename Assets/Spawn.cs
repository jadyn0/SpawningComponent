using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
public class Spawn : MonoBehaviour
{
    public float PosY = 10f;
    public Object Prefab;                

    public int minPerBatch = 3;            
    public int maxPerBatch = 6;            


    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;

    public float spawnBatchRadius = 4f;

    public void Start()
    {
        StartCoroutine(SpawnFish());
    }
    private IEnumerator SpawnFish()
    {
        while (true)
        {

            int numToSpawn = Random.Range(minPerBatch, maxPerBatch);
            for (int i = 0; i < numToSpawn; i++)
            {
                Vector3 spawnOffset = Random.insideUnitSphere * spawnBatchRadius;
                Vector3 spawnPosition = (Vector3)gameObject.transform.position + spawnOffset;
                spawnPosition.y = PosY;

                Object newBox = Instantiate(Prefab, spawnPosition, Quaternion.identity);
            }
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
        }

    }
}
