using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightStartIntensity = 4f;
    [SerializeField] float lightStartAngle = 80f;

    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;

    [SerializeField] float minimumAngle = 40f;
    [SerializeField] float lightLifetime = 60f;
    float timeBetweenDecay;
    Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenDecay = lightLifetime / (lightStartIntensity / lightDecay);

        myLight = GetComponent<Light>();
        myLight.intensity = lightStartIntensity;
        myLight.spotAngle = lightStartAngle;
    }

    // Update is called once per frame
    void Update()
    {
        // if light is ON
        //StartCoroutine(DecreaseLightAngle());
        StartCoroutine(DecreaseLightIntensity());
        //DecreaseLightIntensity();

        // else do nothing
    }

    IEnumerator DecreaseLightAngle()
    {
        if (myLight.spotAngle >= minimumAngle)
        {
            myLight.spotAngle -= angleDecay;
            yield return new WaitForSeconds(timeBetweenDecay);
        }
    }

    IEnumerator DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay;
        yield return new WaitForSeconds(10f);
    }
}
