using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public TerrainBlock terrain;
    public int count = 3;

    List<Vector3> emptyPos = new List<Vector3>();

    private void Start()
    {
        for(int x = -terrain.Extent; x <= terrain.Extent; x++)
        {
            if (transform.position.z == 0 && x == 0) continue;

            emptyPos.Add(transform.position + Vector3.right * x);
        }

        for(int i = 0; i < count; i++)
        {
            int index = Random.Range(0, emptyPos.Count);
            Vector3 spawnPos = emptyPos[index];
            Instantiate(treePrefab, spawnPos, Quaternion.identity, transform);
            emptyPos.RemoveAt(index);
        }

        Instantiate(treePrefab, transform.position + Vector3.right * -(terrain.Extent + 1), Quaternion.identity, transform);
        Instantiate(treePrefab, transform.position + Vector3.right * (terrain.Extent + 1), Quaternion.identity, transform);
    }
}
