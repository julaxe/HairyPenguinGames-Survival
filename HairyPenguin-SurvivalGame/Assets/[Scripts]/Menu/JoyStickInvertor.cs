using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickInvertor : MonoBehaviour
{
    public SaveOptions saveOptions;
    public GameObject ButtonA;
    public GameObject ButtonB;
    public GameObject JoyStick;

    // Start is called before the first frame update
    void Start()
    {
        if (saveOptions.getInverted())
        {
            Invert();
        }
        if (!saveOptions.getInverted())
        {
            Restart();
        }
    }
    public void Invert()
    {
        ButtonA.transform.Translate(-2711,0,0);
        ButtonB.transform.Translate(-2611, 0, 0);
        JoyStick.transform.Translate(2711, 0, 0);
    }
    public void Restart()
    {
        ButtonA.transform.Translate(-350, 0, 0);
        ButtonB.transform.Translate(-197, 0, 0);
        JoyStick.transform.Translate(-66, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
