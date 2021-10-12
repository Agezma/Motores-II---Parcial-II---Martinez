using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointManager))]
public class WaypointManagerEditor : Editor
{
    private WaypointManager _target;

    void OnEnable()
    {
        _target = (WaypointManager)target;
        _target.allWaypoints = _target.GetComponentsInChildren<Waypoint>();
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < _target.allWaypoints.Length; i++)
        {
            var current = _target.allWaypoints[i].transform;

            for (int j = 0; j < _target.allWaypoints[i].nextPoint.Count; j++)
            {
                var next = _target.allWaypoints[i].nextPoint[j].transform;

                Vector3 dir = next.position - current.position;

                Handles.DrawLine(current.position,next.position);
                Handles.ConeHandleCap(0, current.position + dir/ 2f, Quaternion.LookRotation(dir, Vector3.up), 0.5f, EventType.Repaint);
                
            }

            current.position = Handles.PositionHandle(current.position, current.rotation);
            Handles.SphereHandleCap(0, current.position, Quaternion.identity, 0.5f, EventType.Repaint);
        }
    }
}
