using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceCollectionController : MonoBehaviour
{
    PlayerController playerController;
    public static int enemiesKilled = 0;
    public bool hasMiningPick;
    public bool hasAxe;
    public bool canCollect;
    public GameObject currentResourceToCollect;
    public GenerateItem pickUpResource;

    private Bag _bag;

    public ItemSlotGridDimensioner grid;
    
    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        playerController = GetComponent<PlayerController>();
        _bag = GetComponent<Bag>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isPickingUp) return;
            
        if (!canCollect) return;
       
        //Add timer
        //Connect to inventory
        //Inventory Full
        canCollect = false;
        pickUpResource = currentResourceToCollect.GetComponent<ResourceInfo>().collectingResource(hasMiningPick, hasAxe); 
        _bag.AddNewItemToTheBag(pickUpResource);
        grid.UpdateBag();
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
}
