using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SonıucManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dogruText, yanlışText, puanText;
    [SerializeField] private GameObject birinciYıldız, ikinciYıldız, ucuncuYıldız;
    public void SonuclarıYazdır(int dogruAdet, int yanlışAdet, int puan)
    {
        dogruText.text = dogruAdet.ToString();
        yanlışText.text = yanlışAdet.ToString();
        puanText.text = puan.ToString();
        
        birinciYıldız.SetActive(false);
        ikinciYıldız.SetActive(false);
        ucuncuYıldız.SetActive(false);

        if (dogruAdet == 1)
        {
            birinciYıldız.SetActive(true);
        }
        else if (dogruAdet == 2)
        {
            birinciYıldız.SetActive(true);
            ikinciYıldız.SetActive(true);
        }
        else
        {
            birinciYıldız.SetActive(true);
            ikinciYıldız.SetActive(true);
            ucuncuYıldız.SetActive(true);
        }
    }
}
