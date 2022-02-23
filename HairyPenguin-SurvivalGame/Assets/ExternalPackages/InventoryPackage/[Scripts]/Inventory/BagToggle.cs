using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BagToggle : MonoBehaviour
{
    private IEnumerator coroutine;
    private ItemSlotGridDimensioner grid;


    private void Start()
    {
    }

    private void OnEnable()
    {
        coroutine = WaitAndLoad(); // wait a little bit so we can get the grid loaded
        grid = gameObject.GetComponentInParent<ItemSlotGridDimensioner>();
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitAndLoad()
    {
        grid.LoadBag();
        yield return new WaitForSeconds(0.1f);
        grid.UpdateBag();
    }
    private void OnDisable()
    {
        //Disappear Items from current Bag (grid).
        grid.UnLoadBag();
    }
}
