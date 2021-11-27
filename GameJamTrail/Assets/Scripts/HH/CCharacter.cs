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
    }

    public void Evade()
    {
        evadeAni.SetTrigger("Evade");
    }
}
