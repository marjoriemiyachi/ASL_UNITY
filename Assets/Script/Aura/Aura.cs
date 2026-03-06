using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField] VFXColeta efeito;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Coletar();
        }
    }
    public void Coletar()
    {
        // tocar efeito ao coletar
        efeito.TocarEfeito(transform.position);
        ContadorDeAura.Instancia.AdicionarAura(1);//adicionar aura
        Destroy(gameObject);//destruir após coletado
    }
}