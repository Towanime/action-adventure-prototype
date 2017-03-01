using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DamageableEntity damageableEntity = other.gameObject.GetComponent<DamageableEntity>();
            if (damageableEntity)
            {
                damageableEntity.OnDamage(gameObject, damage);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }
}
