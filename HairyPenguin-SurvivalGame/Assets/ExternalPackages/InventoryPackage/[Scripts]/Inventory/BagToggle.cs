using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagToggle : MonoBehaviour
{
    private IEnumerator coroutine;
    private ItemSlotGridDimensioner grid;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        coroutine = WaitAndLoad(0.01f); // wait a little bit so we can get the grid loaded
        grid = gameObject.GetComponentInParent<ItemSlotGridDimensioner>();
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitAndLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        grid.LoadBag();
    }
    private void OnDisable()
    {
        //Disappear Items from current Bag (grid).
        grid.UnLoadBag();
    }
}
