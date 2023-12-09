using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookController : MonoBehaviour
{
    // Unity looks through the components attached to the GameObject that the script is attached to.
    // It searches for a component that matches the specified type T.
    // T component = gameObject.GetComponent<T>();
    // GetComponent<Rigidbody>()

    public GameObject notebookCover;
    private Vector3 initialCoverRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialCoverRotation = notebookCover.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenNotebook()
    {
        // Rotation along the parent's Z-axis
        notebookCover.transform.localRotation = Quaternion.Euler(initialCoverRotation.x, initialCoverRotation.y, 90);
    }

    public void CloseNotebook()
    {
        notebookCover.transform.localRotation = Quaternion.Euler(initialCoverRotation);
    }
}
