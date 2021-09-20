using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDamage : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text text;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        while (text.color.a > 0)
        {
            rectTransform.anchoredPosition += Vector2.up * 2f;
            var color = text.color;
            color.a -= 0.01f;
            text.color = color;
            yield return new WaitForSeconds(.01f);
        }
        Destroy(gameObject);
    }
}
