using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlameThrowerController : MonoBehaviour
{
    ParticleSystem flames;
    public bool flamesTimerOn;
    BoxCollider hitBox;
    float _timer;
    public float cdTick = 0.5f; //how many seconds between damage-tick


    // Start is called before the first frame update
    void Start()
    {
        flames = GetComponentInChildren<ParticleSystem>();
        hitBox = GetComponent<BoxCollider>(); 
        if (flamesTimerOn)
        {
            StartCoroutine(TimerOn());
        }



    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TimerOn()
    {
        flames.Play();
        hitBox.enabled = true;
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(TimerOff());
    }
    IEnumerator TimerOff()
    {
        hitBox.enabled = false;
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(TimerOn());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (_timer == 0.0f)
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<Animation>().Blend("CharacterArmature|Punch", 3.0f);
            other.GetComponent<PlayerHealthController>().setHealth(-10);
        }
        _timer += Time.fixedDeltaTime;
        if (_timer >= cdTick) _timer = 0.0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _timer = 0.0f;
    }

}
