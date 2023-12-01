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
        initialRotation = secondHand.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;

        //Debug.Log("Current Time: " + now.ToString());

        float hourDegrees = (now.Hour % 12) * 30 + now.Minute * 0.5f; // 12 hours -> 360 degrees, each hour is 30 degrees, add minutes
        float minuteDegrees = now.Minute * 6; // 60 minutes -> 360 degrees, each minute is 6 degrees
        float secondDegrees = now.Second * 6; // 60 seconds -> 360 degrees, each second is 6 degrees

        // When you multiply two quaternions, you effectively combine their rotations (non-commutative).

        hourHand.transform.localRotation = Quaternion.Euler(hourDegrees, 0, -90);
        minuteHand.transform.localRotation = Quaternion.Euler(minuteDegrees, 0, -90);
        secondHand.transform.localRotation = Quaternion.Euler(secondDegrees, 0, -90);
    }
}
