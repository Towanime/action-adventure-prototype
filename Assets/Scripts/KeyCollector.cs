using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCollector : MonoBehaviour
{
    public Text lblKeys;
    public float hideLabelsAfter = 5;
    public int currentKeys;

    void Start()
    {
    }

    public void PickupKey()
    {
        CancelInvoke("HideCoinLabel");
        currentKeys++;
        lblKeys.gameObject.SetActive(true);
        lblKeys.text = GetKeyResult();
        Invoke("HideCoinLabel", hideLabelsAfter);
    }

    private void HideKeyLabel()
    {
        lblKeys.gameObject.SetActive(false);
    }
    
    public string GetKeyResult()
    {
        return " x " + currentKeys;
    }
}