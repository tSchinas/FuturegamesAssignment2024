using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Mechadroids {
    // Route points for the AI
    [CreateAssetMenu(menuName = "Mechadroids/RouteSettings", fileName = "Route", order = 0)]
    public class Route : ScriptableObject {
        public int routeId;
        public Vector3 [] routePoints;
        public bool showGizmos = false;

        
    }
}
