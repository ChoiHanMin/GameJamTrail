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
    [SerializeField] private GameObject[] impediments;

    private List<CField> fieldList = new List<CField>();

    private int m = 0;
    [SerializeField] private int riverNum = 2;

    [SerializeField] private Transform spawn;

    private float firstImpedimentM = 30f;

    private float[] firstImpedimentRan = {5f, 5f, 2.5f};

    private float[] firstImpedimentPos = {30f, 20f, 12.5f};

    private bool gameend = false;

    private void Update()
    {
        Debug.Log(firstImpedimentM + " " + CGameManager.Instance.MoveM());
            
        if (firstImpedimentM < CGameManager.Instance.MoveM())
        {
            int rnd = Random.Range(0, 5);
            GameObject ob = CObjectPool.instance.GetObject(impediments[rnd]);
            ob.transform.SetParent(transform);
            CObjectMove om = ob.GetComponent<CObjectMove>();
            om.MoveStart(spawn.position);

            if (CGameManager.Instance.MoveM() < 300f && !gameend)
            {
                firstImpedimentM += Random.Range(firstImpedimentPos[0] - firstImpedimentRan[0], firstImpedimentPos[0] + firstImpedimentRan[0]);
            }
            else if (CGameManager.Instance.MoveM() < 600f && !gameend)
            {
                firstImpedimentM += Random.Range(firstImpedimentPos[1] - firstImpedimentRan[1], firstImpedimentPos[1] + firstImpedimentRan[1]);
            }
            else if (CGameManager.Instance.MoveM() < 960f && !gameend)
            {
                firstImpedimentM += Random.Range(firstImpedimentPos[2] - firstImpedimentRan[0], firstImpedimentPos[2] + firstImpedimentRan[2]);
            }
            else 
            {
                Debug.Log(firstImpedimentM);
                firstImpedimentM += 1000f;
            }
            Debug.Log("»ý¼º !! ");
        }
    }


    public void FieldMoveEnd()
    {

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

        bool finish = false;

        if (!gameend && m >= 1000)
        {
            gameend = true;
            finish = true;
        }
        field.SetField(this, m, fieldList[fieldList.Count - 1].NowZPos() + 10f, riverNum == 4, finish);
        Plus();


        fieldList.Add(field);
    }

    private void Awake()
    {
        //Debug.Log(riverNum == 4);
        firstField[0].SetField(this, m, riverNum == 4, false);
        Plus();
        fieldList.Add(firstField[0]);

        //Debug.Log(riverNum == 4);
        firstField[1].SetField(this, m, riverNum == 4, false);
        Plus();
        fieldList.Add(firstField[1]);

        //Debug.Log(riverNum == 4);
        firstField[2].SetField(this, m, riverNum == 4, false);
        Plus();
        fieldList.Add(firstField[2]);
    }

    
}
