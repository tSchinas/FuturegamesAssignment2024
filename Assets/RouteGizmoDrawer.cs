using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Mechadroids {
    public class RouteGizmoDrawer : MonoBehaviour {
        [SerializeField] private Route routeData;

        private void OnDrawGizmos() {
            if(routeData == null || !routeData.showGizmos) {
                Debug.Log(routeData == null ? "Route data not found":"");
                return;
            }

            Gizmos.color = Color.red;

            foreach(Vector3 v in routeData.routePoints) {
                if(v != null) {
                    Gizmos.DrawSphere(v, 1f); // Draw a sphere at the transform's position
                    Gizmos.DrawLine(v, v + Vector3.up); // Example line
                    Debug.Log(routeData.routePoints.Length);
                }
            }
        }
    }
}
