using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private int CountDownAmount;
    [SerializeField] private TMP_Text CountdownText;

    private void OnEnable()
    {
        StartCoroutine(CountdownCoroutine(CountDownAmount));
    }

    private IEnumerator CountdownCoroutine(int countDownAmount)
    {
        for (int i = 3; i > 0; i--)
        {
            CountdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
    }
}
