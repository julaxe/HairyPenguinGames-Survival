using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickUpRadius : MonoBehaviour
{
    private GameObject _playerRef = null;
    private PickUp _pickUp;
    void Start()
    {
        _pickUp = transform.parent.GetComponent<PickUp>();
    }

    private void Update()
    {
        if (_playerRef)
        {
            //if (_playerRef.GetComponent<PlayerController>().isPickingUp)
            //{
                _playerRef.GetComponent<Bag>().AddNewItemToTheBag(_pickUp.item);
                Destroy(transform.parent.gameObject);
            //}

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerRef = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerRef = null;
        }
    }
}
