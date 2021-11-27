using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacter : MonoBehaviour
{
    [SerializeField] private Sprite sickSprite;
    [SerializeField] private Animator evadeAni;
    public void SickStart()
    {
        CGameManager.Instance.SickAniStart(sickSprite);
    }

    public void Evade()
    {
        evadeAni.SetTrigger("Evade");
    }
}
