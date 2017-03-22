using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Texture2D crosshairActiveTexture;
    //private GauntletController gauntlet;

    void Start()
    {
       // gauntlet = GetComponent<GauntletController>();
    }

    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 5);
    }

    void OnGUI()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        Texture2D activeTexture;
        /*if (gauntlet.GetTargetAnalyzer() != null && gauntlet.GetTargetAnalyzer().InSight())
        {
            activeTexture = gauntlet.GetTargetAnalyzer().GetInSightTexture();
        }
        else
        {
            activeTexture = crosshairTexture;
        }*/
        activeTexture = crosshairTexture;
        Rect position = new Rect((Screen.width - activeTexture.width) / 2, (Screen.height - activeTexture.height) / 2, activeTexture.width, activeTexture.height);
        GUI.DrawTexture(position, activeTexture);
    }
}