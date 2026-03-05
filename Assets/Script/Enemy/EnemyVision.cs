using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    //adicionar um script de detecção. Se o jogador estiver perto do inimigo começa o som de piseiro
    [Header("Vision Settings")]
    [SerializeField] float detectRange = 10f;
    

    [Header("References")]
    public Transform player;
    public LayerMask playerLayer; // assign in inspector
    public TMP_Text rangeText;
    public TMP_Text hiddenText;
    //public TMP_Text angleText;
    public TMP_Text detectedText;

    public bool PlayerDetected { get; private set; }

    bool isInRange;

    bool isVisible;

    void Update()
    {
        CheckRange();//se o player estiver na area
        
        CheckVisibility();//detecta parede e detecta o player
        FinalDetection();//condições para realizar, no caso deve ser OU

    }

    void CheckRange()
    {
        //transform.forward  considera a rotação
        //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Transform-forward.html
        float forwardOffset = detectRange * 0.5f; // multiplica para que o raio da esfera pegue a parte da frente do inimigo. 0.5f metade do corpo

        Vector3 origin = transform.position+ Vector3.up * 1.6f      // altura dos olhos
                         + transform.forward * forwardOffset; // frente do inimigo

        bool inRange = Physics.CheckSphere(origin, detectRange * 0.5f, playerLayer);

        if (inRange)
        {
            isInRange = true;
            rangeText.text = "Socorro";
            rangeText.color = Color.red;
        }
        else
        {
            isInRange = false;
            rangeText.text = "De boas";
            rangeText.color = Color.green;
        }
}

    void CheckVisibility()
    {//anteriormente da outra forma botando a posição do player , o raycast saia na diagonal em direção ao player
     // Altura dos olhos do inimigo
        Vector3 origin = transform.position + Vector3.up * 1.6f;
        //ver tamanho do player

        // Altura central do player
        Collider playerCollider = player.GetComponentInChildren<Collider>();//verificar os filhos
        Vector3 target = playerCollider.bounds.center; //usar de ponto de referencia

        //https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Bounds.html 


        // Direção horizontal única
        Vector3 direction = target - origin;
        direction.Normalize();


        RaycastHit hit;


        if (Physics.Raycast(origin, direction, out hit, detectRange))
        {
            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                isVisible = true;
                hiddenText.text = "Tá vendo ele!";
                hiddenText.color = Color.red;
            }
            else
            {
                isVisible = false;
                hiddenText.text = "Obstruído";
                hiddenText.color = Color.green;
            }
        }
        Debug.DrawRay(origin, direction * detectRange, Color.red);

    }

    void FinalDetection()
    {
        if (isInRange && isVisible)
        {
            detectedText.text = "PLAYER DETECTADO";
            detectedText.color = Color.red;
        }
        else
        {
            detectedText.text = "Player Não Detectado";
            detectedText.color = Color.green;
        }
    }
    void LateUpdate()
    {
        PlayerDetected = isInRange || isVisible;//OU - priorizando 
        //diminuir tamanho do range
    }
    void OnDrawGizmosSelected()
    {
        float forwardOffset = detectRange * 0.5f;//desenhar o gizmos lembrando das refs utilizadas anteriormente

        Vector3 origin = transform.position
                         + Vector3.up * 1.6f
                         + transform.forward * forwardOffset;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, detectRange * 0.5f);
    }
}