using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    private bool isHovering = true;

    //private Quaternion initialRotation;

    void Start()
    {
        // local and global coordinate systems are the same
        //initialRotation = this.transform.localRotation;
    }

    void Update()
    {
        if (isHovering)
        {
            //this.transform.rotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0) * this.transform.rotation;
            //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            transform.Rotate(0, 3, 0, Space.World);
        }
    }

    public void StartRotation()
    {
        isHovering = true;
    }

    public void StopRotation()
    {
        isHovering = false;
    }
}
