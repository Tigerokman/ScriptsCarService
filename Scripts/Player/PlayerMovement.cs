using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerTextSound))]
//[RequireComponent(typeof(PlayerBag))]
[RequireComponent(typeof(PlayerParticles))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerModel;

    private PlayerParticles _particles;
    private PlayerBag _playerBag;
    private bool _speedDowned = false;
    private float _speed = 4;
    private float _speedDirection = 0.2f;
    private float _speedDownStrenght = 1.5f;
    private string _runAnimation = "IsRun";

    private void Awake()
    {
        _particles = GetComponent<PlayerParticles>();
        //_playerBag = GetComponent<PlayerBag>();
    }

    //private void OnEnable()
    //{
        //_playerBag.DetailCountChange += HasStrenghtSpeed;
    //}

    //private void OnDisable()
    //{
        //_playerBag.DetailCountChange -= HasStrenghtSpeed;
    //}

    private void Update()
    {
        Move();
    }

    public void SpeedUpgrade(float upgrade)
    {
        if (_playerBag.HasStrenght == false)
            upgrade /= _speedDownStrenght;

        _speed += upgrade;
        //_textSound.LevelUpText();
        _particles.UpgradeOn(Color.green);
    }

    private void HasStrenghtSpeed()
    {
        if (_playerBag.FreePlace == true)
        {
            if (_speedDowned == true && _playerBag.HasStrenght == true)
            {
                //_speed *= _speedDownStrenght;
                _speedDowned = false;
                Debug.Log("lil");
            }
            else if(_speedDowned == false && _playerBag.HasStrenght == false)
            {
                //_speed /= _speedDownStrenght;
                _speedDowned = true;
                Debug.Log("lol");
            }
        }
    }

    private void Move()
    {
        _animator.SetBool(_runAnimation, _joystick.Horizontal != 0 || _joystick.Vertical != 0);

        Vector3 move = new Vector3(_joystick.Horizontal * _speed, 0, _joystick.Vertical * _speed);

        if (Vector3.Angle(Vector3.forward, move) > 1f || Vector3.Angle(Vector3.forward, move) == 0)
        {
            if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
            {
                Vector3 _direction = Vector3.RotateTowards(transform.forward + move, move, _speedDirection, 0.0f);
                _playerModel.transform.rotation = Quaternion.LookRotation(_direction);;
            }
        }

        transform.position += move * Time.deltaTime;
    }
}
