using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    private void Awake()
    {
        // Mantém este objeto quando a cena mudar
        DontDestroyOnLoad(gameObject);

        // Verifica se já existe outro objeto com o mesmo tipo
        if (FindObjectsByType<DontDestroyOnLoadObject>(FindObjectsSortMode.None).Length > 1)
        {
            // Se houver mais de um, destrói este objeto
            Destroy(gameObject);
        }
    }
}
