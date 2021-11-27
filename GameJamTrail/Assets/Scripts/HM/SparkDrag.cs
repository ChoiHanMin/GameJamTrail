using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using com.ootii.Messages;


public class SparkDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    float distance = 10.0f;

    bool bIsPointerIn;
    private void Start()
    {
        MessageDispatcher.AddListener("ImageIn", ImageIn);
        bIsPointerIn = false;
    }

    private void OnDestroy()
    {
        MessageDispatcher.RemoveListener("ImageIn", ImageIn);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        transform.position = mousePosition;
        Debug.Log("aaaaaaaaaaaa");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("cccccccccccc");
        if(bIsPointerIn)
        {
            MessageDispatcher.SendMessage("SpawnNumMinus");
            MessageDispatcher.SendMessage("AddSpark");
            
            Destroy(this.gameObject);
        }
    }

    void ImageIn(IMessage rMessage)
    {
        bIsPointerIn = true;
    }
    
}
