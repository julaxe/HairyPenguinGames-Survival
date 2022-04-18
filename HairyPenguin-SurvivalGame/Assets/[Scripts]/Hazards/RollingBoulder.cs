using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBoulder : Hazard
{
    private Rigidbody rb;

    [Header("Boulder")]
    public float minimumVelForCollision = 1.5f;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (rb.velocity.magnitude < minimumVelForCollision) return;
        other.gameObject.GetComponent<AudioSource>().Play();
        other.gameObject.GetComponent<PlayerHealthController>().setHealth(-100);
        //other.gameObject.GetComponent<Animation>().Blend("CharacterArmature|Punch", 3.0f);
    }
}
