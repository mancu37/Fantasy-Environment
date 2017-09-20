using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    public int selectedWeapon = 0;
    public int previousSelectedWeapon;

    public Animator animator;
    public float weaponNextFireRate = 0f;
    public float weaponFireRate = 15f;
    public ParticleSystem muzzeflash;

    [SerializeField]
    Weapon[] weapons;
    [SerializeField]
    Transform weaponHolder;
    [SerializeField]
    Transform firePosition;

    // Use this for initialization
    void Start () {

        if (!isLocalPlayer)
            return;

        foreach(Weapon weapon in weapons)
        {
            foreach(WeaponSound sound in weapon.sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
            }
        }

        SelectWeapon();
	}

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in weaponHolder)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                animator = weapon.GetComponent<Animator>();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButton("Fire2"))
        {
            
            animator.SetBool("Aim", true);
        }
        else
        {
            animator.SetBool("Aim", false);
        }

        if (Input.GetButton("Fire1") && weapons[selectedWeapon].Type == Weapon.TypeEnum.Automatic)
        {

            animator.SetBool("Shoot", true);

            if (Time.time >= weapons[selectedWeapon].WeaponNextFireRate)
            {
                

                weapons[selectedWeapon].WeaponNextFireRate = Time.time + 1f / weapons[selectedWeapon].WeaponFireRate;

                //weapons[selectedWeapon].Muzzeflash.Play();

                //PlaySound("Shoot", weapons[selectedWeapon].sounds);

                CmdFireShot(firePosition.position, firePosition.forward);
                
            }
        }

        if (Input.GetButtonDown("Fire1") && weapons[selectedWeapon].Type == Weapon.TypeEnum.Manual)
        {

            

            if (Time.time >= weapons[selectedWeapon].WeaponNextFireRate)
            {
                animator.SetBool("Shoot", true);

                weapons[selectedWeapon].WeaponNextFireRate = Time.time + weapons[selectedWeapon].WeaponFireRate;

                CmdFireShot(firePosition.position, firePosition.forward);
                


            }
        }

        if (!Input.GetButton("Fire1") || !Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Shoot", false);
        }


        previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
            SelectWeapon();

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
            PlaySound("Reload", weapons[selectedWeapon].sounds);
        }
    }

    public void PlaySound(string name, WeaponSound[] weaponSounds)
    {
        WeaponSound s = Array.Find(weaponSounds, p => p.name == name);
        s.source.Play();
    }

  

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {

        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red, 1f);

        bool result = Physics.Raycast(ray, out hit, 50f);

        
        if (result)
        {
            Debug.Log("Hit something: " + hit.collider.name);

            PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();

            if (enemy != null)
                enemy.TakeDamage();
        }


        RpcShowEffect();
    }

    [ClientRpc]
    void RpcShowEffect()
    {

        Debug.Log("RpcShowEffect Method called");

        weapons[selectedWeapon].Muzzeflash.Play();
        //Array.Find(weapons[selectedWeapon].sounds, p => p.name == "Shoot").source.Play();


    }


}
