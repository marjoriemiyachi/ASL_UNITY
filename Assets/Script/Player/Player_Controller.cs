using UnityEngine;

public class Player_Controller : MonoBehaviour
{
   [SerializeField] float speed = 50;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
       
    }



    void Update()
    {
    }
    private void FixedUpdate()
    {
        //chamar função 
        Movimentar();
    }
    void Movimentar()
    {
        Vector3 dir;
        float dirZ = Input.GetAxis("Vertical");
        float dirX = Input.GetAxis("Horizontal");
        dir= new Vector3(dirX,0,dirZ);
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

    }
}
