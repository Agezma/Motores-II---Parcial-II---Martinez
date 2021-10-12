using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Main : MonoBehaviour
{
    public GameObject boxCol;
    public GameObject sphereCol;
    public GameObject capsuleCol;

    private static Main _instance;

    public static Main Instance { get { return _instance; } }

    private void OnGUI()
    {       
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }  
}
