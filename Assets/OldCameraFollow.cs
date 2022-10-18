using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCameraFollow : MonoBehaviour
{
    public Player player;
    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    Vector3 targetAnimalPos;
    Vector3 lastAnimalPos;

    private void Update()
    {
        if (player == null) return;
        if (player.enabled == false) return;
        if (lastAnimalPos == player.transform.position) return;

        targetAnimalPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        transform.position = targetAnimalPos + offset;

        lastAnimalPos = player.transform.position;
    }
}
