using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    public bool braking;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBrakeTorque;

    public float handbrakeSlip = 1f;
    private WheelFrictionCurve _originalWFC;
    private WheelFrictionCurve _driftWFC;

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass += new Vector3(0, -0.2f, 0);
        _originalWFC = axleInfos[0].leftWheel.sidewaysFriction;
        _driftWFC = _originalWFC;
        _driftWFC.extremumSlip = handbrakeSlip;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;

            }
            if (axleInfo.braking)
            {
                //braking for left wheel
                if ((axleInfo.leftWheel.rpm > 0 && motor < 0) || //going forward and pressing back
                    (axleInfo.leftWheel.rpm < 0 && motor > 0)) //going backward and pressing forward
                {
                    //brake
                    axleInfo.leftWheel.brakeTorque = maxBrakeTorque;
                    Debug.Log("Braking");
                }
                else axleInfo.leftWheel.brakeTorque = 0;
                //braking for right wheel
                if ((axleInfo.rightWheel.rpm > 0 && motor < 0) || //going forward and pressing back
                    (axleInfo.rightWheel.rpm < 0 && motor > 0)) //going backward and pressing forward
                {
                    //brake
                    axleInfo.rightWheel.brakeTorque = maxBrakeTorque;
                    Debug.Log("Braking");
                }
                else axleInfo.rightWheel.brakeTorque = 0;
            }

            //E-Brake
            
            if (Input.GetButton("Jump"))
            {
                axleInfo.rightWheel.sidewaysFriction = _driftWFC;
                axleInfo.leftWheel.sidewaysFriction = _driftWFC;
            }
            else
            {
                axleInfo.rightWheel.sidewaysFriction = _originalWFC;
                axleInfo.leftWheel.sidewaysFriction = _originalWFC;
            }

            //Reset car (flip car back over)
            if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 fwd = transform.forward;
                transform.rotation = Quaternion.identity;
                transform.forward = fwd;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        //do nothing if collider doesn't have children
        if (collider.transform.childCount == 0) return;

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}