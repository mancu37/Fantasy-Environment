using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCanvas : MonoBehaviour {

    public static PlayerCanvas canvas;

    [Header("Component References")]
    [SerializeField]
    Image crosshair;

    [SerializeField]
    UIFader damage;

    [SerializeField]
    CanvasGroup playerDamage;

    [SerializeField]
    Slider health;

    [SerializeField]
    Text log;

    [SerializeField]
    GameObject healthBar;

    private void Awake()
    {
        if(canvas == null)
        {
            canvas = this;
            HideHealthBar();
        }
        else if (canvas != null)
        {
            Destroy(gameObject);
        }
    }

    private void Reset()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        damage = GameObject.Find("DamageFlash").GetComponent<UIFader>();
        log = GameObject.Find("Log").GetComponent<Text>();
        playerDamage = GameObject.Find("PlayerDamage").GetComponent<CanvasGroup>();
        healthBar = GameObject.Find("HealthBar");
        health = healthBar.GetComponent<Slider>();
    }

    public void Initialize()
    {
        healthBar.SetActive(true);
        crosshair.enabled = true;
        
    }

    public void FlashDamageEffect()
    {
        damage.FlashDamageEffect();
    }

    public void HideCrosshair()
    {
        crosshair.enabled = false;
    }

    public void HideHealthBar()
    {
        healthBar.SetActive(false);
    }

    public void WriteLog(string text)
    {
        log.text = text;
    }

    internal void SetHealth(int value)
    {
        health.value = value;        
    }

    public CanvasGroup playerDamageImage()
    {
        return playerDamage;
    }

    public void HidePlayerDamage()
    {
        playerDamage.alpha = 0f;
    }
}
