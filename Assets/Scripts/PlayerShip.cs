using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    [SerializeField]
    private AudioSource _thrusterSound = null;
    [SerializeField]
    private TrailRenderer _trail = null;
    [SerializeField]
    private ParticleSystem _deathParticle = null;
    [SerializeField]
    private Material passThroughMaterialBody = null;
    [SerializeField]
    private Material passThroughMaterialFins = null;

    [Header("Player Ship Materials")]
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _nose;
    [SerializeField] private GameObject _bLFin;
    [SerializeField] private GameObject _bRFin;
    [SerializeField] private GameObject _tLFin;
    [SerializeField] private GameObject _tRFin;

    Material _originShipBodyMaterial = null;
    Material _originShipNoseMaterial = null;
    Material _originShipFinMaterial = null;


    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _trail.emitting = false;
        _thrusterSound.enabled = false;
        _originShipBodyMaterial = _body.GetComponent<Renderer>().material;
        _originShipNoseMaterial = _nose.GetComponent<Renderer>().material;
        _originShipFinMaterial = _bLFin.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            _thrusterSound.enabled = true;
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            _thrusterSound.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    //Uses forces to build momentum forward/backward
    void MoveShip()
    {
        // S/Down = -1, W/Up = 1, None = 0. Scale by moveSpeed
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        // combine our direction with our calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        // Apply the movement to the physics object
        _rb.AddForce(moveDirection);
    }

    // Don't use forces for this. We want rotations to be precise
    void TurnShip()
    {
        // A/Left = -1, D/Right = 1, None = 0. Scale by turnSpeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // Specify an axis to apply our turn amount (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // Spin the Rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill()
    { 
        this.gameObject.SetActive(false);
        Instantiate(_deathParticle,
            transform.position, transform.rotation);
    }

    public void SetMoveSpeed(float newSpeedAdjustment)
    {
        _moveSpeed += newSpeedAdjustment;
    }

    public void SetBoosters(bool activeState)
    {
        _trail.emitting = activeState;
    }

    public void SetLayer(int layerNum)
    {
        // Hazard: 6, Player: 7
        gameObject.layer = layerNum;
    }

    public void SetPassThroughLook(bool activeState)
    {
        if(activeState)
        {
            // Set the Ship Materials to the new Material
            _body.GetComponent<Renderer>().material = passThroughMaterialBody;
            _nose.GetComponent<Renderer>().material = passThroughMaterialFins;
            _bLFin.GetComponent<Renderer>().material = passThroughMaterialFins;
            _bRFin.GetComponent<Renderer>().material = passThroughMaterialFins;
            _tLFin.GetComponent<Renderer>().material = passThroughMaterialFins;
            _tRFin.GetComponent<Renderer>().material = passThroughMaterialFins;
        }
        else
        {
            // Set the Ship Materials back to the originals
            _body.GetComponent<Renderer>().material = _originShipBodyMaterial;
            _nose.GetComponent<Renderer>().material = _originShipNoseMaterial;
            _bLFin.GetComponent<Renderer>().material = _originShipFinMaterial;
            _bRFin.GetComponent<Renderer>().material = _originShipFinMaterial;
            _tLFin.GetComponent<Renderer>().material = _originShipFinMaterial;
            _tRFin.GetComponent<Renderer>().material = _originShipFinMaterial;
        }
    }

}
