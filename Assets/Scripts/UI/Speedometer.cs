using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody car;
    public Text speedometer;

    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        speedometer.text = $"KPH\n{Mathf.Floor(car.velocity.magnitude *3.6f)}";
    }
}
