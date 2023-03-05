using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Soru[] sorular;
    private static List<Soru> cevaplanmamışSorular;
    
    private Soru geçerliSoru;

    [SerializeField] private TextMeshProUGUI soruText;
    void Start()
    {
        if (cevaplanmamışSorular == null || cevaplanmamışSorular.Count == 0)
        {
            cevaplanmamışSorular = sorular.ToList<Soru>();
        }

        RastgeleSoruSeç();
         
    }

    void RastgeleSoruSeç()
    {
        int randomSoruIndex = Random.Range(0, cevaplanmamışSorular.Count);
        geçerliSoru = cevaplanmamışSorular[randomSoruIndex];
        
        soruText.text = geçerliSoru.soru;
    }

    IEnumerator SorularArasıBekleRoutine()
    {
        cevaplanmamışSorular.Remove(geçerliSoru);
        
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void dogruButonaBasıldı()
    {
        if (geçerliSoru.dogrumu)
        {
            Debug.Log("dogru cevapladınız.");
        }
        else
        {
            Debug.Log("yanlış cevapladınız.");
        }
        StartCoroutine(SorularArasıBekleRoutine());
    }
    public void yanlışButonaBasıldı()
    {
        if (!geçerliSoru.dogrumu)
        {
            Debug.Log("dogru cevapladınız.");
        }
        else
        {
            Debug.Log("yanlış cevapladınız.");
        } 
        StartCoroutine(SorularArasıBekleRoutine());
    }
}
