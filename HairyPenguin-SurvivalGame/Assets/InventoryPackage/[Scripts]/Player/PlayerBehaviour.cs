using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    private GameObject containerCanvas;

    // Update is called once per frame
    private void Start()
    {
        containerCanvas = GameObject.Find("Canvas/ContainerCanvas/ContainerUI/InventoryBackground");
    }
    void Update()
    {
        // float inputX = Input.GetAxisRaw("Horizontal");
        // float inputY = Input.GetAxisRaw("Vertical");
        // transform.position += new Vector3(inputX * moveSpeed * Time.deltaTime, inputY * moveSpeed * Time.deltaTime, 0);
    }
    public void OpenContainer()
    {
        containerCanvas.SetActive(true);
    }

    public void CloseContainer()
    {
        containerCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.tag == "Container")
        {
            //set the current bag of the container first
            Bag currentBag = collision.transform.GetComponentInParent<Bag>();
            containerCanvas.GetComponentInParent<ItemSlotGridDimensioner>().SetCurrentBag(currentBag);
            OpenContainer();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //  if (collision.gameObject.tag == "Container")
        {

            CloseContainer();
        }
    }
}
