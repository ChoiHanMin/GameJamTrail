using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrain : MonoBehaviour
{

    private Animator trainJumpAni;
    private void Awake()
    {
        trainJumpAni = GetComponent<Animator>();
    }

    public void Jump()
    {
        if (trainJumpAni.GetCurrentAnimatorStateInfo(0).IsName("TrainWait"))
        {
            trainJumpAni.SetTrigger("Jump");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
