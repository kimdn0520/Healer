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

    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(.0f, .0f, .0f, 1.0f);
    public float width, height;

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
        // ���� ������ PlayerController�� ã�Ƽ� ������ �Ҵ��մϴ�.
        PlayerController foundPlayerController = FindObjectOfType<PlayerController>();

        // PlayerController�� �����ϴ� ��� ������ �Ҵ�� ��ü�� GameManager�� playerController ������ �Ҵ��մϴ�.
        if (foundPlayerController != null)
        {
            playerController = foundPlayerController;
        }
    }

    private void Update()
    {
        
    }

    void OnGUI()
    {
        Rect position = new Rect(width, height, Screen.width, Screen.height);

        float fps = 1.0f / Time.deltaTime;
        float ms = Time.deltaTime * 1000.0f;
        string text = string.Format("{0:N1} FPS ({1:N1}ms)", fps, ms);

        GUIStyle style = new GUIStyle();

        style.fontSize = fontSize;
        style.normal.textColor = color;

        GUI.Label(position, text, style);
    }
}
