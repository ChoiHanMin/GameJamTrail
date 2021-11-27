using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldControl
{
    void FieldMoveEnd(CField field);
}

public class CFieldManager : MonoBehaviour, IFieldControl
{
    [SerializeField] private CField[] firstField;

    private List<CField> fieldList = new List<CField>();

    private int m = 0;
    [SerializeField] private int riverNum = 2;

    public void FieldMoveEnd()
    {
        throw new System.NotImplementedException();
    }

    public void Plus()
    {
        m += 10;
        riverNum++;
        if (riverNum > 4)
        {
            riverNum = 0;
        }
    }

    public void FieldMoveEnd(CField field)
    {
        fieldList.Remove(field);
        field.SetField(this, m, fieldList[fieldList.Count - 1].NowZPos() + 10f, riverNum == 4);
        Plus();


        fieldList.Add(field);
    }

    private void Awake()
    {
        //Debug.Log(riverNum == 4);
        firstField[0].SetField(this, m, riverNum == 4);
        Plus();
        fieldList.Add(firstField[0]);

        //Debug.Log(riverNum == 4);
        firstField[1].SetField(this, m, riverNum == 4);
        Plus();
        fieldList.Add(firstField[1]);

        //Debug.Log(riverNum == 4);
        firstField[2].SetField(this, m, riverNum == 4);
        Plus();
        fieldList.Add(firstField[2]);
    }

}
