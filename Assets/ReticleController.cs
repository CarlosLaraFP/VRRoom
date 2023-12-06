using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    private bool isHovering = true;

    public float pulseMagnitude = 0.1f; // The amount by which the reticle will increase and decrease in size
    public float pulseSpeed = 2.0f; // Speed of the pulsing effect
    private float pulseTimer = 0.0f;
    private Vector3 baseScale;

    //private Quaternion initialRotation;

    void Start()
    {
        baseScale = this.transform.localScale;
    }

    void Update()
    {
        if (isHovering)
        {
            this.transform.localRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0) * this.transform.localRotation;
            //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            //transform.Rotate(0, 3, 0, Space.World);

            // Continuous pulsing effect
            pulseTimer += Time.deltaTime; // Counts up in seconds
            float scale = 1.0f + Mathf.Sin(pulseTimer * pulseSpeed) * pulseMagnitude;
            this.transform.localScale = baseScale * scale;
        }
        else
        {
            // Reset to base scale when not hovering
            transform.localScale = baseScale;
            pulseTimer = 0.0f; // Reset the pulse timer
        }
    }

    public void StartAnimation()
    {
        isHovering = true;
    }

    public void StopAnimation()
    {
        isHovering = false;
    }
}

/*
    FixedUpdate Method: In physics calculations, you often use FixedUpdate instead of Update. 
    FixedUpdate runs at a consistent rate, making it ideal for physics-related calculations. 
    In this case, Unity handles the time step internally, so you typically don't use Time.deltaTime 
    (you would use Time.fixedDeltaTime if needed, but most physics calculations in FixedUpdate don’t require it).
 */