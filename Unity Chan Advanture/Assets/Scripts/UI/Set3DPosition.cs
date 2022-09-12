using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set3DPosition : MonoBehaviour
{
    public Transform setTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Camera.main.WorldToScreenPoint(setTransform.position);
    }
}
