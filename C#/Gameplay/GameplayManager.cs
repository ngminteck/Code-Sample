using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }
    [SerializeField] private List<Page> pages = null;


    public void Awake()
    {
        AudioSource[] audiosource = (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource));

        foreach (AudioSource audio in audiosource)
        {
            audio.playOnAwake = false;
        }
    }


    void Start()
    {

        AudioSource[] audiosource = (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource));

        foreach (AudioSource audio in audiosource)
        {
            audio.playOnAwake = false;
        }

        DOTween.SetTweensCapacity(500, 50);

        // Initialize static object
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Initialize screens
        foreach (Page page in pages)
            page.Initialize();
    }

    private void Update()
    {
        // escape key to quit the application
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


      


    }

   

    public void RestartSession()
    {
      
    
    
        SplashPage.Instance.bSplashScreen = true;
        TLPage.Instance.bSplashScreen = true;
        MPPage.Instance.bSplashScreen = true;
        PNPage.Instance.bSplashScreen = true;
    
        SplashPage.Instance.Setup();
        SplashPage.Instance.Activate2(0, 1.0f);
        TLPage.Instance.BackToSplashAni();
        MPPage.Instance.BackToSplashAni();
        PNPage.Instance.BackToSplashAni();
    
       
    }

    static public void SetActiveRecursively(Transform obj, bool active)
    {
        // Set active state of object and all its children
        obj.gameObject.SetActive(active);
        foreach (Transform child in obj)
        {
            child.gameObject.SetActive(active);
            SetActiveRecursively(child, active);
        }
    }

   
}
