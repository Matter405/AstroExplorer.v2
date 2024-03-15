using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinVolume : MonoBehaviour
{
    [SerializeField] TMP_Text winText;
    [SerializeField] AudioClip _winSound = null;

    private void Start()
    {
        winText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        if(playerShip != null)
        {
            other.gameObject.SetActive(false);
            AudioHelper.PlayClip2D(_winSound, 1);
            winText.enabled = true;
        }
    }
}
