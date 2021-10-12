using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurnOffRender))]
public class TurnOffRenderEditor : Editor
{
    TurnOffRender _target;

    private void OnEnable()
    {
        _target = (TurnOffRender)target;
    }

    private void OnSceneGUI()
    {
        GOCollider col = _target.GetComponentInParent<GOCollider>();

        if (col != null)
            col.sceneGUI();
    }
}
