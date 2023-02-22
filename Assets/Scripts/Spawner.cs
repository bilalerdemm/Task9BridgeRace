using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cube
{
    public string name;

    public GameObject Prefab;
    [Range(0f, 100f)] public float Chance = 100f;
    [HideInInspector] public double weight;

    public static implicit operator GameObject(Cube v)
    {
        throw new System.NotImplementedException();
    }
}
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube[] cubes;

    private void Awake()
    {
        CalculateWights();
    }

    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    private void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            SpawnRandomCubes(new Vector3(Random.Range(-10, 10), 0.1f, Random.Range(-5   , 8)));
        }
    }
    private void SpawnRandomCubes(Vector3 position)
    {
        Cube randomCube = cubes[GetRandomCubeIndex()];
        Instantiate(randomCube.Prefab, position, Quaternion.identity); 
    }

    private int GetRandomCubeIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;
        for (int i = 0; i < cubes.Length; i++)
            if (cubes[i].weight >= r)
                return i;
        return 0;          
    }

    private void CalculateWights()
    {
        accumulatedWeights = 0f;
        foreach (Cube cube in cubes)
        {
            accumulatedWeights += cube.Chance;
            cube.weight = accumulatedWeights;
        }
    }



















    //public GameObject cubePrefab;

    //public float timer = 0f;
    //void Update()
    //{
    //    if (timer <= 3)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //    if (timer >= 3)
    //    {
    //        Vector3 randomSpawnPos = new Vector3(Random.Range(-10, 11), 0.1f, Random.Range(-10, 10));
    //        Instantiate(cubePrefab, randomSpawnPos, Quaternion.identity);
    //    }
    //}

}
