using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLaser : MonoBehaviour {

    public LineRenderer line;
    public GameObject emissor;
    private GameObject target;
    private bool active;

    // Use this for initialization
    void Start()
    {
        //line.enabled = false;
        //active = true;
        //StartCoroutine("FireLaser");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator FireLaser()
    {
        //line.enabled = true;
        while (active)
        {
            Ray ray = new Ray(emissor.transform.position, emissor.transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                target = hit.collider.CompareTag("Manipulable") ? hit.collider.gameObject : null;
            }
            else
            {
                target = null;
                line.SetPosition(1, ray.GetPoint(10));
            }
            yield return null;
        }
    }

    public void Activate()
    {
        active = true;
        StartCoroutine("FireLaser");
    }
    
    public void Deactivate()
    {
        active = false;
    }

    public GameObject GetTarget()
    {
        return this.target;
    }
}
