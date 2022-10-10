using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;
    public GameObject ragDoll;

    //currentHealth can be changed by outside forces, do not change max health in most cases
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("queen elizabeth");
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(ragDoll, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            
        }
    }

}
