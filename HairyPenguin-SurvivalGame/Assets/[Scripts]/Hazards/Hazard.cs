using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float damage = 10.0f;
    public float cdTick = 0.5f; //how many seconds between damage-tick

    private float _timer = 0.0f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (_timer == 0.0f)
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<Animation>().Blend("CharacterArmature|Punch", 3.0f);
            other.GetComponent<PlayerHealthController>().setHealth(-10);
        }
        _timer += Time.fixedDeltaTime;
        if (_timer >= cdTick) _timer = 0.0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _timer = 0.0f;
    }
}
