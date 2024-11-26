using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugToggleModel : MonoBehaviour
{
    [Header("Settings")]
    public List<GameObject> baseModelObjects; // Lista dos objetos do modelo base
    public Button toggleButton; // Bot�o de debug

    private bool isModelVisible = true; // Estado atual do modelo (vis�vel ou invis�vel)

    void Start()
    {
        if (toggleButton == null)
        {
            Debug.LogError("O bot�o de debug n�o foi atribu�do no inspetor.");
            return;
        }

        // Adiciona o evento ao bot�o
        toggleButton.onClick.AddListener(ToggleModelVisibility);
    }

    void ToggleModelVisibility()
    {
        // Inverte o estado da visibilidade
        isModelVisible = !isModelVisible;

        foreach (GameObject obj in baseModelObjects)
        {
            if (obj != null)
            {
                // Ativa ou desativa o Mesh Renderer
                MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = isModelVisible;
                }
            }
        }

        Debug.Log($"Modelo base agora est� {(isModelVisible ? "vis�vel" : "invis�vel")}");
    }
}
