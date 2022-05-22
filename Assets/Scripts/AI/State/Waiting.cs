using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    public class Waiting : IState
    {

        private MeeleMinionBB _blackBoard;

        private float _waitTimer;

        public Waiting(MeeleMinionBB blackBoard)
        {
            _blackBoard = blackBoard;
        }

        public void SateUpdate()
        {
            //CheckPlayer
            if(_waitTimer >= 0)
            {
                _waitTimer -= Time.deltaTime;
            }
            else
            {
                ChangeState(_blackBoard.patrolState);
                return;
            }
        }

        public void OnStateEnter()
        {
            EnemySO enemySO = _blackBoard.enemySO;
            _waitTimer = Random.Range(enemySO.waitTimerMin,enemySO.waitTimerMax);
        }

        public void OnStateExit()
        {
            _waitTimer = 0;
        }

        public void ChangeState(IState newState)
        {
            _blackBoard.fsm.ChangeState(newState);
        }

    }
}
