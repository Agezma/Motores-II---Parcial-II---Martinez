using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    private Waypoint _wp;
    Waypoint[] allPoints;

    void OnEnable()
    {
        _wp = (Waypoint)target;
        _wp.parent = _wp.GetComponentInParent<WaypointManager>();
        allPoints = _wp.parent.allWaypoints;
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        for (int i = 0; i < allPoints.Length; i++)
        {
            Handles.SphereHandleCap(0, allPoints[i].transform.position, Quaternion.identity, 0.5f, EventType.Repaint);
            if (allPoints[i] != _wp && !_wp.nextPoint.Contains(allPoints[i]))
            {
                DrawButton("Connect", allPoints[i].transform.position, allPoints[i].transform.position, ButtonType.addConection, allPoints[i]);
            }
            else if (allPoints[i] != _wp)
            {
                DrawButton("BreakConection", allPoints[i].transform.position, allPoints[i].transform.position, ButtonType.breakConection, allPoints[i]);
            }
        }
        Handles.EndGUI();

        for (int i = 0; i < allPoints.Length; i++)
        {
            Handles.SphereHandleCap(0, allPoints[i].transform.position, Quaternion.identity, 0.5f, EventType.Repaint);
        }
        for (int i = 0; i < _wp.nextPoint.Count; i++)
        {
            Handles.DrawLine(_wp.transform.position, _wp.nextPoint[i].transform.position);

            Vector3 dir = _wp.nextPoint[i].transform.position - _wp.transform.position;
            Handles.ConeHandleCap(0, _wp.transform.position + dir / 2f, Quaternion.LookRotation(dir, Vector3.up), 0.5f, EventType.Repaint);

        }
    }

    public enum ButtonType
    {
        addConection,
        breakConection
    }

    private void DrawButton(string text, Vector3 position, Vector3 dir, ButtonType typ, Waypoint wp)
    {
        var p = Camera.current.WorldToScreenPoint(position);
        var size = 3000 / Vector3.Distance(Camera.current.transform.position, position) ;
        var r = new Rect(p.x - size / 2, Screen.height - p.y - size, size, size / 2);

        GUIStyle buttonStyle = new GUIStyle();
        buttonStyle.stretchHeight = true;
        buttonStyle.stretchWidth = true;

        if (GUI.Button(r, text))
        {
            switch (typ)
            {
                case ButtonType.addConection:
                    _wp.AddNextWp(wp);

                    break;
                case ButtonType.breakConection:
                    _wp.RemoveFromNextWp(wp);
                    break;
            }
        }
    }
}
