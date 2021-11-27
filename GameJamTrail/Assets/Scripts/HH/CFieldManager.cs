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

    public void FieldMoveEnd()
    {
        throw new System.NotImplementedException();
    }

    public void FieldMoveEnd(CField field)
    {
        fieldList.Remove(field);
        field.SetField(this, m, fieldList[fieldList.Count - 1].NowZPos() + 10f);
        m += 5;

        fieldList.Add(field);
    }

    private void Awake()
    {
        firstField[0].SetField(this, m);
        m += 5;
        fieldList.Add(firstField[0]);
        firstField[1].SetField(this, m);
        m += 5;
        fieldList.Add(firstField[1]);
        firstField[2].SetField(this, m);
        m += 5;

        fieldList.Add(firstField[2]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
