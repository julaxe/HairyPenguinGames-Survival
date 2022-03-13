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

    // Start is called before the first frame update
    void Start()
    {
        isChoosingPlacement = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnPlatformPressed()
    {
        if (!isChoosingPlacement)
        {
            ghostPlatform = Instantiate(platform, transform);
            ghostPlatform.transform.position = platformSpawnPosition.transform.position;
            isChoosingPlacement = true;
            upButton.SetActive(true);
            downButton.SetActive(true);
            ghostPlatform.GetComponent<MeshRenderer>().material = ghostMaterial;
            ghostPlatform.GetComponent<BoxCollider>().enabled = false;
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
    public void upButtonPressed()
    {
        ghostPlatform.transform.position += Vector3.up * 0.5f;
    }
    public void downButtonPressed()
    {
        ghostPlatform.transform.position += Vector3.up * -0.5f;
    }
}
