using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public bool started = false;
    public GameObject localPlayer;

    public static SceneData Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
