using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gamemanger;

    public void Awake()
    {
        if(gamemanger == null)
        {
            gamemanger = this;
        }
        else
        {
            Destroy(this);
        }
        
    }
}
