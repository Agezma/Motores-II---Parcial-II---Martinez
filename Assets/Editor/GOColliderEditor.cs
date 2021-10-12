using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(GOCollider))]
public class GOColliderEditor : Editor
{
    GOCollider _target;
    GUIStyle titleStyle;

    bool arrayFold;

    List<GameObject> currentSons = new List<GameObject>();

    private void OnEnable()
    {
        _target = (GOCollider)target;
        _target.sceneGUI = OnSceneGUI;

        var a = _target.GetComponentsInChildren<TurnOffRender>();
        currentSons = a.Select(x => x.gameObject).ToList();
       
        titleStyle = new GUIStyle();
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.fontSize = 12;
    }


    private void OnSceneGUI()
    {
        Handles.BeginGUI();

        var v = EditorWindow.GetWindow<SceneView>().camera.pixelRect;

        DrawLayout(20, 20, v.width * 0.3f, v.height);
        Handles.EndGUI();
    }

    public void DrawLayout(float x, float y, float width, float height)
    {
        GUILayout.BeginArea(new Rect(x, y, width, height));
        var rec = EditorGUILayout.BeginVertical();
        GUI.color = new Color32(180, 180, 180, 255);
        GUI.Box(rec, GUIContent.none);


        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("My Game Object", titleStyle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        arrayFold = (EditorGUILayout.Foldout(arrayFold, "Colliders"));
        if (arrayFold)
        {
            arrayFold = true;
            for (int i = 0; i < currentSons.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Col:");
                currentSons[i] = (GameObject)EditorGUILayout.ObjectField(currentSons[i], typeof(GameObject), true);
                if (GUILayout.Button("Delete"))
                {
                    RemoveObject(currentSons[i]);
                }
                if (GUILayout.Button("Duplicate"))
                {
                    CreateEmptyObj(currentSons[i], currentSons[i].transform);
                }

                GUILayout.EndHorizontal();
            }
        }

        if (GUILayout.Button("Box Collider"))
        {
            CreateEmptyObj((GameObject)Resources.Load("BoxCollider"), _target.transform);
        }
        if (GUILayout.Button("Sphere Collider"))
        {
            CreateEmptyObj((GameObject)Resources.Load("SphereCollider"), _target.transform);
        }
        if (GUILayout.Button("Capsule Collider"))
        {
            CreateEmptyObj((GameObject)Resources.Load("CapsuleCollider"), _target.transform);
        }

        GUILayout.Space(5);
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }


    GameObject objToSpawn;
    void CreateEmptyObj(GameObject toInstantiate, Transform _transform)
    {
        objToSpawn = Instantiate(toInstantiate);

        objToSpawn.transform.position = _transform.position;
        objToSpawn.transform.rotation = _transform.rotation;
        objToSpawn.transform.parent = _target.transform;

        //Selection.activeObject = objToSpawn;
        _target.sceneGUI = OnSceneGUI;
        //objToSpawn.GetComponent<TurnOffRender>().parentGUI = OnSceneGUI;
        Selection.activeTransform = objToSpawn.transform;
        currentSons.Add(objToSpawn);
    }

    void RemoveObject(GameObject toDestroy)
    {
        if (currentSons.Contains(toDestroy))
            currentSons.Remove(toDestroy);
        GameObject.DestroyImmediate(toDestroy);
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

}
