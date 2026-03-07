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
        MovimentarNormal();
    }
    void MovimentarNormal()
    {
        Vector3 dir;
        float dirZ = Input.GetAxis("Vertical");
        float dirX = Input.GetAxis("Horizontal");
        dir= new Vector3(dirX,0,dirZ);
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

    }
}
/*
 * Quaternion vai adicionando numa rotaçãode 360 e depois voltam para um tempo em comum e acumula 
 * então existe uma quarta variavel para armazenar visto que nunca teremos 361
 * Euler angles são vector 3 com angulação
 * 
 */