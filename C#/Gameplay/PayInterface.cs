using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PayInterface : MonoBehaviour
{
    [SerializeField] GameObject TextInput = null;
    [SerializeField] GameObject SGQRPhone = null;
    [SerializeField] GameObject PayNowPhone = null;
   

    private string DisplayText = "";


    private int Stroke_pos = 0;

    public void Init()
    {

        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).GetComponent<NumberInteractEffect>().Init();
        }

        DisplayText = "";
        TextInput.GetComponent<TextMeshProUGUI>().text = DisplayText;
        TextInput.GetComponent<TextMeshProUGUI>().color = Color.white;
        Stroke_pos = 0;
    }


    public void EnableInput(bool value)
    {
        for(int i = 0; i< transform.childCount; ++ i)
        {
            transform.GetChild(i).GetComponent<NumberInteractEffect>().EnableInput(value);
        }
    }

    public void EnableInputExcept(bool value)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {

          

            transform.GetChild(i).GetComponent<NumberInteractEffect>().EnableInput(value);
        }
    }


    public void ChangeToRed()
    {
        if (SGQRPhone != null)
        {
            MPPage.Instance.ChangePromptColor(2, 1, 0);
           
        }
   
        if(PayNowPhone != null)
        {
            PNPage.Instance.ChangePromptColor(2, 1, 0);
            PNPage.Instance.ChangePromptColor(3, 1, 0);
            PNPage.Instance.ChangePromptColor(4, 1, 0);
        }

        Stroke_pos = 0;
        DisplayText = "";
        TextInput.GetComponent<TextMeshProUGUI>().text = DisplayText;

    }

    public void ChangeToWhite()
    {
        if (SGQRPhone != null)
        {
            MPPage.Instance.ChangePromptColor(2, 0, 1);
          
        }

        if (PayNowPhone != null)
        {
            PNPage.Instance.ChangePromptColor(2, 0, 1);
            PNPage.Instance.ChangePromptColor(3, 0, 1);
            PNPage.Instance.ChangePromptColor(4, 0, 1);
        }
        TextInput.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void AddNumber(int new_number)
    {
       if(DisplayText.Length ==  Stroke_pos)
       {
           DisplayText += new_number.ToString();
           
       }
       else
       {
           DisplayText.Insert(Stroke_pos, new_number.ToString());
       }
        ++Stroke_pos;
        UpdateDisplay();
    }

    public void AddDot()
    {
        if(DisplayText.IndexOf('.') == -1)
        {
            DisplayText += ".";
            ++Stroke_pos;
            
            UpdateDisplay();
        }
    }

    public void RemoveValue()
    {
        if (DisplayText.Length == 0 )
            return;


        DisplayText = DisplayText.Remove(Stroke_pos - 1, 1);
        --Stroke_pos;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        ChangeToWhite();
        TextInput.GetComponent<TextMeshProUGUI>().text = DisplayText;
    }

    public void Enter()
    {
        UpdateFinalDisplayNumber();
        SGQRPhone.GetComponent<SGQRPhone>().VerifyValue(TextInput.GetComponent<TextMeshProUGUI>().text);
    }

    public void Enter2()
    {
        UpdateFinalDisplayNumber();
        PayNowPhone.GetComponent<PNPhone>().VerifyValue(TextInput.GetComponent<TextMeshProUGUI>().text);
    }

    private void UpdateFinalDisplayNumber()
    {
        if(DisplayText.Length == 0)
        {
            DisplayText = "0";
        }

        if (DisplayText.IndexOf('.') == 0)
        {
            DisplayText = "0" + DisplayText;
        }

        decimal DecimalNumber = decimal.Parse(DisplayText);
        DisplayText = DecimalNumber.ToString("0.00#######");
        Stroke_pos = DisplayText.Length;
        TextInput.GetComponent<TextMeshProUGUI>().text = DisplayText;

    }

    public void DetectInput()
    {
        if(SGQRPhone!=null)
        {
            MPPage.Instance.DetectInput();
        }
        if(PayNowPhone != null)
        {
            PNPage.Instance.DetectInput();
        }
    }
}
