using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CField : MonoBehaviour, IMove
{
    private IFieldControl fieldControl;
    [SerializeField] private TMP_Text mText;

    public void SetField(IFieldControl fieldControl, int m)
    {
        this.fieldControl = fieldControl;
        mText.text = m +"M";
        CGameManager.Instance.AddMove(this);
    }

    public void SetField(IFieldControl fieldControl, int m, float startZPos)
    {
        SetField(fieldControl, m);
        transform.position = new Vector3(transform.position.x, transform.position.y, startZPos);
    }

    public void Move(float movePos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - movePos);

        if (transform.position.z < -10F)
        {
            CGameManager.Instance.RemoveMove(this);
            fieldControl.FieldMoveEnd(this);
        }

    }

    public float NowZPos()
    {
        return transform.position.z;
    }

}
