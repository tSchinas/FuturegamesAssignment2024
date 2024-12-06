using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
namespace Mechadroids {
    [CustomEditor(typeof(Route))]
    public class RouteVisualizer : Editor {

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            Route routeData = (Route)target;

            DrawDefaultInspector();

            if(GUILayout.Button(routeData.showGizmos ? "Hide Gizmos" : "Show Gizmos"))
            {
                routeData.showGizmos = !routeData.showGizmos;

                //SceneView.RepaintAll();
            }
        }
        private void OnEnable() {
            SceneView.duringSceneGui += OnSceneUpdate;
        }

        private void OnDisable() {
            SceneView.duringSceneGui -= OnSceneUpdate;
        }

        private void OnSceneUpdate(SceneView sceneView) {
            Route routeData = (Route)target;
            Handles.color = Color.yellow;
            int rpIndex;
            string rpLabel;
            if(routeData.showGizmos) {
                for(int i = 0; i < routeData.routePoints.Length; ++i) {
                    Handles.DrawWireCube(routeData.routePoints[i], new Vector3(1.5f, 1.5f, 1.5f));
                    rpIndex = i + 1;
                    rpLabel = rpIndex.ToString();
                    Handles.Label(routeData.routePoints[i] + new Vector3(0, 10, 0), rpLabel);
                }
                for(int l = 0; l < routeData.routePoints.Length - 1; ++l) {

                    Handles.DrawLine(routeData.routePoints[l], routeData.routePoints[l + 1]);

                }
                if(routeData.routePoints.Length > 2) {
                    Handles.DrawLine(routeData.routePoints[^1], routeData.routePoints[0]);
                }


            }
        }

    }


}
#endif
