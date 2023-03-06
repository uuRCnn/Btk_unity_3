using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Soru[] sorular;
    private static List<Soru> cevaplanmamışSorular;
    
    private Soru geçerliSoru;

    [SerializeField] private TextMeshProUGUI soruText;
    [SerializeField] private TextMeshProUGUI dogruCevapText, yanlışCevapText;
    [SerializeField] private GameObject dogruButton, yanlışButton;
    [SerializeField] private GameObject sonuçPaneli;
    [SerializeField] private GameObject yenidenBaşlatButton;
    private SonıucManager _sonıucManager;
    private int dogruAdet, yanlışAdet, toplamPuan; 
    void Start()
    {
        if (cevaplanmamışSorular == null || cevaplanmamışSorular.Count == 0)
        {
            cevaplanmamışSorular = sorular.ToList<Soru>();
        }

        yanlışAdet = 0;
        dogruAdet = 0;
        toplamPuan = 0;
        RastgeleSoruSeç();
         
    }

    void RastgeleSoruSeç()
    {
        yanlışButton.GetComponent<RectTransform>().DOLocalMoveX(184.13f, 1f);
        dogruButton.GetComponent<RectTransform>().DOLocalMoveX(-183.25f, 1f);
        
        int randomSoruIndex = Random.Range(0, cevaplanmamışSorular.Count);
        geçerliSoru = cevaplanmamışSorular[randomSoruIndex];
        
        soruText.text = geçerliSoru.soru;

        if (geçerliSoru.dogrumu)
        {
            dogruCevapText.text = "Dogru cepladınız";
            yanlışCevapText.text = "Yanlış cevapladınız";
        }
        else
        {
            dogruCevapText.text = "Yanlış cepladınız";
            yanlışCevapText.text = "Dogru cevapladınız";
        }
    }

    public void YenidBaşlat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator SorularArasıBekleRoutine()
    {
        cevaplanmamışSorular.Remove(geçerliSoru);
        
        yield return new WaitForSeconds(1f);
        if (cevaplanmamışSorular.Count() <=0)
        {
            sonuçPaneli.SetActive(true);

            _sonıucManager = Object.FindObjectOfType<SonıucManager>();
            _sonıucManager.SonuclarıYazdır(dogruAdet,yanlışAdet,toplamPuan);
        }
        else
        {
            RastgeleSoruSeç();
        }
    }
    
    public void dogruButonaBasıldı()
    {
        if (geçerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlışAdet++;
        }

        yanlışButton.GetComponent<RectTransform>().DOLocalMoveX(1000f, 2f);
        StartCoroutine(SorularArasıBekleRoutine());
    }
    public void yanlışButonaBasıldı()
    {
        if (!geçerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlışAdet++;
        }

        dogruButton.GetComponent<RectTransform>().DOLocalMoveX(-1000f, 2f);
        StartCoroutine(SorularArasıBekleRoutine());
    }
}
