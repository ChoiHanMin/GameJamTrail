using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrain : MonoBehaviour
{

    private Animator trainJumpAni;
    private float zPos = 0f;
    private void Awake()
    {
        trainJumpAni = GetComponent<Animator>();
        zPos = transform.position.z;
    }

    public void FirstMove(float moveZPos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos + moveZPos);
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
