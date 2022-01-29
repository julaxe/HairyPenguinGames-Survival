using UnityEngine;
[CreateAssetMenu(fileName = "ChanceOfRainEffect", menuName = "ScriptableObjects/Effect/ChanceOfRainEffect", order = 1)]
public class ChanceOfRainEffect : Effect
{
    private GameObject _rainGameObject;

    public override void ActivateEffect()
    {
        base.ActivateEffect();
        _rainGameObject = GameObject.Find("Rain");

        if (Random.Range(0, 2) == 0)
        {
            _rainGameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    public override void ExitEffect()
    {
        base.ExitEffect();
        _rainGameObject.GetComponent<ParticleSystem>().Stop();
    }
}
