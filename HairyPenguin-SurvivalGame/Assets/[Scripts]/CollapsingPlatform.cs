using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public float secondsToCollapse;

    public IEnumerator WaitAFewSecondsAndCollapse()
    {
        yield return new WaitForSeconds(secondsToCollapse);
        
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float angle = Vector3.Angle(other.GetContact(0).normal, Vector3.down); //angle between the normal of the collision and the vector down.
            
            if (angle < 20.0f) //the contact is under the player
            {
                Debug.Log("collision with collapsing platform");
                StartCoroutine(WaitAFewSecondsAndCollapse());
            }
            
        }
    }
}
