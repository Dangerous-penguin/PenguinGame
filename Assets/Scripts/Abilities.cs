using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{

    [Header("Eldritch Blast")]
    public Image eBlast;
    public float cooldownBlast = 5;
    bool isCooldownBlast = false;
    public KeyCode blastKey;


    [Header("Blink")]
    public Image blink;
    public float cooldownBlink = 3;
    bool isCooldownBlink = false;
    public KeyCode blinkKey;


    [Header("Fire!")]
    public Image firePillar;
    public float cooldownFirePillar = 2;
    bool isCooldownFirePillar = false;
    public KeyCode dashKey;


    [Header("Pengu")]
    public Image pengu;
    public Image penguHorns;
    public Image fullPengu;

    public bool isBlastEnabled = false;
    public bool isFirePillarEnabled = false;

    void Start()
    {
        pengu.enabled = true;
        penguHorns.enabled = false;
        fullPengu.enabled = false;

        eBlast.fillAmount = 0;
        blink.fillAmount = 0;
        firePillar.fillAmount = 0;
    }


    void Update()
    {
        EldritchBlast();
        Blink();
        FirePillar();
        PenguSettings();
    }

    void EldritchBlast(){
        if(Input.GetKey(blastKey) && isCooldownBlast == false){
            isCooldownBlast = true;
            Debug.Log("blast");
            eBlast.fillAmount = 1;
        }
        if(isCooldownBlast){
            eBlast.fillAmount -= 1/cooldownBlast * Time.deltaTime;
            if(eBlast.fillAmount <= 0){
                eBlast.fillAmount = 0;
                isCooldownBlast = false;
            }
        }
    }

    void Blink (){
        if(Input.GetKey(blinkKey) && isCooldownBlink == false){
            isCooldownBlink = true;
            blink.fillAmount = 1;
        }
        if(isCooldownBlink){
            blink.fillAmount -= 1/cooldownBlink * Time.deltaTime;
            if(blink.fillAmount <= 0){
                blink.fillAmount = 0;
                isCooldownBlink = false;
            }
        }
    }

    void FirePillar (){
        if(Input.GetKey(dashKey) && isCooldownFirePillar == false){
            isCooldownFirePillar = true;
            firePillar.fillAmount = 1;
        }
        if(isCooldownFirePillar){
            firePillar.fillAmount -= 1/cooldownFirePillar * Time.deltaTime;
            if(firePillar.fillAmount <= 0){
                firePillar.fillAmount = 0;
                isCooldownFirePillar = false;
            }
        }
    }

    void PenguSettings(){
        if(isBlastEnabled){
            pengu.enabled = false;
            penguHorns.enabled = true;
        }
        if(isFirePillarEnabled){
            penguHorns.enabled = false;
            fullPengu.enabled = true;
        }
    }
}
