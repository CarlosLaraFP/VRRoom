using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script makes it easier to toggle between a new material, and the objects original material.
/// </summary>
public class ChangeMaterial : MonoBehaviour
{
    [Tooltip("The material that's switched to.")]
    public Material otherMaterial = null;

    private bool usingOther = false;
    private MeshRenderer meshRenderer = null;
    private Material originalMaterial = null;

    public Camera cameraLens;

    private void Awake()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    public void SetOtherMaterial()
    {
        usingOther = true;
        meshRenderer.material = otherMaterial;
    }

    public void SetOriginalMaterial()
    {
        usingOther = false;
        meshRenderer.material = originalMaterial;
    }

    public void ToggleMaterial()
    {
        usingOther = !usingOther;

        if (usingOther)
        {
            meshRenderer.material = otherMaterial;
        }
        else
        {
            meshRenderer.material = originalMaterial;
        }
    }

    // This approach is based on viewport coordinates, which are a normalized representation of where something appears in the camera's view.
    // These coordinates are independent of screen resolution & aspect ratio, which is why the check works regardless of specific Editor settings.
    bool IsObjectInCameraFOV(GameObject gameObject)
    {
        // Converts the world position of the GameObject to viewport space of the camera. In viewport coordinates, the bottom-left
        // of the camera is (0, 0), the top-right is (1, 1), and the Z value represents the distance from the camera in world units.
        Vector3 screenPoint = cameraLens.WorldToViewportPoint(gameObject.transform.position);
        // Check if the object is within the camera's viewport
        // In viewport space, a negative Z value means the point is behind the camera.
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }

    bool IsViewToGameObjectObstructed(GameObject gameObject)
    {
        RaycastHit hit;

        Vector3 direction = gameObject.transform.position - cameraLens.transform.position;

        if (Physics.Raycast(cameraLens.transform.position, direction, out hit))
        {
            // Check if the first object hit by the raycast is the target object
            return hit.transform != gameObject.transform;
        }

        return false;
    }

    // Checking if a specific GameObject is within the Field of View (FOV) of a camera involves calculating
    // whether the object is within the camera's frustum and is not obstructed from the camera's perspective.
    bool IsVisibleToCamera(GameObject gameObject)
    {
        bool isInFOV = IsObjectInCameraFOV(gameObject);
        //bool isObstructed = IsViewToGameObjectObstructed(gameObject);
        return isInFOV; //&& !isObstructed;
    }
}
