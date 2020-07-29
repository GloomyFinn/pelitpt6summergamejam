using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesBehavior : MonoBehaviour
{
    // "Bobbing" animation from 1D Perlin noise.

    // Range over which height varies.
    public float heightScale = 0.2f;

    // Distance covered per second along X axis of Perlin plane.
    public float xScale = 0.2f;


    void Update()
    {
        float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
        Vector3 pos = gameObject.transform.localPosition;
        pos.y = height;
        gameObject.transform.localPosition = pos;
    }
}
