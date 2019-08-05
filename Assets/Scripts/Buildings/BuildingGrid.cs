using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    //Public
    public int debugGridWitdh = 10;
    public int debugGridHeight = 10;
    public float gridSpacing = 1.0f;

    public Vector3 GetNearestPoint(Vector3 position)
    {
        //Rounds the given position to the closest point on the grid
        position.x = Mathf.Round(position.x * 1 / gridSpacing) * gridSpacing;
        position.z = Mathf.Round(position.x * 1 / gridSpacing) * gridSpacing;

        return position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 1);

        for (int i = 0; i < debugGridWitdh; i++)
        {
            for (int j = 0; j < debugGridHeight; j++)
            {
                Gizmos.DrawCube(new Vector3(i * gridSpacing, 1, j * gridSpacing), new Vector3(0.1f, 0.1f, 0.1f));
            }
        }
    }
}
