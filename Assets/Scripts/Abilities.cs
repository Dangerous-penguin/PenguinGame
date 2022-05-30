using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Abilities;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField] private AbilityBase powerSlapInfo;
    [SerializeField] private AbilityBase eldritchBlastInfo;
    [SerializeField] private AbilityBase dashInfo;
    [SerializeField] private AbilityBase fireDashInfo;

    [SerializeField] private GameObject rollHolder;
    [SerializeField] private GameObject fireRollHolder;

    [SerializeField] private GameObject blastHolder;
    [SerializeField] private GameObject noBlastHolder;

    public Image pSlap;
    public Image eBlast;
    public Image blink;
    public Image fireBlink;

    [Header("Pengu")]
    public Image pengu;

    public Image penguHorns;
    public Image fullPengu;

    public bool isBlastEnabled    = false;
    public bool isFireDashEnabled = false;

    void Start()
    {
        pengu.enabled      = true;
        penguHorns.enabled = false;
        fullPengu.enabled  = false;

        eBlast.fillAmount = 0;
        blink.fillAmount  = 0;
    }


    void Update()
    {
        PowerSlap();
        Blink();
        EldritchBlast();
        FireBlink();
        PenguSettings();
        UiSettings();
    }

    private void UiSettings()
    {
        if (isFireDashEnabled)
        {
            fireRollHolder.SetActive(true);
            rollHolder.SetActive(false);
        }

        if (isBlastEnabled)
        {
            blastHolder.SetActive(true);
            noBlastHolder.SetActive(false);
        }
    }

    void PowerSlap()
    {
        pSlap.fillAmount = powerSlapInfo.CooldownRemainingPct;
    }
    void EldritchBlast()
    {
        eBlast.fillAmount = eldritchBlastInfo.CooldownRemainingPct;
    }

    void Blink()
    {
        blink.fillAmount = dashInfo.CooldownRemainingPct;
    }

    void FireBlink()
    {
        fireBlink.fillAmount = fireDashInfo.CooldownRemainingPct;
    }

    void PenguSettings()
    {
        if (isBlastEnabled)
        {
            pengu.enabled      = false;
            penguHorns.enabled = true;
        }

        if (isFireDashEnabled)
        {
            penguHorns.enabled = false;
            fullPengu.enabled  = true;
        }
    }
}