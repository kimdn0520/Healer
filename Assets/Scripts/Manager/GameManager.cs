using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 현재 씬에서 PlayerController를 찾아서 변수에 할당합니다.
        PlayerController foundPlayerController = FindObjectOfType<PlayerController>();

        // PlayerController가 존재하는 경우 변수에 할당된 객체를 GameManager의 playerController 변수에 할당합니다.
        if (foundPlayerController != null)
        {
            playerController = foundPlayerController;
        }
    }
}
