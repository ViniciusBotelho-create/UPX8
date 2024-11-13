using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject startObject;                   // Primeiro objeto
    public List<GameObject> endObjects;              // Lista de objetos finais poss�veis
    public List<GameObject> arrowPrefabs;            // Lista de prefabs de seta
    private GameObject arrowInstance;                // Inst�ncia da seta
    private GameObject currentEndObject;             // Objeto final atual para a seta

    void Update()
    {
        if (startObject != null && currentEndObject != null && arrowInstance != null)
        {
            // Pega as posi��es do startObject e do currentEndObject
            Vector3 startPos = startObject.transform.position;
            Vector3 endPos = currentEndObject.transform.position;

            // Ajusta a posi��o da seta para o meio dos dois objetos
            arrowInstance.transform.position = (startPos + endPos) / 2;

            // Calcula a rota��o da seta para apontar do startObject ao currentEndObject
            Vector3 direction = (endPos - startPos).normalized;
            arrowInstance.transform.rotation = Quaternion.LookRotation(direction);

            // Ajusta o tamanho da seta para cobrir a dist�ncia entre os objetos
            float distance = Vector3.Distance(startPos, endPos);
            arrowInstance.transform.localScale = new Vector3(arrowInstance.transform.localScale.x, arrowInstance.transform.localScale.y, distance);
        }
    }

    // Fun��o p�blica que cria ou atualiza a seta usando um �ndice combinado
    public void CreateArrowWithCombinedIndex(int combinedIndex)
    {
        // Calcula os �ndices do endObject e do prefab com base no �ndice combinado
        int endObjectIndex = combinedIndex % endObjects.Count;
        int arrowPrefabIndex = combinedIndex / endObjects.Count;

        // Verifica se os �ndices est�o dentro do intervalo
        if (endObjectIndex >= 0 && endObjectIndex < endObjects.Count && arrowPrefabIndex >= 0 && arrowPrefabIndex < arrowPrefabs.Count)
        {
            // Define o novo objeto final
            currentEndObject = endObjects[endObjectIndex];

            // Destroi a seta anterior, se existir
            if (arrowInstance != null)
            {
                Destroy(arrowInstance);
            }

            // Instancia a nova seta com o prefab selecionado
            arrowInstance = Instantiate(arrowPrefabs[arrowPrefabIndex]);

            // Atualiza a posi��o e rota��o da seta para o novo endObject
            Update();
        }
    }
}
