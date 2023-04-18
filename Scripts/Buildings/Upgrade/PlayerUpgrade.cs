using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : BuildingUpgrade
{
    [SerializeField] private GameObject _player;

    protected GameObject Player => _player;
}
