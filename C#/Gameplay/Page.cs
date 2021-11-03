using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public abstract class Page : MonoBehaviour
{
    
    [SerializeField] public float idleResetTime = 60.0f;
    public float elapsedTime = 0.0f;
    [SerializeField] protected CanvasGroup canvastable = null;
    [SerializeField] protected CanvasGroup canvaswall = null;
    [SerializeField] Camera cam = null;
    private EventTrigger[] buttons;

 
 

    public bool bSplashScreen = false;
    public bool idleCountdown = true;
  

    void Awake()
    {
        buttons = transform.GetComponentsInChildren<EventTrigger>(true);
     
    }

    public virtual void Update()
    {
        if (!bSplashScreen && idleCountdown)
        {
           
           elapsedTime += Time.deltaTime;



            if (elapsedTime >= idleResetTime)
            {
                DetectInput();
                bSplashScreen = true;
                idleCountdown = false;
                RestartPageSession();
            }
        }
    }

    public virtual void DetectInput()
    {
        elapsedTime = 0.0f;
    }

   
    public virtual void RestartPageSession()
    {

    }
    

    public abstract void Initialize();

    // Do not set duration to 0
    protected virtual void FadeIn(float duration)
    {

     

        canvaswall.DOFade(1.0f, duration).OnStart(() => canvaswall.gameObject.SetActive(true));

        canvastable.DOFade(1.0f, duration).OnStart (() => gameObject.SetActive(true)).OnComplete(() => EnableButtons() );

   
    }

   

    // Do not set duration to 0
    protected virtual void FadeOut(float duration, float delay)
    {


        canvaswall.DOFade(0.0f, 1.0f).SetDelay(delay);

        canvastable.DOFade(0.0f, 1.0f).SetDelay(delay);

      
    }

    protected virtual void FadeInImmediately()
    {
        gameObject.SetActive(true);
        canvastable.DOFade(1.0f, 0.0f);
        canvaswall.DOFade(1.0f, 0.0f);
        EnableButtons();
    }

    protected virtual void FadeOutImmediately()
    {
        gameObject.SetActive(false);
        canvastable.DOFade(0.0f, 0.0f);
        canvaswall.DOFade(0.0f, 0.0f);
        
    }
    
    protected virtual void EnableButtons()
    {
        if (buttons == null)
            return;

        foreach (var button in buttons)
            button.enabled = true;
    }
    
  
    
    public virtual async void Activate(int millisecondsDelay, float fadeInDuration)
    {
        await Task.Delay(millisecondsDelay);
    }

    public virtual void Deactivate(float fadeOutDuration)
    {
        FadeOut(fadeOutDuration, 0.0f);
    }

    public virtual void ButtonFunction(int index)
    {
        
    }

    public Vector3 ConvertToCamScreenCoord(Vector3 input)
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
}
