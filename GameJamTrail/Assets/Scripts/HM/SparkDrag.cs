using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using com.ootii.Messages;


public class SparkDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    float distance = 10.0f;

    bool bIsPointerIn;
    private void Start()
    {
        MessageDispatcher.AddListener("ImageIn", ImageIn);
        bIsPointerIn = false;
    }
    //#if UNITY_ANDROID && UNITY_IOS
    private void Update()
    {
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                MessageDispatcher.SendMessage("SpawnNumMinus");
                MessageDispatcher.SendMessage("AddSpark");

                Destroy(this.gameObject);
            }
        }*/

    }

    private void OnDestroy()
    {
        MessageDispatcher.RemoveListener("ImageIn", ImageIn);
    }
    //#endif


    public void OnDrag(PointerEventData eventData)
    {
#if UNITY_STANDALONE_WIN
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        transform.position = mousePosition;
        Debug.Log("aaaaaaaaaaaa");
#endif
    }

    public void OnEndDrag(PointerEventData eventData)
    {
#if UNITY_STANDALONE_WIN
        Debug.Log("cccccccccccc");
        if(bIsPointerIn)
        {
            MessageDispatcher.SendMessage("SpawnNumMinus");
            MessageDispatcher.SendMessage("AddSpark");
            
            Destroy(this.gameObject);
        }
#endif
    }

    void ImageIn(IMessage rMessage)
    {
#if UNITY_STANDALONE_WIN
        bIsPointerIn = true;
#endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MessageDispatcher.SendMessage("SpawnNumMinus");
        MessageDispatcher.SendMessage("AddSpark");

        Destroy(this.gameObject);
    }
}
