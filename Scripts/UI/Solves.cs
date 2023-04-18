using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solves : MonoBehaviour
{
    [SerializeField] private Menu _menu;

    private int _activeChild = 0;

    public void NextSolve()
    {
        if( _activeChild + 1 == transform.childCount )
        {
            _menu.PauseOff();
            gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild( _activeChild ).gameObject.SetActive(false);
            _activeChild++;
            transform.GetChild(_activeChild).gameObject.SetActive(true);
        }
    }
}
