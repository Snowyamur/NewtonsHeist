using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public IEnumerator FadeImageInOut(float t1, float t2, Image i, float delay)
    {
        StartCoroutine(FadeImageToFullAlpha(t1, i));
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeImageToZeroAlpha(t2, i));
    }

    public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
          i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
          while (i.color.a < 1.0f)
          {
              i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
              yield return null;
          }
    }

    public IEnumerator FadeImageToZeroAlpha(float t, Image i)
    {
          i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
          while (i.color.a > 0.0f)
          {
              i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
              yield return null;
          }
    }

    public IEnumerator FadeTextInOut(float t1, float t2, Text i, float delay)
    {
        StartCoroutine(FadeTextToFullAlpha(t1, i));
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeTextToZeroAlpha(t2, i));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
          i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
          while (i.color.a < 1.0f)
          {
              i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
              yield return null;
          }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
          i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
          while (i.color.a > 0.0f)
          {
              i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
              yield return null;
          }
    }
}
