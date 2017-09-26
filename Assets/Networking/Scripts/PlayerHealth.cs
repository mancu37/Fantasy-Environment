
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SerializeField] int maxHealth = 100;

    [SyncVar(hook = "OnChangedHealthValue")]int health;

    Player player;
    

    private void Awake()
    {
        health = maxHealth;
        player = GetComponent<Player>();
    }

    

    [ServerCallback]
    private void OnEnable()
    {
        health = maxHealth;
    }

    [Server]
    public bool TakeDamage()
    {
        //Debug.Log("TakeDamage Method is called");
        bool died = false;

        if (health <= 0)
            return died;

        health -= 2;

        died = health <= 0;

        RpcTakeDamage(died);

        return died;
    }


    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if (isLocalPlayer)
        {
            //PlayerCanvas.canvas.WriteLog("Flash Damage: " + GetComponent<NetworkIdentity>().netId);
            PlayerCanvas.canvas.FlashDamageEffect();            
        }

        if (died)
        {
            player.Die();
        }
    }

    void OnChangedHealthValue(int value)
    {
        health = value;
        if (isLocalPlayer)
        {
            PlayerCanvas.canvas.SetHealth(value);

            if (health < 30)
                PlayerCanvas.canvas.playerDamageImage().alpha = 1;  // 1.0f - (health / 100);
        }
    }
}
