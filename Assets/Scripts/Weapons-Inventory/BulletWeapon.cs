using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : MonoBehaviour
{
    public float fireRate = 1f;
    private float cooldown;
    public Transform bulletPosition;
    public float damage = 1f;
    public Animator animator;
    public bool autoFire = false;
    public GameObject muzzleFlash;

   // public Transform ads;
  //  public Transform hipfire;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //garbage solution to animation delay

    
    IEnumerator AnimationHandler()
    {
        animator.SetBool("Firing", true);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("Firing", false);
        muzzleFlash.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
       
        cooldown = Mathf.Clamp(cooldown -= Time.deltaTime,0,Mathf.Infinity);
        //Checks for mouse1 and for the lack of cooldown
        if (Input.GetButtonDown("Fire1") && cooldown <= 0 && autoFire == false)
        {
            FireBullet();
        }
        if (Input.GetButton("Fire1") && cooldown <= 0 && autoFire == true)
        {
            FireBullet();
            Debug.Log("autofire proc");
        }
      
            


    }
    
    void FireBullet()
    {
        StartCoroutine(AnimationHandler());
        cooldown = fireRate;
        //Actually makes the raycast (Its in an if statement to stop an error if it hits nothing)
        RaycastHit hit;
        if (Physics.Raycast(bulletPosition.position, bulletPosition.transform.forward, out hit, Mathf.Infinity))
        {
            //Ray does damage if it is an enemy
            if (hit.transform.tag == "Enemy")
            {

                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
                Debug.Log("hit enemy");
                Debug.DrawRay(bulletPosition.position, bulletPosition.transform.forward * 100000000f, Color.red, 10f);
            }
            //No damage if its a non-enemy (does not include not hitting anything)
            else
            {
                Debug.Log("hit non enemy");
                Debug.DrawRay(bulletPosition.position, bulletPosition.transform.forward * 100000000f, Color.yellow, 10f);
            }
        }
        
    }
}
