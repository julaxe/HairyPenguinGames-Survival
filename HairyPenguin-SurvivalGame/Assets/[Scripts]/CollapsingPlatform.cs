using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public float secondsToCollapse;
    public float secondsToRespawn;
    public GameObject platform;
  
    public IEnumerator WaitAFewSecondsAndCollapse()
    {
        yield return new WaitForSeconds(secondsToCollapse);

        platform.gameObject.SetActive(false);

        StartCoroutine(Respawn());
        //Destroy(this.gameObject);
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(secondsToRespawn);

        platform.gameObject.SetActive(true);
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        float angle = Vector3.Angle(other.GetContact(0).normal, Vector3.down); //angle between the normal of the collision and the vector down.
            
    //        if (angle < 20.0f) //the contact is under the player
    //        {
    //            Debug.Log("collision with collapsing platform");
    //            StartCoroutine(WaitAFewSecondsAndCollapse());
    //        }
            
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with collapsing platform");
            StartCoroutine(WaitAFewSecondsAndCollapse());
        }
    }
}
