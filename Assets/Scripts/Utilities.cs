using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utilities {

    public static Vector2 CalculateCurvePosition(List<Vector2> points, float fraction)
    {
        if (points.Count == 2)
        {
            return Vector2.Lerp(points[0], points[1], fraction);
        }
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count - 1; i++)
        {
            newPoints.Add(Vector2.Lerp(points[i], points[i + 1], fraction));
        }
        return CalculateCurvePosition(newPoints, fraction);
    }
}
