using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClick : MonoBehaviour
{
    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.BroadCast(EventType.ShowText);
        }
        );
    }
}
