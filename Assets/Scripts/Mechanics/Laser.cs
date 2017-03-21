using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer line;
    public GameObject emissor;
    public float damage;
    public float damageRate = 1;
    public float distance = 20;
    private DamageableEntity target;
    private SwitchActivator switchActivator;
    private SwitchActivator lastSwitchActivator;
    private bool firstHit;

    // Use this for initialization
    void Start () {
        StartCoroutine("FireLaser");
    }
	
    private IEnumerator FireLaser()
    {
        while (true)
        {
            Ray ray = new Ray(emissor.transform.position, emissor.transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, distance))
            {
                //Debug.Log("Hitting: " + hit.collider.name + " with damageable entity? " + (target != null));
                line.SetPosition(1, hit.point);
                target = hit.collider.gameObject.GetComponent<DamageableEntity>();
                switchActivator = hit.collider.gameObject.GetComponent<SwitchActivator>();
                if (hit.collider.CompareTag("Player") && target != null && !firstHit)
                {
                    DoDamage();
                    firstHit = true;
                }
                else
                {
                    // reset hit if it's not a damageable entity
                    firstHit = false;
                }
                // check if switch 
                //switchActivator = hit.collider.gameObject.GetComponent<SwitchActivator>();
                if (switchActivator != null && switchActivator != lastSwitchActivator)
                {
                    switchActivator.Activate(gameObject);
                    lastSwitchActivator = switchActivator;
                }
                // check if previous switch is differnt or it's not hitting a switch anymore
                if (lastSwitchActivator != null && switchActivator == null)//(switchActivator == null || lastSwitchActivator != switchActivator))
                {
                    //Debug.Log("Desactivate wat");
                    lastSwitchActivator.Desactivate();
                    lastSwitchActivator = null;
                }
                //lastSwitchActivator = switchActivator;
            }
            else
            {
                if (switchActivator != null)
                {
                    switchActivator.Desactivate();
                    switchActivator = null;
                    lastSwitchActivator = null;
                }
                target = null;
                firstHit = false;
                line.SetPosition(1, ray.GetPoint(distance));
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
