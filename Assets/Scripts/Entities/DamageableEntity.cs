using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public bool ignoreDamage = false;
    public float life;
    protected float currentLife;

    void Start()
    {
        currentLife = life;
    }

    public virtual bool OnDamage(GameObject origin, float damage)
    {
        Debug.Log("Damage on object: " + gameObject.name);
        if (ignoreDamage) return false;
        ModifyCurrentLife(damage);
        if (currentLife <= 0)
        {
            OnDeath();
        }
        return true;
    }

    protected virtual void ModifyCurrentLife(float damage)
    {
        currentLife = Mathf.Max(currentLife - damage, 0);
    }

    protected virtual void OnDeath()
    {
        Debug.Log("Destroyed!");
        Refresh();
        //Destroy(gameObject);
    }

    public virtual void Refresh()
    {
        currentLife = life;
    }

    public float CurrentLife
    {
        get { return currentLife; }
    }

    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            currentLife = value;
        }
    }
}
