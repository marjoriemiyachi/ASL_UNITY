using UnityEngine;
using UnityEngine.UI;

public class EnemyDebugUI : MonoBehaviour
{
    [SerializeField] private EnemyAI enemy; // referência ao inimigo
    [SerializeField] private Image stateIndicator; // imagem da UI

    [SerializeField] private Color patrolColor = Color.green;
    [SerializeField] private Color chaseColor = Color.red;
    [SerializeField] private Color investigateColor = Color.yellow;

    void Update()
    {
        if (enemy == null || stateIndicator == null) return;

        // Atualiza a cor de acordo com o estado atual do inimigo
        switch (enemy.currentState)
        {
            case EnemyAI.EnemyState.Patrol:
                stateIndicator.color = patrolColor;
                break;
           

            case EnemyAI.EnemyState.Chase:
                stateIndicator.color = chaseColor;
                break;
        }
    }
}