using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speedIncreaseAmount = 20f;
    [SerializeField]
    private float _powerupDuration = 4f;

    [SerializeField]
    private GameObject _artToDisable = null;

    [Header("Audio Controll")]
    [SerializeField]
    private AudioClip _collectSound = null;
    [SerializeField]
    private AudioClip _endCollectSound = null;

    [SerializeField]
    private ParticleSystem _collectParticlePrefab;
    [SerializeField]
    private ParticleSystem _endParticlePrefab;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip =
            other.gameObject.GetComponent<PlayerShip>();
        if (playerShip != null)
        {
            // Powerup Sequence
            StartCoroutine(PowerupSequence(playerShip));
        }
    }

    // Coroutine
    public IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        // Soft Disable
        _collider.enabled = false;
        _artToDisable.SetActive(false);

        ActivatePowerup(playerShip);
        yield return new WaitForSeconds(_powerupDuration);

        // Reenable if desired
        DeactivatePowerup(playerShip);

        Destroy(gameObject);
    }

    public void ActivatePowerup(PlayerShip playerShip)
    {
        playerShip.SetMoveSpeed(_speedIncreaseAmount);
        playerShip.SetBoosters(true);
        // Spawns collect particle
        if(_collectParticlePrefab != null)
        {
            Instantiate(_collectParticlePrefab,
                transform.position, transform.rotation);
        }
        AudioHelper.PlayClip2D(_collectSound, 1);
    }

    private void DeactivatePowerup(PlayerShip playerShip)
    {
        playerShip.SetMoveSpeed(-_speedIncreaseAmount);
        if(_endParticlePrefab != null)
        {
            Instantiate(_endParticlePrefab,
                playerShip.transform.position, playerShip.transform.rotation);
        }
        playerShip.SetBoosters(false);
        AudioHelper.PlayClip2D(_endCollectSound, 1);
    }

}
