using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee1Movement : MonoBehaviour
{
    public Transform goal;
    private GameObject player;
    public float moveSpeed = 7f;
    public float standingRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, standingRange))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("player seen");
                agent.speed = 0f;
            }
            
        }
        
        agent.destination = player.transform.position;
    }
}
