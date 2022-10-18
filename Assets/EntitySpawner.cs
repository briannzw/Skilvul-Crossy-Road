using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityPrefab;
    public Player player;
    public Vector3 spawnPos = new Vector3(0, 2, 7);
    public int timeOut = 10;

    float timer;
    int lastMaxTravel = 0;

    void SpawnEntity()
    {
        player.enabled = false;
        Vector3 position = new Vector3(player.transform.position.x + spawnPos.x, spawnPos.y, player.CurrentTravel + spawnPos.z);
        Quaternion rotation = Quaternion.Euler(0, 180, 0);
        GameObject entityObject = Instantiate(entityPrefab, position, rotation);
        Entity entity = entityObject.GetComponent<Entity>();
        entity.SetUpTarget(player);
    }

    private void Update()
    {
        if (player.isDead) return;

        timer += Time.deltaTime;
        if(timer > timeOut)
        {
            if (player.MaxTravel <= lastMaxTravel)
            {
                SpawnEntity();
                enabled = false;
            }
            lastMaxTravel = player.MaxTravel;
            timer = 0;
        }
    }
}
