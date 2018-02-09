﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerManager m_playerManager;
    public AudioSource m_audioSource;
    public AudioClip brawlSound;
    public AudioClip popSound;
    public AudioClip diggingSound;
    public AudioClip paperSound;
    public AudioClip hurtSound;
    public AudioClip coinSound;
    public AudioClip grenadeSound;

    public float TickDuration=2f;
    public bool supperShovelActive = false;

    public Faction Faction { get; set; }
    public Territory CurrentTerritory { get; set; }
    public bool HasGlasses { get; set; }
    public GameObject Glasses { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public int NumPlayer { get; set; }
    public Dictionary<int, int> Inventory { get; set; }

    public int NumberOfItem(int item)
    {
        var numberItem=0;
        if (Inventory.TryGetValue(item,out numberItem))
        {
            return numberItem;
        }
        return 0;
    }
    public Player ()
    {
        HasGlasses = false;
        Level = 1;
        Gold = 0;
        Inventory = new Dictionary<int, int>();
    }

    private void Awake()
    {

        m_audioSource = gameObject.AddComponent<AudioSource>();
        PlayerManager m_playerManager = FindObjectOfType<PlayerManager>();
        brawlSound = m_playerManager.brawlSound;
        popSound = m_playerManager.popSound;
        diggingSound= m_playerManager.diggingSound;
        paperSound=m_playerManager.paperSound;
        hurtSound= m_playerManager.hurtSound;
        coinSound=m_playerManager.coinSound;
    }

    public void ActivateShovel()
    {
        supperShovelActive = true;
        StartCoroutine(timerShovel());
    }

    IEnumerator timerShovel()
    {
        yield return new WaitForSeconds(TickDuration*15);
        supperShovelActive = false;
    }

    public void PlayCoin()
    {
        m_audioSource.PlayOneShot(coinSound);
    }
    public void PlayHurt()
    {
        m_audioSource.PlayOneShot(hurtSound);
    }
    public void PlayBrawl()
    {
        m_audioSource.PlayOneShot(brawlSound);
    }
    public void PlayPop()
    {
        m_audioSource.PlayOneShot(popSound);
    }
    public void PlayDig()
    {
        m_audioSource.PlayOneShot(diggingSound);
    }
    public void PlayPaper()
    {
        m_audioSource.PlayOneShot(paperSound);
    }
    public void PlayGrenade()
    {
        m_audioSource.PlayOneShot(grenadeSound);
    }
}
