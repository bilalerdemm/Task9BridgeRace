using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float minX, maxX, y, minZ, maxZ;
    public GameObject blueCube, greenCube, purpleCube;

    private void Awake()
    {
        CalculateWights();
    }

    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    private void Start()
    {
        RandomCube();
    }
    private void Update()
    {
        if (GameManager.instance.blueList.Count < 10)
        {
            RandomCube();
        }
    }
    private void RandomCube()
    {
        for (int i = 0; i < 100; i++)
        {
            SpawnRandomCubes(new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ)));
        }
    }
    //public void BlueRandomCube()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        BlueCubeSpawner(new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ)));
    //    }
    //}
    //public void BlueCubeSpawner(Vector3 position)
    //{
    //    Instantiate(blueCube, position, Quaternion.identity, transform);
    //}
    private void SpawnRandomCubes(Vector3 position)
    {
        Cube randomCube = cubes[GetRandomCubeIndex()];
        Instantiate(randomCube.Prefab, position, Quaternion.identity, transform);
        Debug.Log("color= " + randomCube.name + ">*</color> Change: <b>" + randomCube.Chance + "</b>%");
        if (randomCube.name == "BlueCube")
        {
            GameManager.instance.blueList.Add(randomCube.Prefab);
        }
        if (randomCube.name == "GreenCube")
        {
            GameManager.instance.greenList.Add(randomCube.Prefab);
        }
        if (randomCube.name == "PurpleCube")
        {
            GameManager.instance.purpleList.Add(randomCube.Prefab);
        }
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
