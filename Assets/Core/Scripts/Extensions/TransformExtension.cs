using System;
using UnityEngine;

public static class TransformExtension
{
    public static Vector3 RoundAll(this Vector3 vector, int digits = 1)
    {
        vector.x = (float)Math.Round(vector.x * 2, MidpointRounding.AwayFromZero) / 2;
        vector.y = (float)Math.Round(vector.y * 2, MidpointRounding.AwayFromZero) / 2;
        vector.z = (float)Math.Round(vector.z * 2, MidpointRounding.AwayFromZero) / 2;
        Debug.Log((float)Math.Round(vector.z * 2, MidpointRounding.AwayFromZero) / 2);
        Debug.Log(((float)Math.Round(vector.x * 2, MidpointRounding.AwayFromZero) / 2));
        Debug.Log("__________");
        return vector;
    }
}
