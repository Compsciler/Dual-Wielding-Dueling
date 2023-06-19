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

    List<DummyTarget> dummies = new List<DummyTarget>();

    void Awake()
    {
        PlayerCam.OnCameraPositionUpdated += StartTimerForFirstTime;
        DummyTarget.OnDummySpawned += AddDummyToList;
        DummyTarget.OnDummyHit += RemoveDummyFromList;
    }

    void OnDestroy()
    {
        PlayerCam.OnCameraPositionUpdated -= StartTimerForFirstTime;
        DummyTarget.OnDummySpawned -= AddDummyToList;
        DummyTarget.OnDummyHit -= RemoveDummyFromList;
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

    void AddDummyToList(DummyTarget dummy)
    {
        dummies.Add(dummy);
        Debug.Log(dummies.Count);
    }

    void RemoveDummyFromList(DummyTarget dummy)
    {
        dummies.Remove(dummy);

        if (dummies.Count == 0)
        {
            StopTimer();
        }
    }
}
