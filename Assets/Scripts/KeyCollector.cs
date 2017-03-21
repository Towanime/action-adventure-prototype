using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCollector : MonoBehaviour
{
    public Text lblKeys;
    public RawImage keyIcon;
    public float hideLabelsAfter = 5;
    public int currentKeys;

    void Start()
    {
    }

    public void PickupKey()
    {
        CancelInvoke("HideKeyLabel");
        currentKeys++;
        keyIcon.gameObject.SetActive(true);
        lblKeys.gameObject.SetActive(true);
        lblKeys.text = GetKeyResult();
        Invoke("HideKeyLabel", hideLabelsAfter);
    }

    private void HideKeyLabel()
    {
        lblKeys.gameObject.SetActive(false);
        keyIcon.gameObject.SetActive(false);
    }
    
    public string GetKeyResult()
    {
        return " x " + currentKeys;
    }
}