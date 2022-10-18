using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBlock : MonoBehaviour
{
    public GameObject main;
    public GameObject repeat;

    private int extent;

    public int Extent { get => extent; }

    public void Build(int _extent)
    {
        extent = _extent;

        for (int i = -1; i <= 1; i++)
        {
            if (i == 0) continue;
            GameObject mainGO = Instantiate(main, transform);
            mainGO.transform.localPosition = new Vector3(i * (extent + 1), 0, 0);
            mainGO.transform.localScale = new Vector3(1, 1, 1);
            mainGO.transform.GetComponentInChildren<Renderer>().material.color *= Color.grey;
        }

        main.transform.localScale = new Vector3(extent * 2 + 1, main.transform.localScale.y, main.transform.localScale.z);

        if (repeat == null) return;

        for(int x = -(extent + 1); x <= extent + 1; x++)
        {
            if (x == 0) continue;
            GameObject r = Instantiate(repeat, transform);
            r.transform.localPosition = new Vector3(x, 0, 0);
        }
    }
}
