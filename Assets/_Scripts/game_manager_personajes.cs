using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_personajes : MonoBehaviour
{
    public static GameManager_personajes Instance;
    public List<Personajes> personajes;

    private void Awake()
    {
        if (GameManager_personajes.Instance == null)
        {
            GameManager_personajes.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

