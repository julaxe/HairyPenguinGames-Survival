using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeSpawner : MonoBehaviour
{
    public GameObject ResourceType;

    public int amountOfResources;
    private BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        for (int i = 0; i < amountOfResources; i++)
        {
            var temp = Instantiate(ResourceType, transform);
            temp.transform.position = new Vector3(Random.Range(box.bounds.min.x, box.bounds.max.x), 36.31f, Random.Range(box.bounds.min.z, box.bounds.max.z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
