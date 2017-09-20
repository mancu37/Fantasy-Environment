using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour {

    public static PlayerCanvas canvas;

    [Header("Component References")]
    [SerializeField]
    Image crosshair;

    [SerializeField]
    UIFader damageImage;

    [SerializeField]
    Text health;

    private void Awake()
    {
        if(canvas == null)
        {
            canvas = this;
        }
        else if (canvas != null)
        {
            Destroy(gameObject);
        }
    }

    private void Reset()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        damageImage = GameObject.Find("DamageFlash").GetComponent<UIFader>();
        health = GameObject.Find("HealthValue").GetComponent<Text>();

    }

    public void Initialize()
    {
        crosshair.enabled = true;
    }

    internal void FlashDamageEffect()
    {
        
    }

    public void HideCrosshair()
    {
        crosshair.enabled = false;
    }

    public void SetHealth(int value)
    {
        health.text = value.ToString();
    }
}
