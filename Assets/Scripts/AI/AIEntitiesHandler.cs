using System.Collections.Generic;
using UnityEngine;


namespace Mechadroids {
    // Keeps the state for all AI activity
    public class AIEntitiesHandler {
        private readonly AISettings aiSettings;
        private readonly Transform parentHolder;
        private readonly PlayerEntityHandler playerEntityHandler;
        

        private Dictionary<int, EnemyEntityHandler> EnemyEntityHandlers { get; } = new();

        public AIEntitiesHandler(AISettings aiSettings, Transform parentHolder, PlayerEntityHandler playerEntityHandler) {
            this.aiSettings = aiSettings;
            this.parentHolder = parentHolder;
            this.playerEntityHandler = playerEntityHandler;
            
        }

        public void Initialize() {
            // initialize all enemies here
            foreach(EnemyGroup enemy in aiSettings.enemiesToSpawn) {
                for(int i = 0; i < enemy.enemyCount; i++) {
                    EnemyEntityHandler enemyEntityHandler = new(enemy.enemySettings, parentHolder, playerEntityHandler);
                    enemyEntityHandler.Initialize();
                    EnemyEntityHandlers.TryAdd(i, enemyEntityHandler);
                }
            }
        }

        public void Tick() {
            // tick all the enemies
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.Tick();
            }
        }

        public void PhysicsTick() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.PhysicsTick();
            }
        }

        public void Dispose() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.Dispose();
            }
            EnemyEntityHandlers.Clear();
        }
    }
}
