using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public GameObject hourHand;
    public GameObject minuteHand;
    public GameObject secondHand;

    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        // all clock hands have the same initial rotation
        initialRotation = secondHand.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;

        // The hour hand also moves slightly as the minutes pass. In one hour (60 minutes), the hour hand moves 30 degrees.
        // Therefore, for each minute, the hour hand moves 30 degrees / 60 minutes = 0.5 degrees per minute
        float hourDegrees = (now.Hour % 12) * 30 + now.Minute * 0.5f; // 12 hours -> 360 degrees, each hour is 30 degrees, add minutes
        float minuteDegrees = now.Minute * 6; // 60 minutes -> 360 degrees, each minute is 6 degrees
        float secondDegrees = now.Second * 6; // 60 seconds -> 360 degrees, each second is 6 degrees

        // When you multiply two quaternions, you effectively combine their rotations (non-commutative).
        // The order you choose determines whether the additional rotation is applied in the local space of the object (initialRotation * additionalRotation) or in world space (additionalRotation * initialRotation).
        // localRotation angles are relative to the parent's coordinate system
        hourHand.transform.localRotation = Quaternion.Euler(hourDegrees, 0, 0) * initialRotation;
        minuteHand.transform.localRotation = Quaternion.Euler(minuteDegrees, 0, 0) * initialRotation;
        secondHand.transform.localRotation = Quaternion.Euler(secondDegrees, 0, 0) * initialRotation;
    }
}
/*
    Non-Frame-Rate Dependent: The rotation of the clock hands is directly set to match the current time and does not rely on the 
    frame rate of the game. Whether the game runs at 30 FPS or 60 FPS, the clock hands will point to the exact time based on DateTime.Now.

    No Cumulative Movement: Unlike movements that accumulate over time (like moving an object across the screen), 
    the clock hands' rotation is calculated fresh each frame based on the current time, without accumulating or depending on previous frames.
 */

/*
    In Unity, the order in which you multiply quaternions is crucial because quaternion multiplication is not commutative. 
    This means that A * B does not equal B * A when A and B are quaternions. The order determines how one rotation is applied relative to another.

    Let's break down why additionalRotation * initialRotation worked for your scenario, whereas initialRotation * additionalRotation did not:

    initialRotation * additionalRotation

    Global Then Local: When you multiply in this order, it means you first apply the initialRotation in the global space (relative to the world), and then you apply the additionalRotation in the local space of the object (as if the object had already been rotated by the initialRotation).
    
    Effect: This is like saying, "Rotate the object to its initial orientation, and then, considering this new orientation, apply the additional rotation."
    
    additionalRotation * initialRotation

    Local Then Global: This order applies the additionalRotation first, as if the object had no initial rotation (i.e., in the object's local space), and then applies the initialRotation in the global space.
    
    Effect: This is like saying, "Rotate the object as per the additional rotation as if it were in its default orientation, and then, considering the entire scene, apply the initial rotation."
    
    Why the Latter Worked for Your Clock
    
    For the clock hands in your scenario, the additionalRotation represents the rotation based on the current time, and initialRotation represents the fixed starting orientation of the hands in the scene. By using additionalRotation * initialRotation:

    You first apply the time-based rotation as if the clock hands were starting from a standard reference point (e.g., pointing to 12 o'clock).
    Then, you apply the initialRotation to orient the hands correctly in your specific clock model. This ensures that 
    the rotation for the time is correctly represented relative to your clock's unique starting orientation.
    In simpler terms, this order correctly accounts for the fact that your clock hands don't start at the 'default' rotation 
    (e.g., pointing upwards) but have a predefined orientation that needs to be considered after applying the time-based rotation. 
    This approach is especially necessary when dealing with 3D models that don't start in a 'neutral' rotation 
    but have an initial orientation that must be factored into subsequent rotations.
 */
