using UnityEngine;

public class Aura : MonoBehaviour
{
   
    
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
        VFXColeta.Instancia.TocarEfeito(transform.position);
        ContadorDeAura.Instancia.AdicionarAura(1);//adicionar aura
        Destroy(gameObject);//destruir após coletado
    }
}