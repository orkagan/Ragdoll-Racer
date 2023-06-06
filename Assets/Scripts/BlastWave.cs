using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlastWave : MonoBehaviour
{
    public int pointsCount = 50;
    public float maxRadius = 25f;
    public float speed = 100f;
    public float startWidth = 5f;
    public float force = 10f;
    
    private LineRenderer lineRenderer;
    public UnityEvent onHit;
    public LayerMask enemyLayer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;
    }

    private IEnumerator Blast()
    {
        foreach(ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Clear();
            ps.time = 0;
            ps.Play();
        }

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
            rb.AddExplosionForce(force, transform.position, currentRadius);
        }
        hittingObjects = Physics.OverlapSphere(transform.position, currentRadius, enemyLayer);
        for (int i = 0; i < hittingObjects.Length; i++)
        {
            LimbCollision rdc = GetComponentInParent<LimbCollision>();
            if (rdc != null)
            {
                rdc.parentRagdoll.ActivateRagdoll();
                Debug.Log("found");
            }
            else Debug.Log("null rdc");
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
    private void Start()
    {
        StartCoroutine(Blast());
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Blast());
        }
    }*/
}
