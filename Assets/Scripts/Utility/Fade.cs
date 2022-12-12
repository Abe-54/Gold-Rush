using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    
    public IEnumerator FadeOut()
    {
        Tween fadeOut = GetComponent<Image>().DOColor(Color.black, 1f);
        yield return fadeOut.WaitForCompletion();
    }
    
    public IEnumerator FadeIn()
    {
        Tween fadeOut = GetComponent<Image>().DOColor(Color.clear, 1f);
        yield return fadeOut.WaitForCompletion();
    }
}
