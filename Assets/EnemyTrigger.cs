using Mechadroids;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    Entrypoint entrypoint;
    bool hasBeenTriggered;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entrypoint = FindFirstObjectByType<Entrypoint>();
        Debug.Log(entrypoint != null ? "entrypoint is not null" : "entrypoint is null");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered");
        if(other.gameObject.layer == 6 && hasBeenTriggered == false) {
            entrypoint.aiEntitiesHandler.Initialize();
            hasBeenTriggered = true;
        }
    }
}
