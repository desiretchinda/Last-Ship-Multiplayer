using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;
using UnityEngine.Events;

public class UIFader : MonoBehaviour
{

    public static UIFader Instance;

    public Image img;
    public enum FADE { FadeIn, FadeOut }

    private void Awake()
    {
        Instance = this;
    }

    public void Fade(FADE fadeDir, float fadeDuration, float StartDelay, UnityAction OnEnd = null, UnityAction OnStart = null)
    {
        if (img != null)
        {

            if (fadeDir == FADE.FadeIn)
            {
                StartCoroutine(FadeCoroutine(1f, 0f, fadeDuration, StartDelay, true, OnEnd, OnStart));
            }

            if (fadeDir == FADE.FadeOut)
            {
                StartCoroutine(FadeCoroutine(0f, 1f, fadeDuration, StartDelay, false, OnEnd, OnStart));

            }
        }
    }

    IEnumerator FadeCoroutine(float From, float To, float Duration, float StartDelay, bool DisableOnFinish, UnityAction OnEnd = null, UnityAction OnStart = null)
    {
        yield return new WaitForSeconds(StartDelay);
        if (OnStart != null)
            OnStart();

        float t = 0;
        Color col = img.color;
        img.enabled = true;
        img.color = new Color(col.r, col.g, col.b, From);

        while (t < 1)
        {
            float alpha = Mathf.Lerp(From, To, t);
            img.color = new Color(col.r, col.g, col.b, alpha);
            t += Time.deltaTime / Duration;
            yield return 0;
        }

        img.color = new Color(col.r, col.g, col.b, To);
        img.enabled = !DisableOnFinish;
        if (OnEnd != null)
            OnEnd.Invoke();
    }
}
