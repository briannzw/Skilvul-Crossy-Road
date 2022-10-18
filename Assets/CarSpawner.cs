using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public TerrainBlock terrain;

    bool isRight;

    public float minSpawnTime, maxSpawnTime;

    private void Start()
    {
        isRight = (Random.value > 0.5f) ? true : false;

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime + 1));
        SpawnCar();
        StartCoroutine(Spawn());
    }

    void SpawnCar()
    {
        GameObject carGO = Instantiate(carPrefab,
            transform.position + new Vector3(((isRight) ? -(terrain.Extent + 1) : terrain.Extent + 1), 0, 0),
            Quaternion.Euler(0, (isRight) ? 90 : -90, 0), transform); ;
        Car car = carGO.GetComponent<Car>();
        car.SetUp(terrain.Extent);
    }
}
