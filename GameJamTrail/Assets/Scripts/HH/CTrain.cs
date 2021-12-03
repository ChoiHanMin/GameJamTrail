using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrain : MonoBehaviour
{

    private Animator trainJumpAni;
    private float zPos = 0f;
    private int num = 0;

    [SerializeField] private Transform tr;

    private bool action = false;

    private float actionSpeed = 1f;
    private float time = 0f;

    private float ani1 = 0f;

    private bool waitHuddleDamage = true;

    [SerializeField] private bool firstTrain = false;

    [SerializeField] private CCharacter evadeCharacter;

    [SerializeField] private Animation smoke;
    [SerializeField] private Animation chain;
    [SerializeField] private Animation ppuppu;
    private void Awake()
    {
        trainJumpAni = GetComponent<Animator>();
        zPos = transform.position.z;
    }

    public void FirstMove(float moveZPos, int num)
    {
        this.num = num;
        firstTrain = num == 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos + moveZPos);
    }

    public void FinishToMove(float movePos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movePos);
    }

    public void Jump()
    {
        if (trainJumpAni.GetCurrentAnimatorStateInfo(0).IsName("TrainWait"))
        {
            trainJumpAni.SetTrigger("Jump");
            waitHuddleDamage = false;
        }
    }

    public void JumpEnd()
    {
        waitHuddleDamage = true;
    }
    public bool IsJump()
    {
        return !trainJumpAni.GetCurrentAnimatorStateInfo(0).IsName("TrainWait");
    }

    public void AniStart()
    {
        action = true;
        ani1 = 0f;

        if (smoke != null)
        {
            smoke.Play();
        }
        if (chain != null)
        {
            chain.Play();
        }
    }
    public void SpeedChange(float speed)
    {
        if(speed > 10)
        {
            actionSpeed = 0.2f;
        }
        else if (speed < 10)
        {
            actionSpeed = 0.5f;
        }
        else
        {
            actionSpeed = 0.35f;
        }
    }

    public void NextJump()
    {
        CGameManager.Instance.NextJump(num);
    }

    public void ChainSpeed(float speed)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (action)
        {
            time += Time.deltaTime;
            if (time > actionSpeed)
            {
                time = 0f;
                tr.localRotation = Quaternion.Euler(new Vector3(0f, ani1, 0f));
                ani1 = ani1 == 0 ? 180f : 0f;
            }
        }
    }
    [SerializeField] private bool isDamage = false;
    public void Damage(bool damage)
    {
        isDamage = damage;
        evadeCharacter = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish" && firstTrain)
        {
            CGameManager.Instance.Finish();
        }

        if (isDamage) return;

        if (other.tag == "Huddle" && waitHuddleDamage && firstTrain)
        {
            CSoundManager.Instance.PlaySFX(SoundSFX.Huddle_Crush);
            CGameManager.Instance.TrainDamage();
        }
        if (other.tag == "Character" && firstTrain)
        {
            CGameManager.Instance.TrainDamage();
            CCharacter character = other.GetComponent<CCharacter>();
            character.SickStart();
        }
        if (other.tag == "Evade" && firstTrain)
        {
            evadeCharacter = other.GetComponentInParent<CCharacter>();
        }
        

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag != "Evade" && firstTrain && evadeCharacter != null)
    //    {
    //        evadeCharacter = null;
    //    }
    //}

    public void DangerShout()
    {
        if(evadeCharacter != null)// && !isDamage)
        {
            evadeCharacter.Evade();
            if (ppuppu != null)
            {
                ppuppu.Play();
            }
            evadeCharacter = null;
        }
    }

}
