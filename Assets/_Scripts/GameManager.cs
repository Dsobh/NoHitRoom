using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Tiempo que dura la partida de base")]
    private float gameTime = 30f;
    [SerializeField, Tooltip("Tiempo extra")]
    private float extraTime = 5f;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    private float totalTime = 0f;
    
    private bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime+=Time.deltaTime;
        gameTime -= Time.deltaTime;
        UpdateTimeUI(gameTime);
        if(gameTime <=0)
        {
            HandleGameOver();
        }

        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            RestartGame();
        }
    }

    void OnEnable()
    {
        PlayerInteractions.OnPickExtraTime += HandlePickExtraTimeEvent;
        PlayerInteractions.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        PlayerInteractions.OnPickExtraTime -= HandlePickExtraTimeEvent;
        PlayerInteractions.OnGameOver -= HandleGameOver;
    }

    void OnDestroy()
    {
        PlayerInteractions.OnPickExtraTime -= HandlePickExtraTimeEvent;
        PlayerInteractions.OnGameOver -= HandleGameOver;
    }

    void HandlePickExtraTimeEvent()
    {
        gameTime += extraTime;
        StartCoroutine(_uiManager.ShowIncrementTimeText(Mathf.Round(gameTime), extraTime));
    }
    
    void HandleGameOver()
    {
        if(PlayerPrefs.GetFloat("time")<totalTime)
            PlayerPrefs.SetFloat("time",totalTime);
        Time.timeScale = 0;
        _audioSource.Stop();
        isGameOver = true;
        gameObject.GetComponent<AudioSource>().Play();
        _uiManager.ShowGameOverPanel(PlayerPrefs.GetFloat("time"), totalTime);

    }

    private void UpdateTimeUI(float value)
    {
        _uiManager.UpdateText(Mathf.Round(value));
    }

    public void RestartGame()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("MainScene");
        StopAllCoroutines();
        Time.timeScale = 1;

    }

}
