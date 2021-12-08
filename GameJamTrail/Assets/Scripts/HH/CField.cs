using TMPro;
using UnityEngine;

public class CField : MonoBehaviour, IMove
{
    private IFieldControl fieldControl;
    [SerializeField] private TMP_Text mText;
    [SerializeField] private GameObject finishOb;
    [SerializeField] private GameObject signOb;
    [SerializeField] private GameObject textOb;

    [SerializeField] private GameObject[] presets;

    private GameObject preset;

    public void SetField(IFieldControl fieldControl, int m, bool riverOn, bool finish)
    {
        this.fieldControl = fieldControl;
        signOb.SetActive(m % 100 == 0);
        textOb.SetActive(m % 100 == 0);
        mText.text = m +"M";
        CGameManager.Instance.AddMove(this);
        //finishOb.SetActive(finish);

        preset = CObjectPool.instance.GetObject(presets[Random.Range(0, presets.Length)]);
        preset.transform.SetParent(transform);
        preset.transform.localPosition = Vector3.zero;

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
            if (preset != null)
            {
                CObjectPool.instance.PoolObject(preset);
                preset = null;
            }
        }

    }

    public float NowZPos()
    {
        return transform.position.z;
    }

}
