using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPlatform : MonoBehaviour
{
    public GameObject platform;
    public GameObject platformSpawnPosition;
    private bool isChoosingPlacement;
    private GameObject ghostPlatform;
    public GameObject platformsHolder;
    public GameObject upButton;
    public GameObject downButton;
    public Material defualtMaterial;
    public Material ghostMaterial;
    public Bag _bag;

    // Start is called before the first frame update
    void Start()
    {
        isChoosingPlacement = false;
        _bag = GetComponent<Bag>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnPlatformPressed()
    {
        if (!isChoosingPlacement)
        {
            foreach (var item in _bag.listOfItems)
            {
                if (item.gameObject.tag == "Wood")
                {
                    //_bag.DeleteFromlist(item);
                    item.GetComponent<Item>().DeleteItem();
                    placement();
                    break;
                }
            }
        }
        else
        {
            ghostPlatform.transform.SetParent(platformsHolder.transform);
            isChoosingPlacement = false;
            upButton.SetActive(false);
            downButton.SetActive(false);
            ghostPlatform.GetComponent<MeshRenderer>().material = defualtMaterial;
            ghostPlatform.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void placement()
    {

        ghostPlatform = Instantiate(platform, transform);
        ghostPlatform.transform.position = platformSpawnPosition.transform.position;
        isChoosingPlacement = true;
        upButton.SetActive(true);
        downButton.SetActive(true);
        ghostPlatform.GetComponent<MeshRenderer>().material = ghostMaterial;
        ghostPlatform.GetComponent<BoxCollider>().enabled = false;


    }
    public void upButtonPressed()
    {
        ghostPlatform.transform.position += Vector3.up * 0.5f;
    }
    public void downButtonPressed()
    {
        ghostPlatform.transform.position += Vector3.up * -0.5f;
    }
}
