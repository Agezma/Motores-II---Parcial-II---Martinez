using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public WaypointManager parent;

    public List<Waypoint> nextPoint;

    public void AddNextWp(Waypoint nxt)
    {
        nextPoint.Add(nxt);
    }

    public void RemoveFromNextWp(Waypoint nxt)
    {
        if (nextPoint.Contains(nxt)) nextPoint.Remove(nxt);
    }
}
