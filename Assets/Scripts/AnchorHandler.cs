using UnityEngine;
using Vuforia;

public class AnchorHandler : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour == null)
        {
            Debug.LogError("ObserverBehaviour n�o encontrado. Certifique-se de que este script est� anexado ao ImageTarget.");
            return;
        }

        // Associa o evento de mudan�a de status
        observerBehaviour.OnTargetStatusChanged += OnObserverStatusChanged;
    }

    private void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnObserverStatusChanged;
        }
    }

    private void OnObserverStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            AnchorObjects();
        }
    }

    private void AnchorObjects()
    {
        foreach (Transform child in transform)
        {
            // Salva a posi��o e rota��o absolutas no mundo
            Vector3 worldPosition = child.position;
            Quaternion worldRotation = child.rotation;

            // Remove o objeto da hierarquia do ImageTarget
            child.SetParent(null);

            // Define a posi��o e rota��o absolutas
            child.position = worldPosition;
            child.rotation = worldRotation;
        }

        // Desativa o rastreamento para este ImageTarget
        observerBehaviour.enabled = false;

        Debug.Log("Objetos ancorados no mundo e rastreamento do ImageTarget desativado.");
    }
}
