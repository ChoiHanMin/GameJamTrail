using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectMove : MonoBehaviour, IMove
{
    public void Move(float movePos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - movePos);

        if (transform.position.z < -10F)
        {
            CGameManager.Instance.RemoveMove(this);
            CObjectPool.instance.PoolObject(gameObject);
        }
    }

    public void Remove()
    {
        CGameManager.Instance.RemoveMove(this);
    }

    public void MoveStart(Vector3 setPos)
    {
        transform.position = setPos;


        CGameManager.Instance.AddMove(this);
    }

}
