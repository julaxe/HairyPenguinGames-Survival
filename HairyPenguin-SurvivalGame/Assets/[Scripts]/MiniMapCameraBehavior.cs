using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject followObject;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followObject.transform.position.x,
                                         transform.position.y,
                                          followObject.transform.position.z) ;
    }
}
