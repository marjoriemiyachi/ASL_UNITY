using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
   [SerializeField] float detectRange = 10f;
    [Range(0, 180)]
   //[SerializeField] float detectAngle = 90f;


    [Header("References")]
    public Transform player;
    public LayerMask playerLayer; // assign in inspector
    public TMP_Text rangeText;
    public TMP_Text hiddenText;
    //public TMP_Text angleText;
    public TMP_Text detectedText;

    public bool PlayerDetected { get; private set; }

    bool isInRange;
   // bool isInAngle;
    bool isVisible;

    void Update()
    {
        CheckRange();
      //  CheckAngle();
        CheckVisibility();
        FinalDetection();
     
    }

    void CheckRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectRange)
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

  /*  void CheckAngle()
    {
        Vector3 direction = (player.position - transform.position);
            direction.y = 0f;
        direction.Normalize();
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();
        float angle = Vector3.Angle(forward, direction);

        if (angle <= detectAngle / 2f)
        {
            isInAngle = true;
            angleText.text = "No Ângulo";
            angleText.color = Color.red;
        }
        else
        {
            isInAngle = false;
            angleText.text = "Fora do Campo";
            angleText.color = Color.green;
        }
    }*/

    void CheckVisibility()
    {//anteriormente da outra forma botando a posição do player , o raycast saia na diagonal em direção ao player
     // Altura dos olhos do inimigo
        Vector3 origin = transform.position + Vector3.up * 1.6f;
        //ver tamanho do player

        // Altura central do player
        Collider playerCollider = player.GetComponentInChildren<Collider>();
        Vector3 target = playerCollider.bounds.center;

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
        PlayerDetected = isInRange && isVisible;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

       // Vector3 leftLimit = Quaternion.Euler(0, -detectAngle / 2, 0) * transform.forward;
        //Vector3 rightLimit = Quaternion.Euler(0, detectAngle / 2, 0) * transform.forward;

       // Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position, leftLimit * detectRange);
        //Gizmos.DrawRay(transform.position, rightLimit * detectRange);
    }
}