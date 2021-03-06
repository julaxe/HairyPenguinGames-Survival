using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResourceInfo : MonoBehaviour
{
    public TypesOfResourceNodes resourceType;
    public List<GenerateItem> listofPickUps;
    public bool isWood;
    // Start is called before the first frame update
    void Start()
    {
        isWood = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GenerateItem collectingResource(bool hasPick, bool hasAxe)
    {
        GenerateItem temp = null;
        int choosingItem = Random.Range(1, 10);
        isWood = false;
        switch (resourceType)
        {
            case TypesOfResourceNodes.Bush:
                if (choosingItem < 5)
                {
                    temp = listofPickUps[0];
                }
                else
                {
                    temp = listofPickUps[1];
                    isWood = true;
                }
                break;
            case TypesOfResourceNodes.Rock:
                if (!hasPick)
                {
                    Debug.Log("Need Pick Tool");
                    return null;
                }
                temp = listofPickUps[0];
                break;
            case TypesOfResourceNodes.Tree:
                if (!hasAxe)
                {
                    Debug.Log("Need Axe Tool");
                    return null;
                }
                if (choosingItem < 5)
                {
                    temp = listofPickUps[0];
                }
                else
                {
                    temp = listofPickUps[1];
                    isWood = true;
                }
                break;
            default:
                break;
        }
        Destroy(this.gameObject);
        return temp;
    }
    public enum TypesOfResourceNodes
    {
        Bush,
        Rock,
        Tree
    }

    public enum TypesOfResource
    {
        None,
        Berry,
        Twig,
        Rock,
        Ore,
        Plank

    }
    public enum TypesOFCollectionTools
    {
        None,
        MiningPick,
        Axe
    }
}
