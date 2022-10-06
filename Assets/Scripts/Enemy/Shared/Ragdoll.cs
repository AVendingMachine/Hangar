using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public float force;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(player.transform.forward * force);
        StartCoroutine(Freeze());
    }
    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
