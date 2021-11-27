using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TrainSpeedAnchor : MonoBehaviour
{
    [SerializeField]
    private GameObject Anchor;

    private DOTweenAnimation AnchorAnim;
    private Quaternion MyRotate;
    private RectTransform rt;
    private int Degree;


    private void Awake()
    {
        AnchorAnim = Anchor.GetComponent<DOTweenAnimation>();
        // MyRotate = this.gameObject.transform.rotation;

        rt = Anchor.GetComponent<RectTransform>();
        Degree = 20;
    }

    private void Start()
    {
        //  AnchorAnim.DOPlay();
        MyRotate = rt.transform.rotation;
        StartCoroutine(RotateAnc());

    }

    private void OnDestroy()
    {
        StopCoroutine(RotateAnc());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            TouchLeft();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TouchRight();
        }

    }

    private IEnumerator RotateAnc()
    {
        while (true)
        {
            if (Degree <= 0 && Degree >= -90)
            //if (rt.rotation.z < 0 && rt.rotation.z >= -90)
            {
                rt.DOLocalRotate(MyRotate.eulerAngles + new Vector3(0, 0, -5), 0.1f);
                Degree += 5;
            }
            else if (Degree > 0 && Degree <= 90)
            //else if (rt.rotation.z >= 0 && rt.rotation.z <= 90)
            {
                rt.DOLocalRotate(MyRotate.eulerAngles + new Vector3(0, 0, 5), 0.1f);
                Degree -= 5;
            }


            if (Degree >= 90)
            { 
                Degree = 90;
            }

            else if (Degree <= -90)
            {
                Degree = -90;
            }

            yield return new WaitForSeconds(0.5f);

            //MyRotate = this.gameObject.transform.rotation;
            MyRotate = rt.transform.rotation;
            //MyRotate = rt.transform.rotation;
            Debug.Log("MyRotate : " + Degree);
        }
    }

    public void TouchLeft()
    {
        //Anchor.transform.DORotate(MyRotate.eulerAngles + new Vector3(0, 0, 5), 0.1f);
        if (Degree >= 0 && Degree <= 90)
        {
            rt.DOLocalRotate(MyRotate.eulerAngles + new Vector3(0, 0, 5), 0.1f);
            Degree += 5;
        }

        if(Degree >=90)
        {
            Degree = 90;
        }
        Debug.Log("Deleft : " + Degree);

        //  MyRotate = this.gameObject.transform.rotation;
        MyRotate = rt.transform.rotation;
    }

    public void TouchRight()
    {
        //Anchor.transform.DORotate(MyRotate.eulerAngles + new Vector3(0, 0, -5), 0.1f);
        if (Degree < 0 && Degree >= -90)
        {
            rt.DOLocalRotate(MyRotate.eulerAngles + new Vector3(0, 0, -5), 0.1f);
            Degree -= 5;
        }

        if(Degree<=-90)
        {
            Degree = -90;
        }
        Debug.Log("Deright : " + Degree);

        // MyRotate = this.gameObject.transform.rotation;
        MyRotate = rt.transform.rotation;
    }
}
