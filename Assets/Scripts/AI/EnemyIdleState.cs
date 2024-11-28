using System.Collections;
using UnityEngine;

namespace Mechadroids {
    public class EnemyIdleState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private readonly PlayerEntityHandler playerEntityHandler;
        private float idleDuration = 0.1f;
        private float idleTimer;
        //private bool timerHandled;

        public EnemyIdleState(IEntityHandler entityHandler, EnemyReference enemyReference, PlayerEntityHandler playerEntityHandler) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerEntityHandler = playerEntityHandler;
        }

        public void Enter(MonoBehaviour mono) {
            idleTimer = 0f;
            //timerHandled = false;
            // Optionally set idle animation
            Debug.Log(enemyReference.gameObject.name + " entered idle state!");
            
        }

        public void LogicUpdate() {
            idleTimer += Time.deltaTime;
            Debug.Log(enemyReference.gameObject.name + "'s idle timer has " + idleTimer +"s remaining"!);
            if(idleTimer >= idleDuration) {
                TransitionToPatrolState();
            }
        }

        

        public void PhysicsUpdate() {
            // No physics updates needed in idle state
        }

        public void Exit() {
            // Cleanup if necessary
            
        }

        private void TransitionToPatrolState() {
            Exit();
            entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference, playerEntityHandler);
            entityHandler.EntityState.Enter();
        }
    }

}
