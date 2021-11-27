using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrain : MonoBehaviour
{

    private Animator trainJumpAni;
    private float zPos = 0f;
    private int num = 0;
    private void Awake()
    {
        trainJumpAni = GetComponent<Animator>();
        zPos = transform.position.z;
    }

    public void FirstMove(float moveZPos, int num)
    {
        this.num = num;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos + moveZPos);
    }

    public void Jump()
    {
        if (trainJumpAni.GetCurrentAnimatorStateInfo(0).IsName("TrainWait"))
        {
            trainJumpAni.SetTrigger("Jump");
        }
    }
    public bool IsJump()
    {
        return !trainJumpAni.GetCurrentAnimatorStateInfo(0).IsName("TrainWait");
    }

    public void NextJump()
    {
        CGameManager.Instance.NextJump(num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
