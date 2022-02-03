using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoulder : MonoBehaviour
{
    public GameObject boulder;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        if (!other.gameObject.CompareTag("Player")) return;
        boulder.SetActive(true);
        activated = true;
    }
}
