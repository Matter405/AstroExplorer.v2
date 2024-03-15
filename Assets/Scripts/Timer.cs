using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public float timeLeft;

    public bool timerOn = false;

    [SerializeField] private TMP_Text _timer;
    [SerializeField] PlayerShip playerToKill;

    [SerializeField] TMP_Text _loseText;
    [SerializeField] AudioClip _loseSound = null;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            if (playerToKill.gameObject.activeSelf == true)
            {
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                    UpdateTimer(timeLeft);
                }
                else
                {
                    playerToKill.Kill();
                    timerOn = false;
                    AudioHelper.PlayClip2D(_loseSound, 1);
                    _loseText.enabled = true;
                }
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _timer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
