using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class TurnOffRender : MonoBehaviour
{  
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
