using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetting : MonoBehaviour
{
    public Player player;
    public GameManager gm;
    public enum Type
    {
        MaxHp, Damage, Luck, MaxShotDelay, Speed, Bulletspeed,SkillDamage, heal,
        Special
    }
    public Type type;

    public float value;
    public float value2;
    public Transform[] skillPoint;

    Button btn;


    public void OnClickButton()
    {
        SkillSetting skillbutton = GetComponent<SkillSetting>();

        Time.timeScale = 1;
        Destroy(skillPoint[0].GetChild(0).gameObject);
        Destroy(skillPoint[1].GetChild(0).gameObject);
        Destroy(skillPoint[2].GetChild(0).gameObject);
        switch (skillbutton.type)
        {

            case SkillSetting.Type.MaxHp:
                player.maxhp += (int)skillbutton.value;
                player.curhp += (int)skillbutton.value;
                break;

            case SkillSetting.Type.Luck:
                player.luck += (int)skillbutton.value;
                break;
            case SkillSetting.Type.Damage:

                Debug.Log("playerDamage" + player.damage + "but dam" + skillbutton.value);
                player.damage += skillbutton.value;

                break;
            case SkillSetting.Type.MaxShotDelay:
                player.MaxShotDelay =player.MaxShotDelay*skillbutton.value;
                if (player.MaxShotDelay <= 0.1f) {

                    player.MaxShotDelay = 0.1f;
                }
                break;
            case SkillSetting.Type.Speed:
                player.speed += skillbutton.value;
                if (player.speed >=15)
                {

                    player.speed = 15;
                }
                break;
            case SkillSetting.Type.Bulletspeed:
                player.bulletspeed += skillbutton.value;
                break;
            case SkillSetting.Type.SkillDamage:
                player.Skill1damage += skillbutton.value;
                break;

            case SkillSetting.Type.heal:
                if (player.curhp + skillbutton.value >= player.maxhp)
                {
                    player.curhp = player.maxhp;
                }
                else {
                    player.curhp += (int)skillbutton.value;
                }
                break;

            case SkillSetting.Type.Special:
                gm.basicSkill.RemoveAt(6);
                break;

        }



    }



}
