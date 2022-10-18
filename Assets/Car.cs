using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float moveSpeed;
    int extent;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if ((transform.position.x < -extent - 1) || (transform.position.x > extent + 1))
        {
            Destroy(gameObject);
        }
    }

    public void SetUp(int _extent)
    {
        extent = _extent;
    }
}
