using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrainSpeedAnchor : MonoBehaviour
{
    [SerializeField] private RectTransform rt;
    //[SerializeField] private Vector3 nowAngle;
    //[SerializeField] private Vector3 startAngle;
    [SerializeField] private Vector3 endAngle;

    //[SerializeField] private AnimationCurve ac;
    private float time = 0;
    private bool speedChange = false;



    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        //nowAngle = new Vector3(0f, 0f, 90f);
    }

    private void Update()
    {
        //if (speedChange)
        //{
        //    time += Time.deltaTime;
        //    nowAngle = Vector3.Lerp(startAngle, endAngle, ac.Evaluate(time));
        //    if (time >= 1f)
        //    {
        //        time = 0;
        //        speedChange = false;
        //        nowAngle = endAngle;
        //    }
        //    rt.localRotation = Quaternion.Euler(nowAngle);
        //}
    }

    public void SetAng(float nowSpeed, float maxSpeed)
    {
        float r = nowSpeed / maxSpeed * 180f;
        r -= 90f;
        r *= -1f;
        endAngle = new Vector3(0f, 0f, r);
        rt.localRotation = Quaternion.Euler(endAngle);
        //if (nowAngle.z != r)
        //{
        //    //startAngle = nowAngle;
        //    endAngle = new Vector3(0f, 0f, r);
        //    rt.localRotation = Quaternion.Euler(endAngle);
        //    //speedChange = true;
        //    time = 0f;
        //}
    }


}
