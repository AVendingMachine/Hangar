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
    public float maxAmmo = 10;
    public float bulletsUsedPerShot = 1;
    private float currentAmmo;
    public int animLayer = 0;
    public float reloadDelay = 1f;


   


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }
    //garbage solution to animation delay

    
    IEnumerator AnimationHandler(string animationName, float delay)
    {
        animator.SetBool(animationName, true);
        yield return new WaitForSeconds(delay);
        animator.SetBool(animationName, false);
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }
    IEnumerator ResetAmmo()
    {
        yield return new WaitForSeconds(reloadDelay);
        currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        cooldown = Mathf.Clamp(cooldown -= Time.deltaTime,0,Mathf.Infinity);
        //Checks for mouse1 and for the lack of cooldown + having enough ammo
        if (Input.GetButtonDown("Fire1") && cooldown <= 0 && autoFire == false && currentAmmo >0)
        {
            FireBullet();
        }
        if (Input.GetButton("Fire1") && cooldown <= 0 && autoFire == true && currentAmmo >0)
        {
            FireBullet();
            Debug.Log("autofire proc");
        }
        //Checks for if you have 0 ammo and plays the reload animation if true, also resets currentammo to maxammo after
        if (currentAmmo <= 0)
        {
            animator.SetBool("NeedReload", true);
            StartCoroutine(AnimationHandler("NeedReload", reloadDelay));
            StartCoroutine(ResetAmmo());
        }

        
            


    }
    
    void FireBullet()
    {
        currentAmmo = Mathf.Clamp(currentAmmo - bulletsUsedPerShot,0,maxAmmo);
        StartCoroutine(AnimationHandler("Firing",0.05f));
        StartCoroutine(MuzzleFlash());
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
