
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>
{

}

public class Player : NetworkBehaviour {

    [SerializeField] ToggleEvent OnToggleShared;
    [SerializeField] ToggleEvent OnToggleLocal;
    [SerializeField] ToggleEvent OnToggleRemote;

    [SerializeField] float respawnTime = 5f;

    public GameObject mainCamera;

    void Start()
    {
        mainCamera = Camera.main.gameObject;

        EnablePlayer();
    }

    private void EnablePlayer()
    {

        if (isLocalPlayer)
        {
            PlayerCanvas.canvas.Initialize();
            mainCamera.SetActive(false);
        }

        OnToggleShared.Invoke(true);

        if (isLocalPlayer)
            OnToggleLocal.Invoke(true);
        else
            OnToggleRemote.Invoke(true);
    }

    private void DisablePlayer()
    {
        if (isLocalPlayer)
        {
            PlayerCanvas.canvas.HideCrosshair();
            mainCamera.SetActive(true);
        }

        OnToggleShared.Invoke(false);

        if (isLocalPlayer)
            OnToggleLocal.Invoke(false);
        else
            OnToggleRemote.Invoke(false);
    }

    public void Die()
    {
        DisablePlayer();
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        if (isLocalPlayer)
        {
            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }

        EnablePlayer();
    }
}
