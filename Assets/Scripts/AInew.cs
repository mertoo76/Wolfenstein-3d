using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInew : MonoBehaviour {

    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float enemyMovementSpeed;
    public float damping;
    public Transform fpsTarget; 
    Rigidbody theRigidbody;
    Renderer myRenderer;

    //
    public int startingHealth = 60;
    public int currentHealth;
    public bool isDead=false;
        //

    // Use this for initialization
    void Start () {
        myRenderer = GetComponent<Renderer>();
        theRigidbody = GetComponent<Rigidbody>();


        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
        if (fpsTargetDistance < enemyLookDistance)
        {
            myRenderer.material.color = Color.yellow;
            LookAtPlayer();
        }
        if (fpsTargetDistance < attackDistance)
        {
            attack();
            myRenderer.material.color = Color.red;
        }
        else
        {
            myRenderer.material.color = Color.black;
        }
	}

    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
    void attack()
    {
        if (!isDead)
        {
            theRigidbody.AddForce(transform.forward * enemyMovementSpeed);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }

    }

    void Death()
    {
        isDead = true;
        CapsuleCollider capsul = GetComponent<CapsuleCollider>();
        capsul.enabled = false;
        Destroy(gameObject, 2f);

       
    }
}
