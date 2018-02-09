using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGame : MonoBehaviour {

    //TimerGame
    public Image TimerSlide;
    //TimerEquipeRED
    public Image TimerSlideEquipeRED;
    //TimerEquipeGREEN
    public Image TimerSlideEquipeGREEN;
    //TimerEquipeYELLOW
    public Image TimerSlideEquipeYELLOW;
    //TimerEquipeBLUE
    public Image TimerSlideEquipeBLUE;



    public void TimerGameStart(float TimerActuel, float maxTimer)
    {
       
        TimerSlide.fillAmount = TimerActuel/1200;
    }

    public void TimerEquipeRED(float TimerActuel, float maxTimer)
    {

        TimerSlideEquipeRED.enabled = true;
        TimerSlideEquipeRED.fillAmount = TimerActuel / 60;

        TimerSlideEquipeGREEN.enabled = false;
        TimerSlideEquipeYELLOW.enabled = false;
        TimerSlideEquipeBLUE.enabled = false;

    }

    public void TimerEquipeGREEN(float TimerActuel, float maxTimer)
    {
        TimerSlideEquipeGREEN.enabled = true;
        TimerSlideEquipeGREEN.fillAmount = TimerActuel / 60;

        TimerSlideEquipeRED.enabled = false;
        TimerSlideEquipeYELLOW.enabled = false;
        TimerSlideEquipeBLUE.enabled = false;
    }

    public void TimerEquipeYELLOW(float TimerActuel, float maxTimer)
    {
        TimerSlideEquipeYELLOW.enabled = true;
        TimerSlideEquipeYELLOW.fillAmount = TimerActuel / 60;

        TimerSlideEquipeRED.enabled = false;
        TimerSlideEquipeGREEN.enabled = false;
        TimerSlideEquipeBLUE.enabled = false;
    }

    public void TimerEquipeBLUE(float TimerActuel, float maxTimer)
    {
        TimerSlideEquipeBLUE.enabled = true;
        TimerSlideEquipeBLUE.fillAmount = TimerActuel / 60;

        TimerSlideEquipeRED.enabled = false;
        TimerSlideEquipeGREEN.enabled = false;
        TimerSlideEquipeYELLOW.enabled = false;
    }
    
    private void Awake()
    {
        TimerSlideEquipeRED.enabled = false;
        TimerSlideEquipeGREEN.enabled = false;
        TimerSlideEquipeYELLOW.enabled = false;
        TimerSlideEquipeBLUE.enabled = false;
    }
}
