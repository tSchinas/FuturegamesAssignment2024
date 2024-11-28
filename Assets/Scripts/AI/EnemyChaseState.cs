
using UnityEngine;

namespace Mechadroids {
    internal class EnemyChaseState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private readonly PlayerEntityHandler playerEntityHandler;

        public EnemyChaseState(IEntityHandler entityHandler, EnemyReference enemyReference, PlayerEntityHandler playerEntityHandler) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerEntityHandler = playerEntityHandler;
        }

        public void Enter() {
            Debug.Log(enemyReference.gameObject.name + " entered chase state!");
        }

        public void LogicUpdate() {
            MoveTowardsPlayer();
        }

        public void PhysicsUpdate() {

        }

        public void Exit() {

        }

        public void TransitionToIdleState() {
            Exit();
            entityHandler.EntityState = new EnemyIdleState(entityHandler, enemyReference, playerEntityHandler);
            entityHandler.EntityState.Enter();
        }

        public void TransitionToPatrolState() {
            Exit();
            entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference, playerEntityHandler);
            entityHandler.EntityState.Enter();
        }
        private void MoveTowardsPlayer() {
            if(playerEntityHandler.playerReference == null) return;

            Vector3 targetPoint = playerEntityHandler.playerReference.gameObject.transform.position;
            targetPoint.y = enemyReference.transform.position.y;
            Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;

            // Move towards the target point
            enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;

            // Rotate towards the target point
            RotateTowards(direction);

            // Check if the enemy is too far away from the player
            if(Vector3.Distance(enemyReference.transform.position, targetPoint) >= enemyReference.enemySettings.enemy.detectionRadius + enemyReference.enemySettings.enemy.attackRange) {
                Debug.Log(enemyReference.gameObject.name + " lost sight of player!");
                TransitionToIdleState();
            }
        }
        private void RotateTowards(Vector3 direction) {
            if(direction.magnitude == 0) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyReference.transform.rotation = Quaternion.Slerp(
                enemyReference.transform.rotation,
                targetRotation,
                enemyReference.enemySettings.enemy.patrolRotationSpeed * Time.deltaTime
            );
        }
    }
}
