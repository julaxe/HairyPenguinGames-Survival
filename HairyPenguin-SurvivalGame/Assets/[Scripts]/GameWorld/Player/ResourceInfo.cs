using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResourceInfo : MonoBehaviour
{
    public TypesOfResourceNodes resourceType;
    public List<GenerateItem> listofPickUps;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GenerateItem collectingResource(bool hasPick, bool hasAxe)
    {
        GenerateItem temp = null;
        int choosingItem = Random.Range(1, 10);

        switch (resourceType)
        {
            case TypesOfResourceNodes.Bush:
                if (choosingItem < 5)
                {
                    temp = listofPickUps[0];
                }
                else
                {
                    temp = listofPickUps[0];
                }
                break;
            case TypesOfResourceNodes.Rock:
                if (!hasPick)
                {
                    Debug.Log("Need Pick Tool");
                    return listofPickUps[0];
                }
                temp = listofPickUps[0];
                break;
            case TypesOfResourceNodes.Tree:
                if (!hasAxe)
                {
                    Debug.Log("Need Axe Tool");
                    return listofPickUps[0];
                }
                temp = listofPickUps[0];
                break;
            default:
                break;
        }
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
