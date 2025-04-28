using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : MonoBehaviour
{

    public RectTransform r_uRectTransform;

    private void Awake()
    {

    }

    public void SetData(CardClass cfg)
    {

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
