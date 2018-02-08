using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {
    
    public Text _lignePlayer1;
    public Text _lignePlayer2;
    public Text _lignePlayer3;
    public Text _lignePlayer4;
    public Text _lignePlayer5;
    public Text _lignePlayer6;
    public Text _lignePlayer7;
    public Text _lignePlayer8;
    public Text _lignePlayer9;
    public Text _lignePlayer10;
    public Text _lignePlayer11;
    public Text _lignePlayer12;
    public Text _lignePlayer13;
    public Text _lignePlayer14;
    public Text _lignePlayer15;
    public Text _lignePlayer16;
    public Text _lignePlayer17;
    public Text _lignePlayer18;
    public Text _lignePlayer19;
    public Text _lignePlayer20;

    public GameObject _inventairej1;
    public GameObject _inventairej2;
    public GameObject _inventairej3;
    public GameObject _inventairej4;
    public GameObject _inventairej5;
    public GameObject _inventairej6;
    public GameObject _inventairej7;
    public GameObject _inventairej8;
    public GameObject _inventairej9;
    public GameObject _inventairej10;
    public GameObject _inventairej11;
    public GameObject _inventairej12;
    public GameObject _inventairej13;
    public GameObject _inventairej14;
    public GameObject _inventairej15;
    public GameObject _inventairej16;
    public GameObject _inventairej17;
    public GameObject _inventairej18;
    public GameObject _inventairej19;
    public GameObject _inventairej20;


    public void _placementPlayer(string _faction,int _numPos, int _numPlayer, string _nom, int _lvl, int _argent)
    {
        if (_faction == "RED")
        {
            if (_numPos == 1)
            {
                _lignePlayer1.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 2)
            {
                _lignePlayer2.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 3)
            {
                _lignePlayer3.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 4)
            {
                _lignePlayer4.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 5)
            {
                _lignePlayer5.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
        }
        if (_faction == "GREEN")
        {
            if (_numPos == 1)
            {
                _lignePlayer6.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 2)
            {
                _lignePlayer7.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 3)
            {
                _lignePlayer8.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 4)
            {
                _lignePlayer9.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 5)
            {
                _lignePlayer10.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
        }
        if (_faction == "YELLOW")
        {
            if (_numPos == 1)
            {
                _lignePlayer11.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 2)
            {
                _lignePlayer12.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 3)
            {
                _lignePlayer13.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 4)
            {
                _lignePlayer14.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 5)
            {
                _lignePlayer15.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
        }
        if (_faction == "BLUE")
        {
            if (_numPos == 1)
            {
                _lignePlayer16.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 2)
            {
                _lignePlayer17.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 3)
            {
                _lignePlayer18.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 4)
            {
                _lignePlayer19.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
            if (_numPos == 5)
            {
                _lignePlayer20.text = _numPlayer + " - " + _nom + " - Lvl " + _lvl + " - $ " + _argent;
            }
        }

    }



    private void Awake()
    {
        _inventairej1.SetActive(false);
        _inventairej2.SetActive(false);
        _inventairej3.SetActive(false);
        _inventairej4.SetActive(false);
        _inventairej5.SetActive(false);
        _inventairej6.SetActive(false);
        _inventairej7.SetActive(false);
        _inventairej8.SetActive(false);
        _inventairej9.SetActive(false);
        _inventairej10.SetActive(false);
        _inventairej11.SetActive(false);
        _inventairej12.SetActive(false);
        _inventairej13.SetActive(false);
        _inventairej14.SetActive(false);
        _inventairej15.SetActive(false);
        _inventairej16.SetActive(false);
        _inventairej17.SetActive(false);
        _inventairej18.SetActive(false);
        _inventairej19.SetActive(false);
        _inventairej20.SetActive(false);
    }

	void Update ()
    {
		if(_lignePlayer1.text != "")
        {
            _inventairej1.SetActive(true);
        }
        if (_lignePlayer2.text != "")
        {
            _inventairej2.SetActive(true);
        }
        if (_lignePlayer3.text != "")
        {
            _inventairej3.SetActive(true);
        }
        if (_lignePlayer4.text != "")
        {
            _inventairej4.SetActive(true);
        }
        if (_lignePlayer5.text != "")
        {
            _inventairej5.SetActive(true);
        }
        if (_lignePlayer6.text != "")
        {
            _inventairej6.SetActive(true);
        }
        if (_lignePlayer7.text != "")
        {
            _inventairej7.SetActive(true);
        }
        if (_lignePlayer8.text != "")
        {
            _inventairej8.SetActive(true);
        }
        if (_lignePlayer9.text != "")
        {
            _inventairej9.SetActive(true);
        }
        if (_lignePlayer10.text != "")
        {
            _inventairej10.SetActive(true);
        }
        if (_lignePlayer11.text != "")
        {
            _inventairej11.SetActive(true);
        }
        if (_lignePlayer12.text != "")
        {
            _inventairej12.SetActive(true);
        }
        if (_lignePlayer13.text != "")
        {
            _inventairej13.SetActive(true);
        }
        if (_lignePlayer14.text != "")
        {
            _inventairej14.SetActive(true);
        }
        if (_lignePlayer15.text != "")
        {
            _inventairej15.SetActive(true);
        }
        if (_lignePlayer16.text != "")
        {
            _inventairej16.SetActive(true);
        }
        if (_lignePlayer17.text != "")
        {
            _inventairej17.SetActive(true);
        }
        if (_lignePlayer18.text != "")
        {
            _inventairej18.SetActive(true);
        }
        if (_lignePlayer19.text != "")
        {
            _inventairej19.SetActive(true);
        }
        if (_lignePlayer20.text != "")
        {
            _inventairej20.SetActive(true);
        }
    }
}
