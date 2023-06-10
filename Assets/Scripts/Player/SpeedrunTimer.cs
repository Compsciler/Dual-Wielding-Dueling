using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedrunTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private float timer;
    private bool timerActive = true;

    void Start()
    {
        DummyTarget.OnDummyHit += StopTimer;
    }

    void OnDestroy()
    {
        DummyTarget.OnDummyHit -= StopTimer;
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
