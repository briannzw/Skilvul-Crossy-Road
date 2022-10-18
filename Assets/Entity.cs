using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject spawnPrefab;
    public float speed = 1;

    Player player;

    void Update()
    {
        if (transform.position.z <= player.CurrentTravel - 20)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(!player.isDead && transform.position.z <= player.transform.position.z)
        {
            player.isDead = true;
            Instantiate(spawnPrefab, player.transform.position, player.transform.rotation);
        }
    }

    public void SetUpTarget(Player target)
    {
        player = target;
    }
}
