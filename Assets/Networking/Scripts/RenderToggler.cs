using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderToggler : MonoBehaviour {

    [SerializeField] float turnOnDelay = .1f;
    [SerializeField] float turnOffDelay = 5f;
    [SerializeField] bool enabledOnLoad = false;

    Renderer[] renderers;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);

        if (enabledOnLoad)
            EnableRenderers();
        else
            DisableRenderers();
    }

    private void DisableRenderers()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
    }

    private void EnableRenderers()
    {
        for(int i=0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }
    }

    public void ToggleRenderersDelayed(bool isOn)
    {
        if (isOn)
        {
            Invoke("EnableRenderers", turnOnDelay);
        }
        else
        {
            Invoke("DisableRenderers", turnOffDelay);
        }
    }
}
