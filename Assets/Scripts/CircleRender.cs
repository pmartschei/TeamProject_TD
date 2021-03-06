﻿using UnityEngine;
using System.Collections;


public class CircleRender : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        if (line == null) return;
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    public void SetRadius(float radius)
    {
        xradius = radius;
        yradius = radius;
        CreatePoints();
    }

    void CreatePoints()
    {
        if (line == null) return;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, z, y));

            angle += (360f / segments);
        }
    }
}
