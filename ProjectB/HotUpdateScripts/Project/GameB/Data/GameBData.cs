using HotUpdateScripts.Project.BasePrj.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.Project.GameB.Data
{
    public enum GameBStatus
    {
        GameReady,
        Gaming,
        GameOver
    }
    public class GameBData
    {
        //{ "#02dfdc", "#f2bf46", "#fd4e4e", "#3dd071", "4b6ce1", "00e2f0" }
        public static Color32[] ThemeColors = new Color32[6] { new Color32(2,223,220,255), new Color32(88, 177, 246, 255),
            new Color32(242, 191, 70, 255), new Color32(61, 208, 113, 255), new Color32(75, 108, 255, 255), new Color32(0, 226, 240, 255)};

        //Mute 0
        public static bool IsMute
        {
            get
            {
                if (PlayerPrefs.HasKey(GameData.FunJumpEffectStatus))
                {
                    return PlayerPrefs.GetInt(GameData.FunJumpEffectStatus) == 0;
                }
                else
                {
                    PlayerPrefs.SetInt(GameData.FunJumpEffectStatus, 1);
                    return false;
                }
            }
            //set
            //{
            //    PlayerPrefs.SetInt(GameData.FunJumpEffectStatus, value == true ? 0 : 1);
            //}
        }

        //1-9
        private int characterId;
        public static int CharacterId
        {
            get
            {
                if (PlayerPrefs.HasKey("GameBCharacterId"))
                {
                    return PlayerPrefs.GetInt("GameBCharacterId") > 9 || PlayerPrefs.GetInt("GameBCharacterId") < 1 ?
                        1 : PlayerPrefs.GetInt("GameBCharacterId");
                }
                else
                {
                    PlayerPrefs.SetInt("GameBCharacterId", 1);
                    return 1;
                }
            }
            set
            {
                PlayerPrefs.SetInt("GameBCharacterId", value);
            }

        }

        //1-6
        public static int ThemeId
        {
            get
            {
                if (PlayerPrefs.HasKey("GameBThemeId"))
                {
                    return PlayerPrefs.GetInt("GameBThemeId") > 6 || PlayerPrefs.GetInt("GameBThemeId") < 1
                        ? 1 : PlayerPrefs.GetInt("GameBThemeId");
                }
                else
                {
                    PlayerPrefs.SetInt("GameBThemeId", 1);
                    return 1;
                }
            }
            set
            {
                PlayerPrefs.SetInt("GameBThemeId", value);
            }

        }

        //当前解锁数量
        public static int LockedCharacterNum;

        public static int LockedThemeNum;
    }

    public class DataGameBUser
    {
        public int highestScore;

        public int calcScore;


    }
}
