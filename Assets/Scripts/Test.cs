using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test: MonoBehaviour
{
    public GameObject startObject;  // Primeiro objeto
    public GameObject endObject;    // Segundo objeto
    public GameObject arrowPrefab;  // Prefab da seta

    private GameObject arrowInstance; // Instância da seta

    void Start()
    {
        // Instancia a seta
        arrowInstance = Instantiate(arrowPrefab);
    }

    void Update()
    {
        if (startObject != null && endObject != null && arrowInstance != null)
        {
            // Pega as posições dos dois objetos
            Vector3 startPos = startObject.transform.position;
            Vector3 endPos = endObject.transform.position;

            // Ajusta a posição da seta para o meio dos dois objetos
            arrowInstance.transform.position = (startPos + endPos) / 2;

            // Calcula a rotação da seta para apontar do startObject ao endObject
            Vector3 direction = (endPos - startPos).normalized;
            arrowInstance.transform.rotation = Quaternion.LookRotation(direction);

            // Ajusta o tamanho da seta para cobrir a distância entre os objetos
            float distance = Vector3.Distance(startPos, endPos);
            arrowInstance.transform.localScale = new Vector3(arrowInstance.transform.localScale.x, arrowInstance.transform.localScale.y, distance);
        }
    }
}
