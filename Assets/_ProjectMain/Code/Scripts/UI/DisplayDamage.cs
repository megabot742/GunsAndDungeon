using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float timeDisplay = 0.5f;
    [SerializeField] GameObject damageUI;
    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }
    IEnumerator ShowSplatter()
    {
        damageUI.SetActive(true);
        yield return new WaitForSeconds(timeDisplay);
        damageUI.SetActive(false);
    }
}
