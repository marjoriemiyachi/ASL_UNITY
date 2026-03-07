using UnityEngine;

public class VFXColeta: MonoBehaviour
{
    //singleton
  
    public static VFXColeta Instancia;//valoriavel da propria classe e acessivel a todo mundo
    [SerializeField] GameObject efeitoPrefab;//fazer um prefab
    void Awake() //antes do start
    {
        if (Instancia != null && Instancia != this)//diferente de nulo e ela não sou eu
        {
            Destroy(this.gameObject);//então irá destruir
        }
        else
        {
            Instancia = this;//eu sou
            //entrar na singleton Unity
        }
    }

    public void TocarEfeito(Vector3 pos)
    {
        GameObject fx = Instantiate(efeitoPrefab, pos, Quaternion.identity);
        Destroy(fx, 2f);
    }
}