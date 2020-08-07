using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightStartIntensity = 4f;
    [SerializeField] float lightStartAngle = 80f;
    [SerializeField] float lightLifetime = 30f;
    [SerializeField] int storedBatteries = 0;

    public float maximumBattery = 100f;
    float timeBetweenDecay = 1f;

    bool lightOn;

    float batteryDecay;
    float batteryLevel;
    Light myLight;

    void Start()
    {
        batteryDecay = maximumBattery / lightLifetime;
        batteryLevel = maximumBattery;

        myLight = GetComponent<Light>();

        myLight.intensity = lightStartIntensity;
        myLight.spotAngle = lightStartAngle;

        myLight.enabled = false;
        lightOn = false;
    }

    void Update()
    {
        ProcessKeyInput();

        if (lightOn && batteryLevel > Mathf.Epsilon)
        {
            StartCoroutine(DecreaseBatteryLife());
        }
        else if (lightOn && batteryLevel <= Mathf.Epsilon && storedBatteries > 0)
        {
            StopCoroutine(DecreaseBatteryLife());

            storedBatteries--;

            batteryLevel = maximumBattery;
        }
        else
        {
            StopCoroutine(DecreaseBatteryLife());
            myLight.enabled = false;
        }
    }

    public void AddStoredBattery()
    {
        storedBatteries++;
    }

    void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        lightOn = !lightOn;

        if (batteryLevel <= Mathf.Epsilon)
        {
            myLight.enabled = false;
        } else {
            myLight.enabled = !myLight.enabled;
        }
    }

    IEnumerator DecreaseBatteryLife()
    {
        batteryLevel -= batteryDecay * Time.deltaTime;
        print(batteryLevel);
        yield return new WaitForSeconds(timeBetweenDecay);
    }
}
