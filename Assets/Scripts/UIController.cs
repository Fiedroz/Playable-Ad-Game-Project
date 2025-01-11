using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject defeatPanel;
    public Image fadePanel;
    public Image logo;
    public Image icon;
                        
    private void Start()
    {
        FadeOut(2f);
    }

    public void WinScreen(bool state)
    {
        winPanel.SetActive(state);
    }

    public void DefeatScreen(bool state)
    {
        defeatPanel.SetActive(state);
    }

    public void FadeIn(float duration)
    {
        fadePanel.gameObject.SetActive(true);
        StartCoroutine(FadeUIImage(fadePanel, 0f, 1f, duration));
    }

    public void FadeOut(float duration)
    {
        fadePanel.gameObject.SetActive(true);
        StartCoroutine(FadeUIImage(fadePanel, 1f, 0f, duration));
    }

    private IEnumerator FadeUIImage(Image image, float startAlpha, float endAlpha, float duration)
    {
        yield return new WaitForSeconds(2f);
        float elapsedTime = 0f;
        Color color = image.color;
        Color colorLogo = logo.color;
        Color colorIcon = icon.color;

        while (elapsedTime < duration)
        {
            float alphaProgress = elapsedTime / duration;
            color.a = startAlpha + alphaProgress * (endAlpha - startAlpha);
            image.color = color;
            icon.color = color;
            logo.color= color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        colorLogo.a = endAlpha;
        colorIcon.a = endAlpha;
        image.color = color;
        logo.color= colorLogo;
        icon.color= colorIcon;
    }
}

