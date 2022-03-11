using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public Player player;


    public Text PlayerHpText;
    public Image PlayerHpBar;

    public Image ExpBar;

    public Text ScoreText;
    public Text stageText;

    public Text goldText;
    public Text levelText;


    bool escButton;
    public GameObject optionPanle;

    public GameManager gm;
    public Text enemycount;
    public MapGenerator map;

    public Button skillButton;
    public RectTransform skill;

    public Text ShopCoin;

    public Image skill1CoolTime;
    public float cool = 5f;
    public float maxcool;

    private void Awake()
    {
        maxcool = cool;

    }
    private void Update()
    {
        GetInput();
        EscOption();
        if (!player.skill1CoolTime)
        {
           skill1Cool();

/*            StartCoroutine(Test());*/
        }
        else
        {

            skill1CoolTime.gameObject.SetActive(false);
            maxcool = 5;
        }


    }

 
 /*   IEnumerator Test()
    {
        skill1CoolTime.gameObject.SetActive(true);
        while (maxcool >= 0) {
            maxcool -= Time.deltaTime;
            skill1CoolTime.fillAmount = maxcool / cool;
            yield return new WaitForFixedUpdate();
        }
    }*/

    public void skill1Cool() {

        skill1CoolTime.gameObject.SetActive(true);
        maxcool -= Time.deltaTime;
        skill1CoolTime.fillAmount = maxcool / cool;
        
    }



    void GetInput()
    {
        escButton = Input.GetKeyDown(KeyCode.Escape);

    }
    void EscOption()
    {
        if (escButton)
        {
            if (optionPanle.activeSelf)
            {
                optionPanle.SetActive(false);
                Time.timeScale = 1;
            }
            else {
                optionPanle.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }



    private void LateUpdate()
    {
        levelText.text="Level : "+gm.level;
        goldText.text =  "Gold : "+ gm.coin;
        stageText.text = "Stage : "+gm.stage;
        ScoreText.text = "Score : " + gm.score;
        ShopCoin.text=  gm.coin+"G";
        enemycount.text = map.enemyzone.childCount.ToString();


        PlayerHpText.text = (float)player.curhp + "/" + player.maxhp;
       
    
        PlayerHpBar.fillAmount = (float)player.curhp / (float)player.maxhp;

        ExpBar.fillAmount = (float)gm.experience /gm.maxexperience;
        //PlayerHpBar.localScale = new Vector3(player.curhp / (float)player.maxhp, 1, 1);


    }
}
