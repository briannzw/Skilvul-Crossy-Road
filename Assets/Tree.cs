using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static List<Vector3> TreePositions = new List<Vector3>();

    private void OnEnable()
    {
        TreePositions.Add(transform.position);
    }

    private void OnDisable()
    {
        TreePositions.Remove(transform.position);
    }
}
