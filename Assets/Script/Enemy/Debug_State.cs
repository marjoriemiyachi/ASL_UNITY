using UnityEngine;
using UnityEngine.UI;

public class EnemyDebugUI : MonoBehaviour
{//Feedback no game para ver se EnemyAi está funcionando
    [SerializeField] private EnemyAI enemy; // referência ao inimigo
    [SerializeField] private Image stateIndicator; // imagem da UI

    [SerializeField] private Color patrolColor = Color.green;
    [SerializeField] private Color chaseColor = Color.red;
    [SerializeField] private Color investigateColor = Color.yellow;

    //no start o icone de detecção deve inicializar como falso
    void Update()
    {
        if (enemy == null || stateIndicator == null) return;

        // Atualiza a cor de acordo com o estado atual do inimigo
        switch (enemy.currentState)
        {
            case EnemyAI.EnemyState.Patrol:
                stateIndicator.color = patrolColor;
                //Não mostrar indicador
                break;
           

            case EnemyAI.EnemyState.Chase:
                stateIndicator.color = chaseColor;
                //mostrar indicador atrelado ao player
                break;
        }
    }
}