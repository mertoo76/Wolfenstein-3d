using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {



    LineRenderer gunLine;
    int shootableMask;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    public float timeBetweenBullets = 0.15f;
    float timer;


  public  int armor = 50;
    public Text armorText;

    //
    public GameObject projectile;
    //


    //
    Ray shootRay = new Ray();
    public float range = 100f;
    public int damagePerShot = 20;
    RaycastHit shootHit;

    //
    // Use this for initialization
    void Awake () {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && armor>0)
        {
            Shoot();
            armor--;
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        armorText.text = "Ammo:" + armor;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    void Shoot()
    {
        timer = 0f;

        gunLight.enabled = true;

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
      
        Vector3 screenCenterPoint = new Vector3(Screen.width / 2, Screen.height / 2);
        shootRay = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(shootRay, out shootHit, Camera.main.farClipPlane))
        {
           
            AInew aı = shootHit.collider.GetComponent<AInew>();
            if (aı != null)
            {
                aı.TakeDamage(damagePerShot);
            }
            gunLine.SetPosition(1, shootHit.point);
        }

      
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
