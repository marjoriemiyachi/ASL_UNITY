using TMPro;
using UnityEngine;

public class ContadorDeAura : MonoBehaviour
{
    [SerializeField] TMP_Text texto;
    private int quantidade = 0; // valor interno do contador

    #region Singleton
    //variavel static 
    //vários inimigos tem vida própria, ataque , proprio, se muda em um muda tudo 
    //é um padrão
    //variavel da propria classe

    public static ContadorDeAura Instancia;//valoriavel da propria classe e acessivel a todo mundo
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
    #endregion
    private void Start()
    {
        AtualizarTexto(); // mostra o valor inicial
    }

    // Adiciona aura
    public void AdicionarAura(int valor = 1)
    {
        quantidade += valor; // incrementa
        AtualizarTexto();
    }

    //public void FazAlgumacoisa()
    /* public void MudarValorTexto(string valor, Color cor)
     {
         texto.text = valor;
         texto.color = cor;

     }*/
    // Atualiza o texto na UI
    private void AtualizarTexto()
    {
        texto.text = "Aura:" + quantidade + "/10" ;
        texto.color = Color.green; // ou outra cor se quiser
    }
}
