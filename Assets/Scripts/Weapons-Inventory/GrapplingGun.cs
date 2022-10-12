using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    public Transform originPos;
    public Transform player;
    public LayerMask groundMask;
    public Transform hook;
    private float grappleTime = 0;
    public LineRenderer lineRenderer;
    private bool canGrapple = false;
    private bool tooClose = false;
    public float unhookDistance = 5;
    public Transform grappleParent;
    public Transform grappleEx;

    //This is all to keep the line attached to grappling gun and avoid lagback
    private void OnEnable()
    {
        Application.onBeforeRender += UpdateLinePosition;
    }
    private void OnDisable()
    {
        Application.onBeforeRender -= UpdateLinePosition;
    }
    private void UpdateLinePosition()
    {
        lineRenderer.SetPosition(0, originPos.position);
    }
    private void LateUpdate()
    {
        UpdateLinePosition();
    }

    //Rest of the code goes in update
    void Update()
    {
        
        //This sets the hook's position, as well as the 2nd line renderer point
        lineRenderer.SetPosition(0, originPos.position);
        RaycastHit hit;
        RaycastHit hit2;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.DrawRay(originPos.position, -originPos.transform.up * 100000000f, Color.magenta, 10f);
            if (Physics.Raycast(originPos.position,-originPos.transform.up, out hit, Mathf.Infinity, groundMask))
            {
                Debug.Log(hit.point);
                hook.position = hit.point;

                
                lineRenderer.SetPosition(1, hit.point);
            }

        }
        grappleParent.rotation = grappleEx.rotation;
        if (Input.GetKey(KeyCode.Mouse0) && tooClose == false)
        {
            //This detects whether you "can grapple" and sets the boolean
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Physics.Raycast(originPos.position, -originPos.transform.up, out hit2, Mathf.Infinity, groundMask);
                if (hit2.transform.tag == "Ground")
                {
                    canGrapple = true;
                }
            }
            //If you "can grapple" then this lerps the player's position to the hook's position over a set time
            if (canGrapple == true)
            {
                grappleParent.LookAt(hook.position);
                player.position = Vector3.Lerp(player.position, hook.position, grappleTime);
                grappleTime = Mathf.Clamp(grappleTime + 0.5f*(Time.deltaTime / Vector3.Distance(player.position, hook.position)), 0, 1);
                lineRenderer.enabled = true;
                player.GetComponent<PlayerMovement>().gravity = 0;
                player.GetComponent<PlayerMovement>().velocity = new Vector3(0, 0, 0);
                player.GetComponent<PlayerMovement>().movingAllowed = false;
            }
            else
            {
                
            }
           
           
          
        }
        //This check if you are too close to the hook position, and if so it detaches the grapple
        if (Vector3.Distance(player.position, hook.position) < unhookDistance)
        {
            tooClose = true;
        }
        if (Vector3.Distance(player.position, hook.position) >= unhookDistance && !Input.GetKey(KeyCode.W))
        {
            tooClose = false;
        }
        if (!Input.GetKey(KeyCode.Mouse0) || tooClose == true)
        {
            
            canGrapple = false;
            grappleTime = 0;
            lineRenderer.enabled = false;
            player.GetComponent<PlayerMovement>().gravity = -29.81f;
            player.GetComponent<PlayerMovement>().movingAllowed = true;
            hook.position = new Vector3(100, 100, 100);
        }
    }
}
