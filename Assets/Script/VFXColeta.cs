using UnityEngine;

public class VFXColeta : MonoBehaviour
{//criar um prefab que será usar no script aura e entre outros

    
    [SerializeField] GameObject efeito;

    public void TocarEfeito(Vector3 posicao)
    {
        Instantiate(efeito, posicao, transform.rotation);
    }

}
