using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPlatform : MonoBehaviour
{
    [Header("Spawning Objects")]
    public GameObject platform;
    public GameObject tent;
    private GameObject gameobjectToBuild;


    public GameObject platformSpawnPosition;
    private bool isChoosingPlacement;
    private GameObject ghostPlatform;
    public GameObject platformsHolder;

    [Header("UI")]
    public GameObject upButton;
    public GameObject downButton;
    public GameObject swapItemButton;

    [Header("Materials")]
    public Material defualtMaterial;
    public Material ghostMaterial;
    public List<Material> materialsList;

    [Header("Last Tent")]
    public GameObject lastTent;
    public GameStateController gameStateController;

    public Bag _bag;




    // Start is called before the first frame update
    void Start()
    {
        isChoosingPlacement = false;
        _bag = GetComponent<Bag>();
        gameobjectToBuild = platform;
        gameStateController = GameObject.Find("GameController").GetComponent<GameStateController>();


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
            swapItemButton.SetActive(false);
            ghostPlatform.GetComponent<MeshRenderer>().material = defualtMaterial;
            ghostPlatform.GetComponent<BoxCollider>().enabled = true;
            if (ghostPlatform.CompareTag("CheckPoint"))
            {
                gameStateController.updateCurrentSpawnPoint(ghostPlatform.transform.GetChild(0).gameObject);
                Destroy(lastTent);
                lastTent = ghostPlatform;
            }

        }
    }

    private void placement()
    {

        ghostPlatform = Instantiate(gameobjectToBuild, transform);
        ghostPlatform.transform.position = platformSpawnPosition.transform.position;
        isChoosingPlacement = true;
        upButton.SetActive(true);
        downButton.SetActive(true);
        swapItemButton.SetActive(true);
        ghostPlatform.GetComponent<MeshRenderer>().material = ghostMaterial;
        ghostPlatform.GetComponent<BoxCollider>().enabled = false;


    }

    public void spawBuildItem()
    {
        if (gameobjectToBuild.CompareTag("CheckPoint"))
        {
            gameobjectToBuild = platform;
            defualtMaterial = materialsList[0];
            ghostMaterial = materialsList[1];
        }
        else
        {
            gameobjectToBuild = tent;
            defualtMaterial = materialsList[2];
            ghostMaterial = materialsList[3];
        }

        Destroy(ghostPlatform);

        placement();

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
