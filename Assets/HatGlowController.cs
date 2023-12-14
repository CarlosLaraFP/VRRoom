using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class HatGlowController : MonoBehaviour
{
    // allows a private field to be serialized and exposed in the Editor
    [SerializeField]
    private Camera headset;

    [SerializeField]
    [Min(0)]
    private float maxIntensity = 8f;

    [SerializeField]
    [Min(0)]
    private float minDistanceForMaxIntensity = 1f;

    private Renderer meshRenderer;
    private Material material;
    private Color initialColor;
    private const string EMISSIVE_COLOR_NAME = "_EmissionColor";
    private const string EMISSIVE_KEYWORD = "_EMISSION";

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();

        if (meshRenderer.material.enabledKeywords.Any(keyword => keyword.name == EMISSIVE_KEYWORD)
            && meshRenderer.material.HasColor(EMISSIVE_COLOR_NAME))
        {
            material = meshRenderer.material;
            initialColor = material.GetColor(EMISSIVE_COLOR_NAME);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Change material/lit shader emission map intensity from 0 to 7.5

        if (meshRenderer.isVisible)
        {
            float euclideanDistance = Vector3.Distance
            (
                headset.transform.position, 
                this.transform.position
            );

            float newIntensity = CalculateIntensity(euclideanDistance);

            SetEmissionIntensity(newIntensity);
        }
    }

    /*
        Calculates an intensity value that is inversely proportional to the distance between two objects. 
        As they get closer, the intensity ramps up, reaching its peak when the distance is at or below a defined minimum threshold.
     */
    private float CalculateIntensity(float distance)
    {
        float normalizedDistance = Mathf.Clamp(distance / minDistanceForMaxIntensity, 0f, 1f);

        // provides a smooth transition between the maximum intensity and zero based on the normalized distance
        return Mathf.Lerp(maxIntensity, 0f, normalizedDistance);
    }

    private void SetEmissionIntensity(float intensity)
    {
        material.SetColor(EMISSIVE_COLOR_NAME, new Color
        (
            initialColor.r * Mathf.Pow(2, intensity),
            initialColor.g * Mathf.Pow(2, intensity),
            initialColor.b * Mathf.Pow(2, intensity),
            initialColor.a
        ));
    }

    /*
        Unity internally scales the RGB values of the emission color based on the intensity slider's value. 
        This slider essentially multiplies the RGB components of the emission color to increase their intensity.
     */
}
