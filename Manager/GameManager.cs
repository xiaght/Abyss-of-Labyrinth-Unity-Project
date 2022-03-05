using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public MapGenerator map;
    public Player player;
    public int score=0;
    public int maxscore;
    public int stage=0;
    public int maxHp;
    public int curHp;

    public int stageIndex;
    public int coin;
    public int luck;

    public int maxexperience;
    public int experience;

    public int level;
    public RectTransform skill;

    public List<GameObject> skilllist;

    public List<GameObject> basicSkill;
    public Transform[] skillPoint;

    public Texture2D cursorImg;

    private void Awake()
    {
        level = 1;
        maxexperience = 10;
        Time.timeScale = 0;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);

        maxscore = PlayerPrefs.GetInt("MaxScore");
    }

    private void Update()
    {
        if (experience > maxexperience) {
            skilllist = basicSkill.ToList();
            Time.timeScale = 0;
            experience =0;
            level++;
            maxexperience = 10+level;

            skill.gameObject.SetActive(true);
            int ran1 = Random.Range(0, skilllist.Count);
            GameObject skill1= Instantiate(skilllist[ran1], skillPoint[0]);
            SkillSetting skill1setting1=skill1.GetComponent<SkillSetting>();
            skill1setting1.player = player;
            skill1setting1.skillPoint = skillPoint;
            skill1setting1.gm = this;
            skilllist.RemoveAt(ran1);

            int ran2 = Random.Range(0, skilllist.Count);
            GameObject skill2 = Instantiate(skilllist[ran2], skillPoint[1]);
            SkillSetting skill1setting2 = skill2.GetComponent<SkillSetting>();
            skill1setting2.player = player;
            skill1setting2.skillPoint = skillPoint;
            skill1setting2.gm = this;
            skilllist.RemoveAt(ran2);

            int ran3 = Random.Range(0, skilllist.Count);
            GameObject skill3 = Instantiate(skilllist[ran3], skillPoint[2]);
            SkillSetting skill1setting3 = skill3.GetComponent<SkillSetting>();
            skill1setting3.player = player;
            skill1setting3.skillPoint = skillPoint;
            skill1setting3.gm = this;
            skilllist.RemoveAt(ran3);





        }
    }




}
