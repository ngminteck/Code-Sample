using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberInteractEffect : MonoBehaviour
{
    [SerializeField] private AudioClip down_sound = null;
    [SerializeField] private GameObject PayInterface = null;
    [SerializeField] private int number = 0;
    [SerializeField] private GameObject down_sprite = null;
    [SerializeField] private Color down_color = Color.white;
    private bool bDownOnce = false;
    private bool bClickOnce = false;

    

    public void Awake()
    {
        GetComponent<AudioSource>().playOnAwake = false;
       
    }

    public void Init()
    {
        bDownOnce = false;
        bClickOnce = false;
        EnableInput(false);

        GetComponent<Image>().DOKill();
        GetComponent<Image>().DOColor(Color.white, 0.2f);

        if (down_sprite!= null)
        {
            down_sprite.GetComponent<CanvasGroup>().DOKill();
            down_sprite.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }

    public void Init2()
    {
        bDownOnce = false;
        bClickOnce = false;

        GetComponent<Image>().DOKill();
        GetComponent<Image>().DOColor(Color.white, 0.2f);

        if (down_sprite != null)
        {
            down_sprite.GetComponent<CanvasGroup>().DOKill();
            down_sprite.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }

    public int GetNumber()
    {
        return number;
    }

    public void EnableInput(bool value)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = value;
        GetComponent<Image>().raycastTarget = value;

        if (down_sprite != null)
        {
            down_sprite.GetComponent<CanvasGroup>().DOKill();
            down_sprite.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }

    public void Down()
    {
        if (bDownOnce)
            return;

        PayInterface.GetComponent<PayInterface>().DetectInput();
        PayInterface.GetComponent<PayInterface>().EnableInputExcept(false);

        bDownOnce = true;
        GetComponent<AudioSource>().PlayOneShot(down_sound);

        GetComponent<RectTransform>().DOKill();

        if (number == 12)
        {
            GetComponent<RectTransform>().DOScale(new Vector3(1.25f, 1.25f, 1.0f), 0.2f).OnComplete(() => Click());
        }
        else if(number == 13)
        {
            GetComponent<RectTransform>().DOScale(new Vector3(1.55f, 1.55f, 1.0f), 0.2f).OnComplete(() => Click());
        }
        else
        {
            GetComponent<RectTransform>().DOScale(new Vector3(1.05f, 1.05f, 1.0f), 0.2f).OnComplete(() => Click());
        }

        if (down_sprite != null)
        {
            down_sprite.GetComponent<CanvasGroup>().DOKill();
            down_sprite.GetComponent<CanvasGroup>().DOFade(1.0f, 0.2f);
            
        }

        if (number == 10)
        {
            GetComponent<Image>().DOKill();
            GetComponent<Image>().DOColor(down_color, 0.2f);
            PayInterface.GetComponent<PayInterface>().AddDot();
        }
        else if (number == 11)
        {
            GetComponent<Image>().DOKill();
            GetComponent<Image>().DOColor(down_color, 0.2f);
            PayInterface.GetComponent<PayInterface>().RemoveValue();
        }
        else if (number == 12)
        {
            return;
        }
        else if (number == 13)
        {
            return;
        }
        else
        {
            GetComponent<Image>().DOKill();
            GetComponent<Image>().DOColor(down_color, 0.2f);
            PayInterface.GetComponent<PayInterface>().AddNumber(number);
        }

    }

    public void Exit()
    {
       // if (!bDownOnce)
       //     return;
       //
       // PayInterface.GetComponent<PayInterface>().DetectInput();
       //
       // bDownOnce = false;
       // GetComponent<RectTransform>().DOKill();
       // GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
       //
       // if (down_sprite != null)
       // {
       //     down_sprite.GetComponent<CanvasGroup>().DOKill();
       //     down_sprite.GetComponent<CanvasGroup>().DOFade(0.0f, 0.2f);
       // }
       //
       // PayInterface.GetComponent<PayInterface>().EnableInput(true);
    }

    public void Click()
    {
        if (bClickOnce)
            return;

        PayInterface.GetComponent<PayInterface>().DetectInput();

        bClickOnce = true;
        GetComponent<RectTransform>().DOKill();


      
        if (number == 12)
        {
            GetComponent<RectTransform>().DOScale(new Vector3(1.2f,1.2f,1.0f), 0.2f).OnComplete(() => AfterDelayClick());
        }
        else if(number ==13)
        {
            GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1.0f), 0.2f).OnComplete(() => AfterDelayClick());
        }
        else
        {
            GetComponent<Image>().DOKill();
            GetComponent<Image>().DOColor(Color.white, 0.2f);
            GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f).OnComplete(() => AfterDelayClick());
        }

        if (down_sprite != null)
        {
            down_sprite.GetComponent<CanvasGroup>().DOKill();
            down_sprite.GetComponent<CanvasGroup>().DOFade(0.0f, 0.2f);
        }

       
    }
    public void AfterDelayClick()
    {
        GetComponent<AudioSource>().Play();
        Init2();

       
        PayInterface.GetComponent<PayInterface>().EnableInput(true);


        if (number == 10)
        {
            return;
        }
        else if (number == 11)
        {
            return;
        }
        else if (number == 12)
        {
            PayInterface.GetComponent<PayInterface>().Enter();
        }
        else if (number == 13)
        {
            PayInterface.GetComponent<PayInterface>().Enter2();
        }
        else
        {
            return;
        }
    }
}
