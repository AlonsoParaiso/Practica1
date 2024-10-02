using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hud : MonoBehaviour
{
    private TMP_Text text;
    public GameManager.GameManagerVariables variable;
    private int point=0;

    void Start()
    {
        text = GetComponent<TMP_Text>();

    }
    void Update()
    {
        switch (variable)
        {
            case GameManager.GameManagerVariables.POINTS:
                if (point != GameManager.instance.GetPoints())
                {
                    point = GameManager.instance.GetPoints();
                    StartCoroutine(FadeOut());

                }
                break;
        }


        IEnumerator FadeOut()
        {
            Color color = text.color; // Para guardar el color actual del objecto
            for (float alpha = 1.0f; alpha >= 0; alpha -= 0.01f)  
            {
                color.a = alpha;
                text.color = color;
                yield return null;

            }
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            text.text = "Points: " + GameManager.instance.GetPoints();
            Color color = text.color;
            for (float alpha = 0.0f; alpha <= 1; alpha += 0.01f)
            {
                color.a = alpha;
                text.color = color;
                yield return null;
            }
        }
    }
}