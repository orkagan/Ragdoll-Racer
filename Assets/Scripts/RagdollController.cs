using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    List<Rigidbody> rbLimbs;
    
    // Start is called before the first frame update
    void Start()
    {
        rbLimbs = new List<Rigidbody>();
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rbLimbs.Add(rb);
            rb.isKinematic = true;
            rb.gameObject.AddComponent<LimbCollision>().parentRagdoll = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            ActivateRagdoll();
        }*/
    }
    public void ActivateRagdoll()
    {
        foreach (Rigidbody rb in rbLimbs)
        {
            rb.isKinematic = false;
        }
    }
}

public class LimbCollision : MonoBehaviour
{
    public RagdollController parentRagdoll;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.1f)
        {
            parentRagdoll.ActivateRagdoll();
        }
    }
}