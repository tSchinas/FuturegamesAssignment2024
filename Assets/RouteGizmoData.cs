using UnityEngine;
using UnityEditor;

namespace Mechadroids {
    [CustomEditor(typeof(Route))]
    public class RouteEditor : Editor {
        
        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            Route routeData = (Route)target;

            DrawDefaultInspector();

            if(GUILayout.Button(routeData.showGizmos ? "Hide Gizmos" : "Show Gizmos")) {
                routeData.showGizmos = !routeData.showGizmos;
               
                SceneView.RepaintAll();
            }
        }

    }
}
