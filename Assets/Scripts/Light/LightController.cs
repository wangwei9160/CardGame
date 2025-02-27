using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D fireLight;
    public float beginVal;
    public float val;
    public bool isOk;

    private void Start()
    {
        fireLight.pointLightOuterRadius = beginVal;
        isOk = false;
    }

    private void Update()
    {
        if(isOk) { return; }
        if(fireLight.pointLightOuterRadius < val)
        {
            fireLight.pointLightOuterRadius += Time.deltaTime;
        }else
        {
            isOk = true;
            EventCenter.Broadcast(EventDefine.OnStartSceneUIShow);
            return;
        }
    }

}
