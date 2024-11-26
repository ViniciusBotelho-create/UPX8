using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARGPS : MonoBehaviour
{
    [Header("AR Settings")]
    public GameObject arrowPrefab; // Prefab da seta
    public Transform imageTarget; // Ponto inicial, vinculado à Image Target detectada
    public float arrowSpacing = 2f; // Espaçamento entre as setas

    [Header("Destinations and Buttons")]
    public List<Transform> destinations; // Lista de destinos
    public List<Button> navigationButtons; // Lista de botões de navegação

    private List<GameObject> arrows = new List<GameObject>();
    private bool navigationStarted = false;
    private Transform currentDestination;

    void Start()
    {
        if (arrowPrefab == null || imageTarget == null || destinations.Count == 0 || navigationButtons.Count != destinations.Count)
        {
            Debug.LogError("Certifique-se de configurar o prefab de seta, a Image Target, os destinos e os botões no inspetor.");
            return;
        }

        // Associa cada botão ao destino correspondente
        for (int i = 0; i < navigationButtons.Count; i++)
        {
            int index = i; // Evita problemas de referência na closure
            navigationButtons[i].onClick.AddListener(() => StartNavigationToDestination(destinations[index]));
        }
    }

    void StartNavigationToDestination(Transform destination)
    {
        // Define o destino atual e inicia a navegação
        currentDestination = destination;
        navigationStarted = true;

        // Gera as setas para o destino selecionado
        GeneratePathArrows();
        Debug.Log($"Navegação iniciada para: {destination.name}");
    }

    void GeneratePathArrows()
    {
        // Limpa setas existentes
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();

        if (currentDestination == null)
        {
            Debug.LogWarning("Destino atual não definido.");
            return;
        }

        // Calcula o caminho entre a Image Target e o destino atual
        Vector3 start = imageTarget.position;
        Vector3 end = currentDestination.position;
        Vector3 direction = (end - start).normalized;

        float distance = Vector3.Distance(start, end);
        int arrowCount = Mathf.FloorToInt(distance / arrowSpacing);

        for (int i = 0; i < arrowCount; i++)
        {
            Vector3 arrowPosition = start + direction * (i * arrowSpacing);
            Quaternion arrowRotation = Quaternion.LookRotation(direction);

            GameObject arrow = Instantiate(arrowPrefab, arrowPosition, arrowRotation);
            arrows.Add(arrow);
        }
    }

    void Update()
    {
        // Atualiza o caminho em tempo real se a Image Target se mover
        if (navigationStarted && imageTarget != null)
        {
            GeneratePathArrows();
        }
    }

    void OnDrawGizmos()
    {
        // Visualiza os caminhos no Editor
        if (imageTarget != null && destinations != null)
        {
            Gizmos.color = Color.green;

            foreach (Transform destination in destinations)
            {
                Gizmos.DrawLine(imageTarget.position, destination.position);
            }
        }
    }
}
