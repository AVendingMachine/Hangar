using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float attackRate = 1f;
    private float cooldown;
    bool attacking = false;
    public Transform target;
    public Transform originTarget;
    private float lerpinTime = 0;
    public float swingSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(originTarget.position, target.position, lerpinTime);
        transform.eulerAngles = Vector3.Lerp(originTarget.eulerAngles, target.eulerAngles, lerpinTime);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attacking = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attacking = false;
        }
        if (attacking == true)
        {
            lerpinTime = Mathf.Clamp(lerpinTime + Time.deltaTime*swingSpeed, 0, 1);
        }
        if (attacking == false)
        {
            lerpinTime = Mathf.Clamp(lerpinTime - Time.deltaTime*swingSpeed, 0, 1);
        }
    }
}
