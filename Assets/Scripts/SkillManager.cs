using System;
using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Abilities;
using UnityEngine;

namespace DangerousPenguin
{
    public class SkillManager : MonoBehaviour
    {
        [SerializeField] private PlayerCombatController player;
        [SerializeField] private global::Abilities      ui;

        public void ReceiveSkill(AbilityBase ability)
        {
            switch (ability.Ability)
            {
                case AttackType.FireDash:
                    ReceiveFireDash();
                    break;
                case AttackType.FireBall:
                    ReceiveFireBall();
                    break;
            }
        }

        private void ReceiveFireBall()
        {
            player.ActivateFireBall();
            ui.isBlastEnabled = true;
        }

        private void ReceiveFireDash()
        {
            player.ActivateFireDash();
            ui.isFireDashEnabled = true;
        }
    }
}
