using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Abilities;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField] private AbilityBase eldritchBlastInfo;
    [SerializeField] private AbilityBase dashInfo;

    public Image eBlast;
    public Image blink;

    [Header("Pengu")]
    public Image pengu;

    public Image penguHorns;
    public Image fullPengu;

    public bool isBlastEnabled      = false;
    public bool isFireDashEnabled = false;

    void Start()
    {
        pengu.enabled      = true;
        penguHorns.enabled = false;
        fullPengu.enabled  = false;

        eBlast.fillAmount     = 0;
        blink.fillAmount      = 0;
    }


    void Update()
    {
        EldritchBlast();
        Blink();
        PenguSettings();
    }

    void EldritchBlast()
    {
        eBlast.fillAmount = eldritchBlastInfo.CooldownRemainingPct;
    }

    void Blink()
    {
        blink.fillAmount = dashInfo.CooldownRemainingPct;
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