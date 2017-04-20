using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Movement : MonoBehaviour {


    //
  public  Animator gameoverOrfinish;

    public int health = 50;
    public Text healthText,GameoverOrFinishText;

    public float speed = 3f, Camspeed = 50f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    

   

    void Awake()
    {
        floorMask = LayerMask.GetMask("Plane");
        playerRigidbody = GetComponent<Rigidbody>();
       

        


    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
         Move(h, v);
          Turning();
        healthText.text = "Health:" + health;
     
        if(health <= 0)
        {
            //Destroy(gameObject, 1f);
            gameoverOrfinish.SetTrigger("GameOver");
        }


        
    }



    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement * speed * Time.deltaTime;
        Quaternion rot = transform.rotation;
        movement = rot * movement;
         playerRigidbody.MovePosition(transform.position + movement);
     
    }

  
    void Turning()
    {
        Vector3 dene = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        transform.Rotate(dene.normalized * Time.deltaTime * Camspeed);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shootable")
        {
            health = health - 10;
        }

        if (collision.gameObject.name == "HealthPoint")
        {
            health = health + 20;
            Destroy(collision.gameObject, 0f);
        }

        if (collision.gameObject.name == "ArmorPoint")
        {
            GameObject child = transform.Find("Gun").gameObject;
            PlayerShooting shooter = child.GetComponent<PlayerShooting>();
            shooter.armor = shooter.armor + 20;
            Destroy(collision.gameObject, 0f);
            //Destroy(collision.gameObject, 1f);
        }


        if (collision.gameObject.name == "Finish")
        {

            GameoverOrFinishText.text = "Finish";
            gameoverOrfinish.SetTrigger("GameOver");
            //Destroy(collision.gameObject, 1f);
        }
    }
}
