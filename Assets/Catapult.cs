using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Catapult : MonoBehaviour
{
    [Header("Shot")]
    [SerializeField] private float _shotForce;
    [SerializeField] private float _shotDamper;
    [SerializeField] private float _shotPosition;

    [Header("Reload")]
    [SerializeField] private float _reloadForce;
    [SerializeField] private float _reloadDamper;
    [SerializeField] private float _reloadPosition;
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private Transform _projectilePosition;
    [SerializeField] private float _timeProjectileSpawn;
    
    private float _curretTimeProjectileSpawn;
    private HingeJoint _hingeJoint;
    private JointSpring _springShot;
    private JointSpring _springReload;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        
        _springShot = new JointSpring();
        _springShot.spring = _shotForce;
        _springShot.damper = _shotDamper;
        _springShot.targetPosition = _shotPosition;

        _springReload = new JointSpring();
        _springReload.spring = _reloadForce;
        _springReload.damper = _reloadDamper;
        _springReload.targetPosition = _reloadPosition;

        _hingeJoint.spring = _springReload;
        _hingeJoint.useSpring = true;
    }

    private void Update()
    {
        if (_projectile != null && _projectilePosition != null)
        {
            if (_curretTimeProjectileSpawn > 0)
            {
                _curretTimeProjectileSpawn -= Time.deltaTime;

                if (_curretTimeProjectileSpawn <= 0)
                {
                    _projectile.transform.position = _projectilePosition.position;
                    _projectile.velocity = Vector3.zero;
                }
            }
        }

        if (_hingeJoint != null)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Shot();
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                Reload();
            }
        }
    }

    private void Shot()
    {
        _hingeJoint.spring = _springShot;
    }

    private void Reload()
    {
        _hingeJoint.spring = _springReload;
        _curretTimeProjectileSpawn = _timeProjectileSpawn;
    }
}
