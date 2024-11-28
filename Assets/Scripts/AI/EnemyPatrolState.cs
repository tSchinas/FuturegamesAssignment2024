using Unity.VisualScripting;
using UnityEngine;

namespace Mechadroids {
    internal class EnemyPatrolState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private readonly PlayerEntityHandler playerEntityHandler;

        public int currentPatrolIndex { get; private set; }

        //EnemySettings enemySettings;



        public EnemyPatrolState(IEntityHandler entityHandler, EnemyReference enemyReference, PlayerEntityHandler playerEntityHandler) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerEntityHandler = playerEntityHandler;
            this.enemyReference.enemySettings = enemyReference.enemySettings;
        }
        
        public void Enter() {
            Debug.Log(enemyReference.gameObject.name + " entered patrol state!");
            SetNextPatrolDestination();
        }

        public void LogicUpdate() {
            MoveTowardsPatrolPoint();
            
            IsPlayerInDetectionRange();
            if(IsPlayerInDetectionRange() == true) { 
                Debug.Log(enemyReference.gameObject.name + " detected player!");
                TransitionToChaseState();
            }
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

        public void TransitionToChaseState() {
            Exit();
            entityHandler.EntityState = new EnemyChaseState(entityHandler, enemyReference, playerEntityHandler);
            entityHandler.EntityState.Enter();
        }

        public void DetectPlayer() {

        }

        private bool IsPlayerInDetectionRange() {
            if(playerEntityHandler.playerReference == null) return false;
            float distance = Vector3.Distance(enemyReference.transform.position, playerEntityHandler.playerReference.transform.position);
            return distance <= enemyReference.enemySettings.enemy.detectionRadius;  
        }
        private void SetNextPatrolDestination() {
            if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
            currentPatrolIndex %= enemyReference.enemySettings.routeSettings.routePoints.Length;
        }

        private void MoveTowardsPatrolPoint() {
            if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;

            Vector3 targetPoint = enemyReference.enemySettings.routeSettings.routePoints[currentPatrolIndex];
            targetPoint.y = enemyReference.transform.position.y;
            Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;

            // Move towards the target point
            enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;

            // Rotate towards the target point
            RotateTowards(direction);

            // Check if the enemy has reached the patrol point
            if(Vector3.Distance(enemyReference.transform.position, targetPoint) <= 0.1f) {
                currentPatrolIndex = (currentPatrolIndex + 1) % enemyReference.enemySettings.routeSettings.routePoints.Length;
                SetNextPatrolDestination();
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
