using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GM : MonoBehaviour
{
    public bool isOpen = false;
#if UNITY_EDITOR

    void Start()
    {
        isOpen = false;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            isOpen = !isOpen;
            if(isOpen) UIManager.Instance.Show("GMUI");
            else UIManager.Instance.Close("GMUI");
        }
    }
#endif

}
