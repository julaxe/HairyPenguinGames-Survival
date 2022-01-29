using UnityEngine;
[CreateAssetMenu(fileName = "SnowEffect", menuName = "ScriptableObjects/Effect/SnowEffect", order = 1)]
public class SnowEffect : Effect
{

    private GameObject _snowGameObject;
    public override void ActivateEffect()
    {
        base.ActivateEffect();

        _snowGameObject = GameObject.Find("Snow");

        _snowGameObject.GetComponent<ParticleSystem>().Play();
    }

    public override void ExitEffect()
    {
        base.ExitEffect();
        _snowGameObject.GetComponent<ParticleSystem>().Stop();
    }
}
