using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
	List<Rigidbody> rbLimbs;
	Collider boxTrigger;

	void Start()
	{
		rbLimbs = new List<Rigidbody>();
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
		{
			rbLimbs.Add(rb);
			rb.gameObject.GetComponent<Collider>().isTrigger = true;
			rb.isKinematic = true;
			rb.gameObject.AddComponent<LimbCollision>().parentRagdoll = this;
		}
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.R)) ActivateRagdoll();
	}
	public void ActivateRagdoll()
	{
		foreach (Rigidbody rb in rbLimbs)
		{
			rb.gameObject.GetComponent<Collider>().isTrigger = false;
			rb.isKinematic = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			ActivateRagdoll();
		}
	}
}

public class LimbCollision : MonoBehaviour
{
	public RagdollController parentRagdoll;
	/*private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.1f)
        {
            parentRagdoll.ActivateRagdoll();
        }
    }*/
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			parentRagdoll.ActivateRagdoll();
			Debug.Log("Ragdoll limb triggered");
		}
	}
}