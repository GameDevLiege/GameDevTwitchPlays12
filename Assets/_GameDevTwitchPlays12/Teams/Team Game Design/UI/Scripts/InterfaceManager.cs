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

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
