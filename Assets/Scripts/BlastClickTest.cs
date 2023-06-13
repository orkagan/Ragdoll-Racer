using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastClickTest : MonoBehaviour
{
    public GameObject blastPrefab;
    public float cooldownSeconds = 0.5f;

    float _cooldownTime;
    Vector3 offset;
    
    private void Start()
    {
        _cooldownTime = 0;
    }

    void Update()
    {
        if (MenuHandler.Instance.CurrentGameState!=GameState.Playing) return;

        if (Input.GetMouseButtonDown(0) & _cooldownTime<=0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Destroy(Instantiate(blastPrefab, hit.point, blastPrefab.transform.rotation),2f);
                _cooldownTime = cooldownSeconds;
            }
        }
        _cooldownTime = Mathf.Clamp(_cooldownTime - Time.deltaTime, 0, cooldownSeconds);
    }
}
