using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HingeJoint))]
public class Rocking : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private float _motorTime = 0.2f;
    private float _curretMotorTime;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (_hingeJoint != null)
        {
            if(_curretMotorTime > 0)
            {
                _curretMotorTime -= Time.deltaTime;

                if(_curretMotorTime <= 0)
                {
                    _hingeJoint.useMotor = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _hingeJoint.useMotor = true;
                _curretMotorTime = _motorTime;
            }
        }
    }
}

