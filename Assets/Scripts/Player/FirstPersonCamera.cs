using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject playerBody;
    public float sensitivity = 2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        playerBody.transform.Rotate(0,sensitivity*mouseX,0);
      
        transform.Rotate(sensitivity*-mouseY,0,0);

        //float rotationS = Mathf.Clamp(transform.eulerAngles.x, 1, 60);
        // transform.rotation = Quaternion.Euler(rotationS, transform.eulerAngles.y, transform.eulerAngles.z);


    }
}
