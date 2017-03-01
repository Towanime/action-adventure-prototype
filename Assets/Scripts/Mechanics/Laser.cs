using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer line;
    public GameObject emissor;
    public float damage;
    public float damageRate = 1;
    private DamageableEntity target;
    private bool firstHit;

    // Use this for initialization
    void Start () {
        line.enabled = false;
        StartCoroutine("FireLaser");
        //FireLaser();
    }
	
	// Update is called once per frame
	void Update () {

        //StartCoroutine("FireLaser");
    }

    private IEnumerator FireLaser()
    {
        line.enabled = true;
        while (true)
        {
            Ray ray = new Ray(emissor.transform.position, emissor.transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                target = hit.collider.gameObject.GetComponent<DamageableEntity>();
                if (!firstHit)
                {
                    DoDamage();
                    firstHit = true;
                }
            }
            else
            {
                target = null;
                firstHit = false;
                line.SetPosition(1, ray.GetPoint(100));
            }
            yield return null;
        }
    }

    /// <summary>
    /// Do damage on the hit entity every X seconds (damage rate)
    /// </summary>
    private void DoDamage()
    {
        if (target)
        {
            target.OnDamage(gameObject, damage);
            // call it again
            Invoke("DoDamage", damageRate);
        }
    }
}
