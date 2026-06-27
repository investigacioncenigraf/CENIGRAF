using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MENUSELECCIONPERSONAJE : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;
    private GameManager_personajes gameManager;
    private void Start()
    {
        gameManager=GameManager_personajes.Instance;
        index=PlayerPrefs.GetInt("JugadorIndex");
        if (index>gameManager.personajes.Count-1)
        {
            index=0;
        }
    }
    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite =gameManager.personajes[index].imagen;
        nombre.text= gameManager.personajes[index].nombre;
    }
    public void SiguientePersonaje()
    {
        if(index==gameManager.personajes.Count-1)
        {
            index=0;
        }
        else
        {
            index+=1;
        }
        CambiarPantalla();
        
    }    
    public void AnteriorPersonaje()
    {
        if(index==0)
        {
            index=gameManager.personajes.Count-1;
        }
        else
        {
            index-=1;
        }
        CambiarPantalla();
    }





}
