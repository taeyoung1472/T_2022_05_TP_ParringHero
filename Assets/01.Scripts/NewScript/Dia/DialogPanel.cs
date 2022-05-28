using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogPanel : MonoBehaviour
{
    private List<TextVo> list; //아이콘과 메시지가 있으며 그걸 담는 역할
    private RectTransform panel; //대화창 패널, 부르기 위해

    public TextMeshProUGUI dialogText; //텍스트매시 프로의 다이얼로그 창
    public GameObject skipButton;//스킵하는 버튼
    private WaitForSeconds shortWs = new WaitForSeconds(0.09f); //글자가 찍히는 속도


    private bool clickToNext = false; //다음 대화로 넘기기 위한 클릭이 판단하는 역할
    private bool isOpen = false; //창이 열렸는지 확인하는 역할

    public GameObject nextIcon; //다음으로 넘기는 아이콘
    public Image profileImage; //프로필이미지를 담는 역할?

    public AudioClip typeClip; //타이핑찍히는 소리

    private int currentIndex; //현재 대화 인덱스
    private RectTransform textTransform;// 텍스트 창의 크기
    private Action endDialogCallback = null;

    public bool isTuto = false;

    private Dictionary<int, Sprite> imageDictionary = new Dictionary<int, Sprite>(); //몇번째 코드의 이미지이진 저장하는 역할

    private bool pressButton = false;

    private void Awake()
    {
        if (isTuto)
        {
            panel = GetComponent<RectTransform>(); //닷트윈을 쓰기위한 효과
            textTransform = dialogText.GetComponent<RectTransform>(); //텍스트에 렉트트랜스폼
        }
    }
    //데이터는 밖이고 코드는 로직
    //1학년이 짜면은 string에 변수받아가지고 짜는ㄷ
    // 데이터를 밖에서 읽어오고 로직으로 처리하는
    public void StartDialog(List<TextVo> list, Action callback = null) //어떤리스트시작하는지 어떤리스트인지 주는 부분
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

    public void TypeIt(TextVo vo) //아이콘, 메시지 첫번째줄 보여주기
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
