using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacter : MonoBehaviour
{
    [SerializeField] private Sprite sickSprite;
    [SerializeField] private Animator evadeAni;
    [SerializeField] private CObjectMove objectMove;
    public void SickStart()
    {
        objectMove.Remove();
        CGameManager.Instance.SickAniStart(sickSprite);
        CObjectPool.instance.PoolObject(gameObject);
        CSoundManager.Instance.PlaySFX(Random.Range(0, 2) == 0 ? SoundSFX.Hit1 : SoundSFX.Hit2);
    }

    public void Evade()
    {
        evadeAni.SetTrigger("Evade");
        CSoundManager.Instance.PlaySFX(SoundSFX.Miss);
        CGameManager.Instance.SuccessEffect(transform.position);
    }
}
