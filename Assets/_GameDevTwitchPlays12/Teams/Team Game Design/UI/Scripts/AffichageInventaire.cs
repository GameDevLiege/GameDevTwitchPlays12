using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageInventaire : MonoBehaviour {

    public GameObject _inventairepos1;

    public GameObject _inventairepos2;

    public GameObject _inventairepos3;

    public GameObject _inventairepos4;

    public GameObject _inventairepos5;



    public void AffichageInventairePlayer(string _nomFaction, int _numPos, GameObject _Bonus)
    {

    }

    public void GestionFaction(string _nomFaction, int _numPos, GameObject _Bonus)
    {
        if (_nomFaction == "RED")
        {
            GestionNumPosRED(_numPos);
        }
        if (_nomFaction == "GREEN")
        {
            GestionNumPosGREEN(_numPos);
        }
        if (_nomFaction == "YELLOW")
        {
            GestionNumPosYELLOW(_numPos);
        }
        if (_nomFaction == "BLUE")
        {
            GestionNumPosBLUE(_numPos);
        }
    }

    public void GestionNumPosRED(int _numPos)
    {
        if (_numPos == 1)
        {

        }
        if (_numPos == 2)
        {

        }
        if (_numPos == 3)
        {

        }
        if (_numPos == 4)
        {

        }
        if (_numPos == 5)
        {

        }
    }
    public void GestionNumPosGREEN(int _numPos)
    {
        if (_numPos == 1)
        {

        }
        if (_numPos == 2)
        {

        }
        if (_numPos == 3)
        {

        }
        if (_numPos == 4)
        {

        }
        if (_numPos == 5)
        {

        }
    }
    public void GestionNumPosYELLOW(int _numPos)

    {
        if (_numPos == 1)
        {

        }
        if (_numPos == 2)
        {

        }
        if (_numPos == 3)
        {

        }
        if (_numPos == 4)
        {

        }
        if (_numPos == 5)
        {

        }
    }
    public void GestionNumPosBLUE(int _numPos)

    {
        if (_numPos == 1)
        {

        }
        if (_numPos == 2)
        {

        }
        if (_numPos == 3)
        {

        }
        if (_numPos == 4)
        {

        }
        if (_numPos == 5)
        {

        }
    }

    void Start()

    {



    }



    // Update is called once per frame

    void Update()

    {



    }

}