using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketController : MonoBehaviour
{
    public XRBaseInteractable startingInteractable;

    // Start is called before the first frame update
    void Start()
    {
        var socket = this.GetComponent<XRSocketInteractor>();
        startingInteractable.transform.position = socket.transform.position;
        startingInteractable.transform.rotation = socket.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
