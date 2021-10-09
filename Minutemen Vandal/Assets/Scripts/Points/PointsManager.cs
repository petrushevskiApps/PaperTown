using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public float pointsPerVolumeUnit = 100f;
    public int Points { get; private set; } = 0;

    private Dictionary<Color, List<ScoredPoint>> scoredPointsPerColor = new Dictionary<Color, List<ScoredPoint>>();

    // Use this to score blob hits after a collision.
    public void ScoreHit(Color color, Vector3 position, float radius)
    {
        List<ScoredPoint> scoredPointsForColor = new List<ScoredPoint>();
        bool colorKeyExists = scoredPointsPerColor.TryGetValue(color, out scoredPointsForColor);
        if (!colorKeyExists)
        {
            scoredPointsPerColor.Add(color, scoredPointsForColor);
        }

        CalculateScoreForPoint(scoredPointsForColor, position, radius);
    }

    private void CalculateScoreForPoint(List<ScoredPoint> scoredPointsForColor, Vector3 position, float radius)
    {
        var biggestIntersectingVolume = 0f;
        var volumeOfSphereToScore = SphereVolume(radius);
        foreach (var existingScoredPoint in scoredPointsForColor)
        {
            var distance = Vector3.Distance(existingScoredPoint.position, position);
            float intersectionVolume = SphereIntersectionVolume(existingScoredPoint.radius, radius, distance);
            if (intersectionVolume > volumeOfSphereToScore / 2)
            {
                // Do not score, more than half of the sphere is in another one.
                return;
            }
            if (intersectionVolume > biggestIntersectingVolume)
            {
                biggestIntersectingVolume = intersectionVolume;
            }
        }

        var volumeToScore = volumeOfSphereToScore - biggestIntersectingVolume;
        int pointsToAdd = (int)(volumeToScore * pointsPerVolumeUnit);
        Points += pointsToAdd;
        ScoredPoint scoredPoint = new ScoredPoint(position, radius, pointsToAdd);
        scoredPointsForColor.Add(scoredPoint);
    }

    private float SphereVolume(float r)
    {
        return 4 * Mathf.PI * Mathf.Pow(r, 3) / 3;
    }

    /// <summary>
    /// https://math.stackexchange.com/questions/863486/volume-of-cavity-between-intersecting-multiple-spheres
    /// </summary>
    /// <param name="r">Radius sphere 1.</param>
    /// <param name="R">Radius sphere 2.</param>
    /// <param name="d">Distance between centers.</param>
    /// <returns></returns>
    private float SphereIntersectionVolume(float r, float R, float d)
    {
        if (r + R < d)
        {
            // If sum of radii is greater than center distance, the spheres are not intersecting => intersection volume is 0.
            return 0;
        }
        return Mathf.PI * Mathf.Pow(R + r - d, 2) * (Mathf.Pow(d, 2) + 2 * d * r - 3 * Mathf.Pow(r, 2) + 2 * d * R - 3 * Mathf.Pow(R, 2) + 6 * r * R) / (12 * d);
    }

    public void Reset()
    {
        Points = 0;
        scoredPointsPerColor = new Dictionary<Color, List<ScoredPoint>>();
    }
}

class ScoredPoint
{
    public Vector3 position { get; set; }
    public float radius { get; set; }
    public float points { get; set; }

    public ScoredPoint(Vector3 position, float radius, float points)
    {
        this.position = position;
        this.radius = radius;
        this.points = points;
    }
}