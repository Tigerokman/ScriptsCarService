using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerTextSound))]
[RequireComponent(typeof(PlayerParticles))]
public class PlayerBag : Bag
{
    [SerializeField] private Animator _animator;

    private PlayerTextSound _textSound;
    private PlayerParticles _particles;
    private int _strenght = 5;
    private bool _hasStrenght => CountDetail <= _strenght;
    private string _isHold = "IsHold";


    public bool HasStrenght => _hasStrenght;
    public event UnityAction DetailCountChange;

    private void Start()
    {
        _textSound = GetComponent<PlayerTextSound>();
        _particles  = GetComponent<PlayerParticles>();
    }

    private void Update()
    {
        _animator.SetBool(_isHold, CountDetail > 0);
    }

    public void StrenghtUp(int upgrade)
    {
        _strenght += upgrade;
        _textSound.LevelUpText();
        _particles.UpgradeOn(Color.red);
        DetailCountChange?.Invoke();
    }

    public override void AddDetail(Detail detail)
    {
        base.AddDetail(detail);

        if (CountDetail == _strenght + 1)
            DetailCountChange?.Invoke();
        else if(FreePlace == false)
            DetailCountChange?.Invoke();
    }

    public override void RemoveDetail()
    {
        base.RemoveDetail();

        if(CountDetail >= _strenght || _strenght > MaxBagSize)
        DetailCountChange?.Invoke();
    }

    public override void UpgradeBag(int upgrade)
    {
        base.UpgradeBag(upgrade);
        _textSound.LevelUpText();
        _particles.UpgradeOn(Color.blue);
        DetailCountChange?.Invoke();
    }
}
