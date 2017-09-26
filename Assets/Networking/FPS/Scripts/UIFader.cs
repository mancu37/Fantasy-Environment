using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{

    [SerializeField] CanvasGroup groupToFade;
    [SerializeField] float fadeSpeed;

    bool isSplaschInScreen;

    public void FlashDamageEffect()
    {
        if(!isSplaschInScreen)
            StartCoroutine(FadeInFadeOut());

    }

    IEnumerator FadeInFadeOut()
    {
        isSplaschInScreen = true;

        for (float i = 0; i <= 1.0; i += 0.1f * fadeSpeed)
        {
            groupToFade.alpha = i;
            yield return null;
        }
        groupToFade.alpha = 1;


        for (float i = 1.0f; i >= 0; i -= 0.1f * fadeSpeed)
        {
            groupToFade.alpha = i;
            yield return null;
        }

        groupToFade.alpha = 0;

        isSplaschInScreen = false;
    }

    IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1.0; i += 0.1f * fadeSpeed)
        {
            groupToFade.alpha = i;
            yield return null;
        }
        groupToFade.alpha = 1;

    }

    IEnumerator FadeOut()
    {
        for (float i = 1.0f; i >= 0; i -= 0.1f * fadeSpeed)
        {
            groupToFade.alpha = i;
            yield return null;
        }

        groupToFade.alpha = 0;
    }

}
