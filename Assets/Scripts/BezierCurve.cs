using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    public static Vector3[] DrawCurve(Transform point0,Transform point2,Vector3 point1,int numPoints)
    {
        Vector3[] positions = new Vector3[numPoints];
        for (int i = 1; i < numPoints ; i++)
        {
            float t = i/(float)numPoints;
            positions[i] = calculateCurvePoint(t, point0.position, point1, point2.position);
        }
        positions[0] = point0.position;
        return positions;
    }

    private static Vector3 calculateCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u=1-t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}
