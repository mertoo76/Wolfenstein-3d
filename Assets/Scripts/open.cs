using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour {

    GameObject player;
  
    
   

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
       
    }
	
	// Update is called once per frame
	void Update () {
     
        
    }



   private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(gameObject, 0f);
            
        }
    }
}
