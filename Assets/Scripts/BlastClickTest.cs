using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastClickTest : MonoBehaviour
{
    public GameObject blastPrefab;
    Vector3 offset;

    void Update()
    {

        if (Input.GetMouseButtonDown(0) & !MenuHandler.paused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Destroy(Instantiate(blastPrefab, hit.point, blastPrefab.transform.rotation),2f);
            }
        }
    }
}
