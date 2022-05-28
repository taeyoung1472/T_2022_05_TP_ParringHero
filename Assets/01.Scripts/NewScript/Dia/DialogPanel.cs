using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogPanel : MonoBehaviour
{
    private List<TextVo> list; //�����ܰ� �޽����� ������ �װ� ��� ����
    private RectTransform panel; //��ȭâ �г�, �θ��� ����

    public TextMeshProUGUI dialogText; //�ؽ�Ʈ�Ž� ������ ���̾�α� â
    public GameObject skipButton;//��ŵ�ϴ� ��ư
    private WaitForSeconds shortWs = new WaitForSeconds(0.09f); //���ڰ� ������ �ӵ�


    private bool clickToNext = false; //���� ��ȭ�� �ѱ�� ���� Ŭ���� �Ǵ��ϴ� ����
    private bool isOpen = false; //â�� ���ȴ��� Ȯ���ϴ� ����

    public GameObject nextIcon; //�������� �ѱ�� ������
    public Image profileImage; //�������̹����� ��� ����?

    public AudioClip typeClip; //Ÿ���������� �Ҹ�

    private int currentIndex; //���� ��ȭ �ε���
    private RectTransform textTransform;// �ؽ�Ʈ â�� ũ��
    private Action endDialogCallback = null;

    public bool isTuto = false;

    private Dictionary<int, Sprite> imageDictionary = new Dictionary<int, Sprite>(); //���° �ڵ��� �̹������� �����ϴ� ����

    private bool pressButton = false;

    private void Awake()
    {
        if (isTuto)
        {
            panel = GetComponent<RectTransform>(); //��Ʈ���� �������� ȿ��
            textTransform = dialogText.GetComponent<RectTransform>(); //�ؽ�Ʈ�� ��ƮƮ������
        }
    }
    //�����ʹ� ���̰� �ڵ�� ����
    //1�г��� ¥���� string�� �����޾ư����� ¥�¤�
    // �����͸� �ۿ��� �о���� �������� ó���ϴ�
    public void StartDialog(List<TextVo> list, Action callback = null) //�����Ʈ�����ϴ��� �����Ʈ���� �ִ� �κ�
    {
        if (isTuto)
        {
            endDialogCallback = callback;
            this.list = list; 
            ShowDialog();
         
        }

    }

    public void ShowDialog() 
    {
        if (isTuto)
        {

            currentIndex = 0;
            panel.DOScale(new Vector3(1, 1, 1), 0.8f).OnComplete(() =>
            {
                DialogManager.TimeScale = 0f; 
                                            
                TypeIt(list[currentIndex]); 
                isOpen = true;
            });
        }
    }

    public void TypeIt(TextVo vo) //������, �޽��� ù��°�� �����ֱ�
    {
        if (isTuto)
        {
            int idx = vo.icon;

            if (!imageDictionary.ContainsKey(idx))
            {
                Sprite imag = Resources.Load<Sprite>($"profile{idx}");
                imageDictionary.Add(idx, imag);
            }

            profileImage.sprite = imageDictionary[idx];

            dialogText.text = vo.msg;
            //nextIcon.SetActive(false);

            clickToNext = false;
            StartCoroutine(Typing());
        }
    }


    IEnumerator Typing()
    {
        if (isTuto)
        {

            dialogText.ForceMeshUpdate();
            dialogText.maxVisibleCharacters = 0;


            int totalVisibleChar = dialogText.textInfo.characterCount;
            for (int i = 1; i <= totalVisibleChar; i++)
            {
                dialogText.maxVisibleCharacters = i;

                if (clickToNext)
                {
                    dialogText.maxVisibleCharacters = totalVisibleChar;
                    break;
                }

                yield return shortWs;

            }

            currentIndex++;
            clickToNext = true;
            nextIcon.SetActive(true);
        }
    }


    private void Update()
    {
        if (isTuto)
        {
            if (!isOpen) return;
            if (pressButton && clickToNext)
            {
                pressButton = false;    
                if (currentIndex >= list.Count)
                {
                    panel.DOScale(new Vector3(0, 0, 1), 0.8f).OnComplete(() =>
                    {
                        DialogManager.TimeScale = 1f;
                        isOpen = false;
                        if (endDialogCallback != null)
                        {
                            endDialogCallback();
                        }
                    });
                }
                else
                {
                    TypeIt(list[currentIndex]);
                }

            }
            else if (pressButton)
            {
                clickToNext = true;
                pressButton = false;
            }
        }
    }
    public void Next()
    {
        pressButton = true;
    }
}
