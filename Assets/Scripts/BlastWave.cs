using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    public int pointsCount = 50;
    public float maxRadius = 25f;
    public float speed = 100f;
    public float startWidth = 5f;
    public float force = 5f;
    
    private LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;
    }

    private IEnumerator Blast()
    {
        float currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
    }

    private void Damage(float currentRadius)
    {
        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius);

        for (int i = 0; i < hittingObjects.Length; i++)
        {
            Rigidbody rb = hittingObjects[i].GetComponent<Rigidbody>();

            if (!rb) continue;
            Debug.Log(hittingObjects[i].name);
            Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;

            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Blast());
        }
    }
}
