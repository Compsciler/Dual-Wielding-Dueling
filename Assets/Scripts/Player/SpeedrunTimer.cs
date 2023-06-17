using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedrunTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private float timer = 0f;
    private bool timerActive = false;

    void Start()
    {
        PlayerCam.OnCameraPositionUpdated += StartTimerForFirstTime;
        DummyTarget.OnDummyHit += StopTimer;
    }

    void OnDestroy()
    {
        PlayerCam.OnCameraPositionUpdated -= StartTimerForFirstTime;
        DummyTarget.OnDummyHit -= StopTimer;
    }

    void StartTimerForFirstTime()
    {
        if (timer != 0f) { return; }
        timerActive = true;
    }

    void StopTimer()
    {
        timerActive = false;
    }

    void Update()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("F2");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
