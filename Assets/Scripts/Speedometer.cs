using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody car;
    public Text speedometer;

    void Update()
    {
        speedometer.text = $"KPH\n{Mathf.Floor(car.velocity.magnitude *3.6f)}";
    }
}
