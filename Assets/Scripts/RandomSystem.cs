using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomSystem
{
    /// <summary>
    /// Returns a random value between two values.
    /// </summary>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns></returns>
    public static float RandomRange(float min, float max)
    {
        return Random.Range(min,max);
    }

    public static Vector2 RandomInsideCircle()
    {
        return Random.insideUnitCircle;
    }

    public static Vector3 RandomInsideSphere()
    {
        return Random.insideUnitSphere;
    }

    /// <summary>
    /// Returns a random value between 1 and 0
    /// </summary>
    /// <returns>Between 0 and 1</returns>
    public static float RandomValue()
    {
        return Random.value;
    }

    /// <summary>
    /// Returns a random value with a gaussian distribution
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}
