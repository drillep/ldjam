using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDamage : MonoBehaviour
{
    GameObject player;
    playerHealth PlayerHealth;
    public int falldamage;
    Animation anim;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = player.GetComponent<playerHealth>();	

	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject == player)
    //    {

    //    }
    //}

    //private void Update()
    //{
    //    if(PlayerHealth.currentHealth <= 0)
    //    {
    //        anim.SetTrigger("PlayerDead");
    //    }
    //} 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mathf.Abs(collision.relativeVelocity.y) > 10f)
        {
            falldamage = (int)((0.1f) * Mathf.Abs(collision.relativeVelocity.y) + 30f);
            Debug.Log(falldamage);
 
            PlayerHealth.TakeDamage(falldamage);
        }
    }

}
