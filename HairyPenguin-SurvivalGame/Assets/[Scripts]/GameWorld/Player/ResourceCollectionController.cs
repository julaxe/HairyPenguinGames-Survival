using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceCollectionController : MonoBehaviour
{
    PlayerController playerController;
    public bool hasMiningPick;
    public bool hasAxe;
    public bool canCollect;
    public GameObject currentResourceToCollect;
    public GenerateItem pickUpResource;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isPickingUp && canCollect)
        {
            //Add timer
            //Connect to inventory
            //Inventory Full
            canCollect = false;
            pickUpResource = currentResourceToCollect.GetComponent<ResourceInfo>().collectingResource(hasMiningPick, hasAxe);
            Destroy(currentResourceToCollect);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResourceNode"))
        {
            canCollect = true;
            currentResourceToCollect = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ResourceNode"))
        {
            canCollect = false;
            currentResourceToCollect = null;
        }
    }
    public void OnPickUp(InputValue value)
    {
        playerController.isPickingUp = value.isPressed;
    }

}