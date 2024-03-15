using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPowerup : MonoBehaviour
{
    [SerializeField] float _powerupDuration = 1.5f;

    [SerializeField] float _blindnessDuration = 1.5f;

    [SerializeField] GameObject _artToDisable = null;

    private int rand;

    [SerializeField] private Camera _camera = null;
    private float originR;
    private float originG;
    private float originB;
    private float originA;

    [SerializeField]
    private AudioClip _collectSound;
    [SerializeField]
    private AudioClip _collectEndSound;
    [SerializeField]
    ParticleSystem _collectParticlePrefab = null;
    [SerializeField]
    ParticleSystem _endParticlePrefab = null;

    Collider _collider = null;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        originR = _camera.backgroundColor.r;
        originG = _camera.backgroundColor.g;
        originB = _camera.backgroundColor.b;
        originA = _camera.backgroundColor.a;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip =
            other.gameObject.GetComponent<PlayerShip>();
        if (playerShip != null)
        {
            rand = UnityEngine.Random.Range(0, 9);
            Debug.Log(rand);
            if (rand >= 4)
            {
                // Powerup Sequence
                Debug.Log("PassThrough");
                StartCoroutine(PowerupSequence(playerShip));
            }
            else if(rand <= 3)
            {
                Debug.Log("Blind");
                StartCoroutine(BlindnessSequence());
            }
        }
    }

    public IEnumerator PowerupSequence(PlayerShip playerShip)
    {

        _collider.enabled = false;
        _artToDisable.SetActive(false);

        ActivatePassThrough(playerShip);

        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePassThrough(playerShip);

        Destroy(gameObject);

    }

    public IEnumerator BlindnessSequence()
    {
        _collider.enabled = false;
        _artToDisable.SetActive(false);

        ActivateBlindness();

        yield return new WaitForSeconds(_blindnessDuration);

        DeactivateBlindness();

        Destroy(gameObject);

    }

    private void ActivatePassThrough(PlayerShip playerShip)
    {
        playerShip.SetLayer(6);
        playerShip.SetPassThroughLook(true);
        if(_collectParticlePrefab != null)
        {
            Instantiate(_collectParticlePrefab,
                transform.position, transform.rotation);
        }
        AudioHelper.PlayClip2D(_collectSound, 1);
    }

    private void DeactivatePassThrough(PlayerShip playerShip)
    {
        playerShip.SetLayer(7);
        playerShip.SetPassThroughLook(false);
        if (_endParticlePrefab != null)
        {
            Instantiate(_endParticlePrefab,
                playerShip.transform.position,
                playerShip.transform.rotation);
        }
        AudioHelper.PlayClip2D(_collectEndSound, 1);
    }

    private void ActivateBlindness()
    {
        _camera.backgroundColor = new Color(1f, 0.37f, 0.16f, 0f);
    }

    private void DeactivateBlindness()
    {
        _camera.backgroundColor =
            new Color(originR, originG, originB, originA);
    }

}