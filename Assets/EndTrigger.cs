using Mechadroids;
using Unity.VisualScripting;
using UnityEngine;

public class EndTrigger : MonoBehaviour {
    private bool hasBeenTriggered = false;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered");
        if(other.gameObject.layer == 6 && hasBeenTriggered == false) {
            hasBeenTriggered = true;
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        }
    }
}
