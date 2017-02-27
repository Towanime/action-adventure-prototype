using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject target;
    public GameObject dummy;
    public float rotationSpeed;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = playerInput.rotation.x;//Input.GetAxis("Mouse X") * rotationSpeed;

        // rotate the camera around the avatar
        transform.RotateAround(target.transform.position, Vector3.up, horizontal * Time.deltaTime);
        // update dummy
        dummy.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //transform.LookAt(target.transform);
    }
}
