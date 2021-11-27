using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CField : MonoBehaviour, IMove
{
    private IFieldControl fieldControl;
    [SerializeField] private TMP_Text mText;
    [SerializeField] private GameObject riverOb;
    [SerializeField] private GameObject finishOb;
    [SerializeField] private GameObject signOb;
    [SerializeField] private GameObject textOb;

    public void SetField(IFieldControl fieldControl, int m, bool riverOn, bool finish)
    {
        this.fieldControl = fieldControl;
        signOb.SetActive(m % 100 == 0);
        textOb.SetActive(m % 100 == 0);
        mText.text = m +"M";
        CGameManager.Instance.AddMove(this);
        riverOb.SetActive(riverOn);
        finishOb.SetActive(finish);
    }

    public void SetField(IFieldControl fieldControl, int m, float startZPos, bool riverOn, bool finish)
    {
        SetField(fieldControl, m, riverOn, finish);
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
