using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public List<Sprite> list = new List<Sprite>();
    public Image FutaImage;
    public TextMeshProUGUI textPidor;
    public TextMeshProUGUI textPidorDa;


    public GameObject menu;

    bool circle = false;
    int number = 0;
    public void PlayGame()
    {
        SceneManager.LoadScene("StoneCave");
    }

    public void ExitGame()
    {
        Debug.Log("���� ���������, ����.");
        Application.Quit();
    }

    public void NextImage()
    {
        textPidorDa.text = "��";
        FutaImage.gameObject.SetActive(true);
        FutaImage.sprite = list[number];
        FutaImage.GetComponent<Image>().preserveAspect = true;
        number++;
        if(number == list.Count)
        {
            number = 0;
            circle = true;
        }
    }

    public void NoButton()
    {
        textPidor.text = "���� �� ��������� �������?";
        textPidorDa.text = "��";
        textPidor.alignment = TextAlignmentOptions.Center;
        FutaImage.gameObject.SetActive(true);
        FutaImage.sprite = list[number];
        FutaImage.GetComponent<Image>().preserveAspect = true;
        number++;
        if (number == list.Count)
        {
            number = 0;
            circle = true;
        }
    }

    public void FreeButton()
    {
        if(circle)
        {
            menu.SetActive(true);
            circle = false;
            number = 0;
        }
    }

}
