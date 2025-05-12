using Food;
using UnityEngine;

public class InsectSpawner : MonoBehaviour
{
    public GameObject[] insectPrefabs;
    public float spawnInterval = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnInsect), 5f, spawnInterval);
    }

    [System.Obsolete]
    private void SpawnInsect()
    {
        FoodBase[] allPlants = FindObjectsOfType<FoodBase>();
        var ripePlants = System.Array.FindAll(allPlants, p => p.IsRipe && p.Health > 0);

        if(ripePlants.Length>0)
        {
            Vector3 spawnPos = ripePlants[Random.Range(0, ripePlants.Length)].transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            GameObject prefab = insectPrefabs[Random.Range(0, insectPrefabs.Length)];
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
