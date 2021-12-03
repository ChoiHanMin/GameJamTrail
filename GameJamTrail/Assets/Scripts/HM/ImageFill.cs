using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;

public class ImageFill : MonoBehaviour
{
    //1 : Normal, 2 : Good, 3 : Bad
    public Sprite[] FillSprites;
    public Image FillImage;
    float SparkFill;

    // Start is called before the first frame update
    void Start()
    {
        SparkFill = 80;

        SetFillAmountImage();

        StartCoroutine(SetFill());

        MessageDispatcher.AddListener("AddSpark", AddSpark);
    }
    private void OnDestroy()
    {
        MessageDispatcher.RemoveListener("AddSpark", AddSpark);
    }

    public IEnumerator SetFill()
    {
        yield return new WaitForSeconds(10.0f);
        
        while(true)
        {
            if(SparkFill!=0)
            {
                SparkFill -= 5;
                SetFillAmountImage();

            }

            yield return new WaitForSeconds(1.0f);
        }
        
    }

    void SetFillAmountImage()
    {
        FillImage.fillAmount = SparkFill / 100;

        if (FillImage.fillAmount >= 0.9)
        {
            FillImage.sprite = FillSprites[1];
            CGameManager.Instance.kms = 15;
        }
        else if (FillImage.fillAmount > 0.4 && FillImage.fillAmount < 0.9)
        {
            FillImage.sprite = FillSprites[0];
            CGameManager.Instance.kms = 10;
        }
        else if (FillImage.fillAmount <= 0.4)
        {
            FillImage.sprite = FillSprites[2];
            CGameManager.Instance.kms = 5;
        }
        CGameManager.Instance.KmToString();
    }

    void AddSpark(IMessage rMessage)
    {
        if(SparkFill<=100)
        {
            SparkFill += 10;
            SetFillAmountImage();
        }
        
    }
}
