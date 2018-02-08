using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageInventaire : MonoBehaviour {

    public GameObject _inventaire1;
    public GameObject _inventaire2;
    public GameObject _inventaire3;
    public GameObject _inventaire4;
    public GameObject _inventaire5;
    public GameObject _inventaire6;
    public GameObject _inventaire7;
    public GameObject _inventaire8;
    public GameObject _inventaire9;
    public GameObject _inventaire10;
    public GameObject _inventaire11;
    public GameObject _inventaire12;
    public GameObject _inventaire13;
    public GameObject _inventaire14;
    public GameObject _inventaire15;

    public GameObject _inventaire16;
    public GameObject _inventaire17;
    public GameObject _inventaire18;
    public GameObject _inventaire19;
    public GameObject _inventaire20;
    public GameObject _inventaire21;
    public GameObject _inventaire22;
    public GameObject _inventaire23;
    public GameObject _inventaire24;
    public GameObject _inventaire25;
    public GameObject _inventaire26;
    public GameObject _inventaire27;
    public GameObject _inventaire28;
    public GameObject _inventaire29;
    public GameObject _inventaire30;

    public GameObject _inventaire31;
    public GameObject _inventaire32;
    public GameObject _inventaire33;
    public GameObject _inventaire34;
    public GameObject _inventaire35;
    public GameObject _inventaire36;
    public GameObject _inventaire37;
    public GameObject _inventaire38;
    public GameObject _inventaire39;
    public GameObject _inventaire40;
    public GameObject _inventaire41;
    public GameObject _inventaire42;
    public GameObject _inventaire43;
    public GameObject _inventaire44;
    public GameObject _inventaire45;

    public GameObject _inventaire46;
    public GameObject _inventaire47;
    public GameObject _inventaire48;
    public GameObject _inventaire49;
    public GameObject _inventaire50;
    public GameObject _inventaire51;
    public GameObject _inventaire52;
    public GameObject _inventaire53;
    public GameObject _inventaire54;
    public GameObject _inventaire55;
    public GameObject _inventaire56;
    public GameObject _inventaire57;
    public GameObject _inventaire58;
    public GameObject _inventaire59;
    public GameObject _inventaire60;





    public void AjoutInventaire(string _Faction, int _numPos, GameObject _Bonus)
    {
        if (_Faction == "RED")
        {
            PositionRED(_numPos, _Bonus);
        }
        if (_Faction == "GREEN")
        {
            PositionGREEN(_numPos, _Bonus);
        }
        if (_Faction == "YELLOW")
        {
            PositionYELLOW(_numPos, _Bonus);
        }
        if (_Faction == "BLUE")
        {
            PositionBLUE(_numPos, _Bonus);
        }
    }

    public void RetireInventaire(string _Faction, int _numPos, GameObject _Bonus)
    {
        if (_Faction == "RED")
        {
            RetirePositionRED(_numPos, _Bonus);
        }
        if (_Faction == "GREEN")
        {
            RetirePositionGREEN(_numPos, _Bonus);
        }
        if (_Faction == "YELLOW")
        {
            RetirePositionYELLOW(_numPos, _Bonus);
        }
        if (_Faction == "BLUE")
        {
            RetirePositionBLUE(_numPos, _Bonus);
        }
    }


    private void PositionRED(int _numPos, GameObject _Bonus)
    {

        if (_numPos ==1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire1.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire2.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire3.SetActive(true);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire4.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire5.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire6.SetActive(true);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire7.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire8.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire9.SetActive(true);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire10.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire11.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire12.SetActive(true);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire13.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire14.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire15.SetActive(true);
            }
        }
    }

    private void PositionGREEN(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire16.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire17.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire18.SetActive(true);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire19.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire20.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire21.SetActive(true);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire22.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire23.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire24.SetActive(true);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire25.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire26.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire27.SetActive(true);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire28.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire29.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire30.SetActive(true);
            }
        }
    }

    private void PositionYELLOW(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire31.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire32.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire33.SetActive(true);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire34.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire35.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire36.SetActive(true);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire37.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire38.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire39.SetActive(true);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire40.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire41.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire42.SetActive(true);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire43.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire44.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire45.SetActive(true);
            }
        }
    }

    private void PositionBLUE(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire46.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire47.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire48.SetActive(true);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire49.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire50.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire51.SetActive(true);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire52.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire53.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire54.SetActive(true);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire55.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire56.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire57.SetActive(true);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire58.SetActive(true);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire59.SetActive(true);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire60.SetActive(true);
            }
        }
    }


    private void RetirePositionRED(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire1.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire2.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire3.SetActive(false);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire4.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire5.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire6.SetActive(false);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire7.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire8.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire9.SetActive(false);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire10.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire11.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire12.SetActive(false);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire13.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire14.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire15.SetActive(false);
            }
        }
    }

    private void RetirePositionGREEN(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire16.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire17.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire18.SetActive(false);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire19.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire20.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire21.SetActive(false);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire22.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire23.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire24.SetActive(false);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire25.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire26.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire27.SetActive(false);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire28.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire29.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire30.SetActive(false);
            }
        }
    }

    private void RetirePositionYELLOW(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire31.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire32.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire33.SetActive(false);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire34.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire35.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire36.SetActive(false);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire37.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire38.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire39.SetActive(false);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire40.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire41.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire42.SetActive(false);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire43.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire44.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire45.SetActive(false);
            }
        }
    }

    private void RetirePositionBLUE(int _numPos, GameObject _Bonus)
    {

        if (_numPos == 1)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire46.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire47.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire48.SetActive(false);
            }
        }
        if (_numPos == 2)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire49.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire50.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire51.SetActive(false);
            }
        }
        if (_numPos == 3)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire52.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire53.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire54.SetActive(false);
            }
        }
        if (_numPos == 4)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire55.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire56.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire57.SetActive(false);
            }
        }
        if (_numPos == 5)
        {
            if (_Bonus.name == "PELLE")
            {
                _inventaire58.SetActive(false);
            }
            if (_Bonus.name == "GRENADE")
            {
                _inventaire59.SetActive(false);
            }
            if (_Bonus.name == "CAILLOUX")
            {
                _inventaire60.SetActive(false);
            }
        }
    }


    private void Awake()
    {
        _inventaire1.SetActive(false);
        _inventaire2.SetActive(false);
        _inventaire3.SetActive(false);
        _inventaire4.SetActive(false);
        _inventaire5.SetActive(false);
        _inventaire6.SetActive(false);
        _inventaire7.SetActive(false);
        _inventaire8.SetActive(false);
        _inventaire9.SetActive(false);
        _inventaire10.SetActive(false);
        _inventaire11.SetActive(false);
        _inventaire12.SetActive(false);
        _inventaire13.SetActive(false);
        _inventaire14.SetActive(false);
        _inventaire15.SetActive(false);

        _inventaire16.SetActive(false);
        _inventaire17.SetActive(false);
        _inventaire18.SetActive(false);
        _inventaire19.SetActive(false);
        _inventaire20.SetActive(false);
        _inventaire21.SetActive(false);
        _inventaire22.SetActive(false);
        _inventaire23.SetActive(false);
        _inventaire24.SetActive(false);
        _inventaire25.SetActive(false);
        _inventaire26.SetActive(false);
        _inventaire27.SetActive(false);
        _inventaire28.SetActive(false);
        _inventaire29.SetActive(false);
        _inventaire30.SetActive(false);

        _inventaire31.SetActive(false);
        _inventaire32.SetActive(false);
        _inventaire33.SetActive(false);
        _inventaire34.SetActive(false);
        _inventaire35.SetActive(false);
        _inventaire36.SetActive(false);
        _inventaire37.SetActive(false);
        _inventaire38.SetActive(false);
        _inventaire39.SetActive(false);
        _inventaire40.SetActive(false);
        _inventaire41.SetActive(false);
        _inventaire42.SetActive(false);
        _inventaire43.SetActive(false);
        _inventaire44.SetActive(false);
        _inventaire45.SetActive(false);

        _inventaire46.SetActive(false);
        _inventaire47.SetActive(false);
        _inventaire48.SetActive(false);
        _inventaire49.SetActive(false);
        _inventaire50.SetActive(false);
        _inventaire51.SetActive(false);
        _inventaire52.SetActive(false);
        _inventaire53.SetActive(false);
        _inventaire54.SetActive(false);
        _inventaire55.SetActive(false);
        _inventaire56.SetActive(false);
        _inventaire57.SetActive(false);
        _inventaire58.SetActive(false);
        _inventaire59.SetActive(false);
        _inventaire60.SetActive(false);


        
    }

}
