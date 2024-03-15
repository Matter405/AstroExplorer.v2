using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HazardVolume : MonoBehaviour
{

    private float _moveIntensity;
    Vector3 lastVelocity;
    Rigidbody _rb = null;

    [SerializeField] TMP_Text _loseText;
    [SerializeField] AudioClip _loseSound = null;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerShip playerShip = collision.gameObject.GetComponent<PlayerShip>();
        // If we found something valid, continue
        if (playerShip != null)
        {
            playerShip.Kill();
            Destroy(gameObject);
            AudioHelper.PlayClip2D(_loseSound, 1);
            _loseText.enabled = true;
        }
        else
        {
            GetComponent<Rigidbody>().velocity =
                Vector3.Reflect(lastVelocity,
                collision.contacts[0].normal);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _loseText.enabled = false;
        StartingMovement();
    }

    private void FixedUpdate()
    {
        lastVelocity = GetComponent<Rigidbody>().velocity;
    }

    void StartingMovement()
    {
        _moveIntensity = (int)(100 + (50 * Random.Range(0, 9)));
        Vector3 moveDirection = transform.forward * _moveIntensity;
        _rb.AddForce(moveDirection);
    }
}
