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

    private bool firstTrain = false;


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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDamage) return;
        if (other.tag == "Huddle" && waitHuddleDamage && firstTrain)
        {
            CGameManager.Instance.TrainDamage();
        }

    }

}
