using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
        EventCenter.AddListener(EventType.ShowText, Show);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.ShowText, Show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
