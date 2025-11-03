using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using System.Collections.Generic;
using HotUpdateScripts.Project.Game.GameA.Data;

namespace My.UI.Panel
{
    public class UIBMainHomePanel : BasePanel
    {
        //公益
        Button btn_Love, btn_Donate, btn_Honor;   //爱心工坊（孵化）, 希望灯塔（捐献）,  荣誉展厅（荣誉） 
        //互娱
        Button btn_Game, btn_Friend;   //游戏 , 交友
        //其他
        Button btn_Shop, btn_Lucky;  //兑换商店 , 幸运小屋
        //主页数据
        DataBMainHome mainHomeData;
        //玩家信息
        DataBUpdataData updateData;
        //左右箭头
        private RectTransform Content;
        private GameObject left, right;

        //左上角信息
        private Image Icon;
        private GameObject IconBg;
        private RectTransform pan_Name;
        private Text txt_Name, txt_MsgNum, txt_JBNum, txt_LoveNum, txt_DonateNum;
        private Button btn_Msg;
        private Image sv_Bottom;
        private bool isFirst = true;
        private RectTransform pan_Top;

        void Awake()
        {
            btn_Love = GameObject.Find("btn_Love").GetComponent<Button>();
            btn_Donate = GameObject.Find("btn_Donate").GetComponent<Button>();
            btn_Honor = GameObject.Find("btn_Honor").GetComponent<Button>();
            btn_Game = GameObject.Find("btn_Game").GetComponent<Button>();
            btn_Friend = GameTools.GetByName(transform, "btn_Friend").GetComponent<Button>();
            btn_Shop = GameObject.Find("btn_Shop").GetComponent<Button>();
            btn_Lucky = GameObject.Find("btn_Lucky").GetComponent<Button>();
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            pan_Name = GameTools.GetByName(transform, "pan_Name").GetComponent<RectTransform>();
            txt_Name = GameTools.GetByName(transform, "txt_Name").GetComponent<Text>();
            btn_Msg = GameTools.GetByName(transform, "btn_Msg").GetComponent<Button>();
            txt_MsgNum = GameTools.GetByName(transform, "txt_MsgNum").GetComponent<Text>();
            txt_JBNum = GameTools.GetByName(transform, "txt_JBNum").GetComponent<Text>();
            txt_LoveNum = GameTools.GetByName(transform, "txt_LoveNum").GetComponent<Text>();
            txt_DonateNum = GameTools.GetByName(transform, "txt_DonateNum").GetComponent<Text>();
            sv_Bottom = GameTools.GetByName(transform, "sv_Bottom").GetComponent<Image>();
            Content = GameTools.GetByName(transform, "Content").GetComponent<RectTransform>();
            left = GameTools.GetByName(transform, "left");
            right = GameTools.GetByName(transform, "right");
            pan_Top = GameTools.GetByName(transform, "pan_Top").GetComponent<RectTransform>();
            transform.Find("Head/BroadCast").gameObject.AddComponent<BroadCastComponent>(); ;

            #region Regist EVENT
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_setting"), () =>
            {
                debug.Log_Blue("点击设置按钮");
                UIMgr.Instance.Open(UIPath.UISettingPanel);
            });
            //退出按钮
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_return"), () =>
             {
                 debug.Log_Blue("点击返回按钮");
                 ReturnToNative();
             });
            GameTools.Instance.AddClickEvent(transform.Find("GuidanceBtn"), () =>
            {
                UIMgr.Instance.Open(UIPath.UIBMainBeginnerGuidePanel);
            });
            GameTools.Instance.AddClickEvent(transform.Find("ActivityBtn"), () =>
            {
                //debug.Log_Blue("活动");
                UIMgr.Instance.Open(UIPath.UIBActivityPanel);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_FirstTimePlay, S2C_Game_FirstTimePlayCallBack);

            MsgCenter.RegisterMsg(null, MsgCode.DayNightUpt, DayNightUptCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Main_HomeGetInfo, S2C_Main_HomeGetInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Init_UserInfo, S2C_Init_UserInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Main_GetBagInfo, S2C_Main_GetBagInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateBagData, S2C_Update_UpdateBagDataCallBack);

            btn_Msg.onClick.AddListener(OnBtnMsgClick);

            IconBg = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "IconBg"), () =>
            {
                UIMgr.Instance.Open(UIPath.UIBMainBagPanel);
            });

            DataBLogin dataBLogin = new DataBLogin();
            dataBLogin.type = "GetLoginToken";
            MsgINAVLocalAndHotupt.Instance.AddEventListener(MsgINAVLocalAndHotuptCode.LocalToHotupt, (o) =>
            {
                debug.Log_purple(o.ToString());
                DataBNativeToUnity dataBNativeToUnity = JsonMapper.ToObject<DataBNativeToUnity>(o.ToString());
                if (dataBNativeToUnity.type == "GetLoginToken")
                {
                    if (string.IsNullOrEmpty(dataBNativeToUnity.token))
                    {
                        //DataBLogin dataBLogin = new DataBLogin();
                        //dataBLogin.type = "GetLoginToken";
                        MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal, JsonMapper.ToJson(dataBLogin));
                    }
                    else
                    {
                        DataMgr.Instance.dataBFirstToServer.token = dataBNativeToUnity.token;
                        NetMgr.Instance.C2S_Game_FirstTimePlay();
                    }
                }
            });

            //广播 
            MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal, JsonMapper.ToJson(dataBLogin));
            #endregion
            //DataMgr.Instance.dataBFirstToServer.token ="";
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                switch (GameData.CurrentServer)
                {
                    case ServerType.LocalServer:
                        //local
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNjEiLCJ1c2VyX25hbWUiOiI2MS0xNTAzNjAxNTUyMCIsInNjb3BlIjpbIlJPTEVfQURNSU4iLCJST0xFX1VTRVIiLCJST0xFX0FQSSJdLCJleHAiOjE2NDQzNDg1NDQsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiJmZDc2ZTQ0NC1hYjlhLTRiODgtYjMyYi01ODIxZGIzOGMwOGMiLCJjbGllbnRfaWQiOiJqdWFpLWxpZmUiLCJjZWxsX3Bob25lIjoiMTUwMzYwMTU1MjAifQ.wm-MdlEKQjN7bEtoaTP0kzeFxA6xse7q7uAKccpTTq4";
                        //熊大
                        DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNTEiLCJ1c2VyX25hbWUiOiI1MS0xODIwNzM3MTc5NyIsInNjb3BlIjpbIlJPTEVfQURNSU4iLCJST0xFX1VTRVIiLCJST0xFX0FQSSJdLCJleHAiOjE2NTAwNTA4NzQsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiJlODNhYzFjYS0yZDNhLTQyZWUtOWFlMi0xOTE0MGFiOGMxODAiLCJjbGllbnRfaWQiOiJqdWFpLWxpZmUiLCJjZWxsX3Bob25lIjoiMTgyMDczNzE3OTcifQ.7CKrov72pZRdauJLeGbetjH4zl_ugRFdcWRKsFjdx1A";
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiMTA3MTYwIiwidXNlcl9uYW1lIjoiMTA3MTYwLTE4ODQ1NTk4MTc5Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY1NzIyNDE3NSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6ImVhM2ZlMzBhLWU1NDMtNDk0NS04ZDVkLTlhYTViNWNjOWFmMyIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxODg0NTU5ODE3OSJ9.sC8xgDkQysEpaRhdYctMVf3Tf7ynErrpyNtnwAi4MSg";
                        break;
                    case ServerType.TestingServer:
                        //test 5238
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNTEiLCJ1c2VyX25hbWUiOiI1MS0xODIwNzM3MTc5NyIsInNjb3BlIjpbIlJPTEVfQURNSU4iLCJST0xFX1VTRVIiLCJST0xFX0FQSSJdLCJleHAiOjE2NDg4NDYxNjMsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiJkMzNkYmIwZS0yM2NhLTRjYzYtODM3OS04OWI2MmVlMmRhYWIiLCJjbGllbnRfaWQiOiJqdWFpLWxpZmUiLCJjZWxsX3Bob25lIjoiMTgyMDczNzE3OTcifQ.hqBj3ifilshwYoGJWahF0zMePIRHEG1w-OvrYWqIjxI.wm-MdlEKQjN7bEtoaTP0kzeFxA6xse7q7uAKccpTTq4";
                        //xuefeng test
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNDYiLCJ1c2VyX25hbWUiOiI0Ni0xODIwNzM3MTc5NyIsInNjb3BlIjpbIlJPTEVfQURNSU4iLCJST0xFX1VTRVIiLCJST0xFX0FQSSJdLCJleHAiOjE2ODEwMDIxNzUsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiJiNTI2YTQyMS01ZDViLTQ2ZTEtYTU1Zi04NTExMWRiOTI3YTkiLCJjbGllbnRfaWQiOiJqdWFpLWxpZmUiLCJjZWxsX3Bob25lIjoiMTgyMDczNzE3OTcifQ.0N_LNxsCD5a-GNtCetCVYy_QhF9kCo-0eWPMh2PbDCA";

                        //6807
                        DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiMjE2NjM0MjQyNTQzOTQxIiwidXNlcl9uYW1lIjoiMjE2NjM0MjQyNTQzOTQxLTE1NTE0NTE2ODA3Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY5NzMzODI0NCwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjgwZTdjZmQ2LTAzMjctNDhmYy04ODk5LTYwMGM1NTgwOGY3NyIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxNTUxNDUxNjgwNyJ9.U1EPBdVaAMWmyRtyio64dT2QisaCbI4yBrfx0lh-2Qc";
                        break;
                    case ServerType.PrePublishingServer:
                        DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNzQ3Mjg2IiwidXNlcl9uYW1lIjoiNzQ3Mjg2LTE1NTE0NTE2ODA3Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY1MTI5MTA0MSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjdhMDI1YmZmLTcyNmMtNDkwZC1iNTEwLWQ4ZjgyOTgzZDRmZSIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxNTUxNDUxNjgwNyJ9.sZ0AdipeVS-d0jpK9AYePFqjcU3n-43zpqObC45TeIE";
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiMTA3MTYwIiwidXNlcl9uYW1lIjoiMTA3MTYwLTE4ODQ1NTk4MTc5Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY1NzIyNDE3NSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6ImVhM2ZlMzBhLWU1NDMtNDk0NS04ZDVkLTlhYTViNWNjOWFmMyIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxODg0NTU5ODE3OSJ9.sC8xgDkQysEpaRhdYctMVf3Tf7ynErrpyNtnwAi4MSg";
                        //xuefeng
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNjcxMzkwIiwidXNlcl9uYW1lIjoiNjcxMzkwLTE4MjA3MzcxNzk3Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NTc4MTczMCwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjcwOWFjOWU2LTE1YzItNDg1NC1hZmJjLWQxNmQ0OGU2ZjI3OSIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxODIwNzM3MTc5NyJ9.kcgU9qHwBpvqYIMuFjtnONqdO640X5zm32EetjepTDQ";
                        //xuefeng2
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNjg4OTU3IiwidXNlcl9uYW1lIjoiNjg4OTU3LTE3NTk2NTUxOTAyIiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NjQ3MjczMSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjIzZDM1YTU2LThjNzEtNDU5ZC04YjU0LWIwOTI4YjkyMTI4NSIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxNzU5NjU1MTkwMiJ9.relbkr8mX2cmwy2A4g4qhB5JjS1e6HxcMMK1adbC16k";
                        //shuaizheng
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiMzUwMjgzIiwidXNlcl9uYW1lIjoiMzUwMjgzLTE1MDM2MDE1NTIwIiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NjQ2OTY2MywiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6ImQ5ZjRlOWMzLTU2MmMtNDcwOS04YWZkLWRjMmExOTRmMTVmNSIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxNTAzNjAxNTUyMCJ9.Qe5ZHf57Y4zIM6R9Xk857xqgCHjNqOuSktDb-CA9RZY";
                        //shiman
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNzY1NDcwIiwidXNlcl9uYW1lIjoiNzY1NDcwLTE4NTYzOTcxOTEwIiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NzUxMDEwOSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjVkMWNiYmRjLWMxYjUtNDg5YS04ZDRiLWQwNzJkZDIyMWU0NiIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxODU2Mzk3MTkxMCJ9.I1J-jbZWScbEmLOcX5fHcofNWIOZbxShE-KNMXCnY2A";

                        break;
                    case ServerType.OfficialServer:
                        //xuefeng
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNjcxMzkwIiwidXNlcl9uYW1lIjoiNjcxMzkwLTE4MjA3MzcxNzk3Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NjgxNzI0MCwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6ImMyZmNmOTQ1LTFhYWEtNDdkYS04NzdkLWNkYjMyZWExN2M2MyIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxODIwNzM3MTc5NyJ9.U0Rls10vyd5dMtvN3WVhd28NnlVr2A5WbHlgEPElo6U";

                        DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNzQ3Mjg2IiwidXNlcl9uYW1lIjoiNzQ3Mjg2LTE1NTE0NTE2ODA3Iiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NzQ0NjEyNCwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6ImU0NDU5NjViLTY4ZmQtNGRlYS1iNjkxLWIwM2Q3M2ZhNTBmNyIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxNTUxNDUxNjgwNyJ9.m7wWy9ew_-WLo12Tz7ECoxcJRgS8-zGOEL8IMKKFHzc";
                        //shiman
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNzY1NDcwIiwidXNlcl9uYW1lIjoiNzY1NDcwLTE4NTYzOTcxOTEwIiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2NzUxMDEwOSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjVkMWNiYmRjLWMxYjUtNDg5YS04ZDRiLWQwNzJkZDIyMWU0NiIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxODU2Mzk3MTkxMCJ9.I1J-jbZWScbEmLOcX5fHcofNWIOZbxShE-KNMXCnY2A";
                        //DataMgr.Instance.dataBFirstToServer.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiMzUwMjgzIiwidXNlcl9uYW1lIjoiMzUwMjgzLTE1MDM2MDE1NTIwIiwic2NvcGUiOlsiUk9MRV9BRE1JTiIsIlJPTEVfVVNFUiIsIlJPTEVfQVBJIl0sImV4cCI6MTY2Nzg0NzUxNCwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjczNzIxN2FjLTlmZWQtNDAxYy1hYmQwLTM1NWEzZDBlNGI4MyIsImNsaWVudF9pZCI6Imp1YWktbGlmZSIsImNlbGxfcGhvbmUiOiIxNTAzNjAxNTUyMCJ9.2VTLThFz5NNUJ37gYQFEAk5iCXd_XCSaijeV4yaMxko";

                        break;
                    default:
                        break;
                }

                NetMgr.Instance.C2S_Game_FirstTimePlay();

                GameObject ObjDebug = GameObject.Find("Debug");
                if (ObjDebug != null) ObjDebug.SetActive(false);
                debug.EnableLog = true;
            }
            else
            {
                if (GameData.CurrentServer == ServerType.OfficialServer)
                {
                    debug.EnableLog = false;
                    GameObject ObjDebug = GameObject.Find("Debug");
                    if (ObjDebug != null) ObjDebug.SetActive(false);
                }
            }
        }
        private void _GotoScene()
        {
            GameTools.SetLoadingActive(false);
            AudioMgr.PlayMusic(AudioConfig.GameA_BGM, GameData.GameAudioBgmStatus);
            //OnBtnCloseClick();
            UIMgr.Instance.Close(UIPath.UIBMainHomePanel, false);
        }
        void Update()
        {
            if (Content == null) return;

            if (Content.localPosition.x >= -70)
            {
                left.SetActive(false);
                right.SetActive(true);
            }
            else if (Content.localPosition.x <= -700)
            {
                left.SetActive(true);
                right.SetActive(false);
            }
            else
            {
                left.SetActive(true);
                right.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                PlayerPrefs.DeleteAll();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                UIMgr.Instance.Open(UIPath.UIBRedPackagePanel);

            }
        }

        void OnDestory()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_FirstTimePlay, S2C_Game_FirstTimePlayCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.DayNightUpt, DayNightUptCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Main_HomeGetInfo, S2C_Main_HomeGetInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Init_UserInfo, S2C_Init_UserInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Main_GetBagInfo, S2C_Main_GetBagInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateBagData, S2C_Update_UpdateBagDataCallBack);

            MsgINAVLocalAndHotupt.Instance.RemoveEventListener(MsgINAVLocalAndHotuptCode.LocalToHotupt, (o) =>
            {

            });
        }

        #region CALLBACK
        //原生第一次登录后回调
        void S2C_Game_FirstTimePlayCallBack(object o)
        {
            GameData.userId = DataMgr.Instance.dataBFirstToUnity.userId;
            if (GameData.userId != -1)
            {
                GameData.SetLocalStorageData(GameData.userId.ToString());
                NetMgr.Instance.C2S_Main_HomeGetInfo(GameData.userId.ToString());
                NetMgr.Instance.C2S_Other_SaveSession(GameData.userId.ToString());
                NetMgr.Instance.C2S_Main_GetBagInfo(GameData.userId.ToString());

                //请求数据 任务列表
                NetMgr.Instance.C2S_Love_TaskGetList(GameData.userId.ToString());
                //用户行为 进入项目
                DataMgr.Instance.dataBProject.userId = GameData.userId;
                DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ENTER_PROJECT];
                NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
            }
            else
            {
                debug.Log_Blue("服务器存储原生传过来的信息失败！");
            }

            //if (DataMgr.Instance.dataBFirstToUnity.msg == true)
            //{
            //    GameData.userId = DataMgr.Instance.dataBFirstToUnity.userId;
            //    if (GameData.userId != -1)
            //    {
            //        NetMgr.Instance.C2S_Main_HomeGetInfo(GameData.userId.ToString());
            //        NetMgr.Instance.C2S_Other_SaveSession(GameData.userId.ToString());
            //        NetMgr.Instance.C2S_Main_GetBagInfo(GameData.userId.ToString());
            //        //用户行为 进入项目
            //        DataMgr.Instance.dataBProject.userId = GameData.userId;
            //        DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ENTER_PROJECT];
            //        NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
            //    }
            //}
            //else
            //{
            //    debug.Log_Blue("服务器存储原生传过来的信息失败！");
            //}
        }

        /// <summary>
        /// 首页UI（白夜）主题更新 监听回调
        /// </summary>
        private void DayNightUptCallBack(object o)
        {
            if (o.ToString() == "day")
            {
                sv_Bottom.sprite = JResource.LoadRes<Sprite>("BaseProject/Main/" + "b_day" + ".png", JResource.MatchMode.UI);
            }
            else if (o.ToString() == "night")
            {
                sv_Bottom.sprite = JResource.LoadRes<Sprite>("BaseProject/Main/" + "b_night" + ".png", JResource.MatchMode.UI);
            }
        }

        /// <summary>
        /// 首页信息获取 接口
        /// </summary>
        /// <param name="o"></param>
        private void S2C_Main_HomeGetInfoCallBack(object o)
        {
            mainHomeData = DataMgr.Instance.dataBMainHome;

            if (isFirst)
            {
                //非员工及已婚人士隐藏交友入口
                btn_Friend.gameObject.SetActive(mainHomeData.AllowMakingFriends);
                GameData.userId = mainHomeData.userId;
                GameData.allUnreadChatMessageNum = mainHomeData.allUnreadChatMessageNum;
                GameData.curLoveNum = mainHomeData.curLoveNum;
                GameData.curDonateLoveNum = mainHomeData.donateLoveNum;
                GameData.accumulativeGetLoveNum = mainHomeData.accumulativeGetLoveNum;
                GameData.curLoveMoney = mainHomeData.curLoveMoney;
                GameData.userHeadImg = mainHomeData.headImgUrl;
                GameData.isHatching = mainHomeData.isHatching;
                GameData.canDraw = mainHomeData.canDraw;
                GameData.curFreeMatchNum = mainHomeData.freeMatchTime;
                debug.Log_purple(SystemInfo.deviceModel);
                //昵称和头像
                txt_Name.text = mainHomeData.nickName;
                NetMgr.Instance.DownLoadHeadImg(r =>
                {
                    if (Icon == null) return;
                    Icon.sprite = r;
                    GameData.userHeadSprite = r;
                    //GameTools.Instance.MatchImgBySprite(Icon);
                }, mainHomeData.headImgUrl);
                LayoutRebuilder.ForceRebuildLayoutImmediate(pan_Name);

                isFirst = false;
            }
            SetHeadView();
            //SetBottomBtns();
            SetButtonsEvent();
            TimeMgr.Instance.GetServerTime();
        }

        /// <summary>
        /// 用户数据重置 接口
        /// </summary>
        /// <param name="o"></param>
        private void S2C_Init_UserInfoCallBack(object o)
        {
            PlayerPrefs.DeleteAll();
            GameTools.SetTip("清空个人数据成功");
            ReturnToNative();
        }

        /// <summary>
        /// 背包数据获取 接口
        /// </summary>
        /// <param name="o"></param>
        void S2C_Main_GetBagInfoCallBack(object o)
        {
            GameData.BagData = DataMgr.Instance.dataBag;
        }

        /// <summary>
        /// 用户信息数据更新 接口
        /// </summary>
        /// <param name="o"></param>
        void S2C_Update_UpdateGameDataCallBack(object o)
        {
            updateData = DataMgr.Instance.dataBUpdataData;
            if (updateData == null) return;
            GameData.allUnreadChatMessageNum = updateData.allUnreadChatMessageNum;
            GameData.isHatching = updateData.isHatching;
            GameData.canDraw = updateData.canDraw;
            GameData.curLoveNum = updateData.curLoveNum;
            GameData.curDonateLoveNum = updateData.donateLoveNum;
            GameData.curEnergy = updateData.curEnergy;
            GameData.accumulativeGetLoveNum = updateData.accumulativeGetLoveNum;
            if (float.Parse(updateData.curLoveMoney) != 0)
            {
                GameData.curLoveMoney = updateData.curLoveMoney;
            }
            GameData.curFreeMatchNum = updateData.freeMatchTime;
            SetHeadView();
            //SetBottomBtns();
        }

        /// <summary>
        /// 背包数据更新 接口
        /// </summary>
        /// <param name="o"></param>
        void S2C_Update_UpdateBagDataCallBack(object o)
        {
            GameData.BagData = DataMgr.Instance.dataBUpdateBagData;
        }

        //原生返回
        void ReturnToNative()
        {
            DataBLogin dataBLogin = new DataBLogin();
            dataBLogin.type = "BackToNative";
            MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal, JsonMapper.ToJson(dataBLogin));
        }


        //新消息提示 按钮
        void OnBtnMsgClick()
        {
            GameData.isOpenFriendList = true;
            UIMgr.Instance.Open(UIPath.UIBFriendHomePanel, null, UILayer.Layer3);
            //if (GameData.curDonateLoveNum >= 20)
            //{
            //    GameData.isOpenFriendList = true;
            //    UIMgr.Instance.Open(UIPath.UIBFriendHomePanel, null, UILayer.Layer3);
            //}
            //else11
            //    GameTools.SetTip("您有 <color=red>" + GameData.allUnreadChatMessageNum + "</color> 条未读消息 \n <b>交友大厅</b> 未解锁");
        }

        //公益
        void OnBtnLoveClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBLoveHomePanel);
        }
        void OnBtnDonateClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBDonateHomePanel);
        }
        void OnBtnHonorClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBHonorHomePanel);
        }

        void OnBtnGameClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBGameHomePanel);
        }
        void OnBtnFriendClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBFriendHomePanel);
        }
        //其他
        void OnBtnShopClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBShopPanel);
        }

        void OnBtnLuckyClick()
        {
            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBLuckyRotatePanel);
        }

        #endregion

        /// <summary>
        ///设置顶部信息
        /// </summary>
        void SetHeadView()
        {
            txt_JBNum.text = float.Parse(GameData.curLoveMoney).ToString("F2");
            txt_LoveNum.text = GameData.curLoveNum.ToString();
            txt_DonateNum.text = GameData.curDonateLoveNum.ToString();
            txt_MsgNum.text = GameData.allUnreadChatMessageNum > 99 ? "99" : GameData.allUnreadChatMessageNum.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(pan_Top);
            if (GameData.allUnreadChatMessageNum <= 0)
            {
                btn_Msg.gameObject.SetActive(false);
            }
            else
            {
                //闪烁几次
                CanvasGroup cg = btn_Msg.GetComponent<CanvasGroup>();
                cg.alpha = 0;
                cg.DOFade(1, 0.65f).SetLoops(6, LoopType.Yoyo).OnComplete(() =>
                {
                    cg.alpha = 1;
                });
                btn_Msg.gameObject.SetActive(true);
            }
        }

        private void SetButtonsEvent()
        {
            btn_Love.onClick.RemoveAllListeners();
            btn_Donate.onClick.RemoveAllListeners();
            btn_Honor.onClick.RemoveAllListeners();
            btn_Game.onClick.RemoveAllListeners();
            btn_Friend.onClick.RemoveAllListeners();
            btn_Shop.onClick.RemoveAllListeners();
            btn_Lucky.onClick.RemoveAllListeners();
            //交友大厅只对内部单身默认开发
            if (mainHomeData.AllowMakingFriends)
            {
                btn_Friend.gameObject.SetActive(true);
                btn_Friend.onClick.AddListener(OnBtnFriendClick);
                SetLockBtnUI(btn_Friend);
            }
            else
            {
                btn_Friend.gameObject.SetActive(false);
            }
            btn_Love.onClick.AddListener(OnBtnLoveClick);
            SetLockBtnUI(btn_Love);
            btn_Donate.onClick.AddListener(OnBtnDonateClick);
            SetLockBtnUI(btn_Donate);
            btn_Honor.onClick.AddListener(OnBtnHonorClick);
            SetLockBtnUI(btn_Honor);
            btn_Game.onClick.AddListener(OnBtnGameClick);
            SetLockBtnUI(btn_Game);
            btn_Shop.onClick.AddListener(OnBtnShopClick);
            SetLockBtnUI(btn_Shop);
            btn_Lucky.onClick.AddListener(OnBtnLuckyClick);
            SetLockBtnUI(btn_Lucky);
        }

        /// <summary>
        /// UI 设置底部按钮
        /// </summary>
        void SetBottomBtns()
        {
            btn_Love.onClick.RemoveAllListeners();
            btn_Donate.onClick.RemoveAllListeners();
            btn_Honor.onClick.RemoveAllListeners();
            btn_Game.onClick.RemoveAllListeners();
            btn_Friend.onClick.RemoveAllListeners();
            btn_Shop.onClick.RemoveAllListeners();
            btn_Lucky.onClick.RemoveAllListeners();

            //交友大厅只对内部单身默认开发
            if (mainHomeData.AllowMakingFriends)
            {
                btn_Friend.gameObject.SetActive(true);
                btn_Friend.onClick.AddListener(OnBtnFriendClick);
                SetLockBtnUI(btn_Friend);
            }
            else
            {
                btn_Friend.gameObject.SetActive(false);
            }
            debug.Log_yellow("当前拥有爱心_" + GameData.curLoveNum);
            debug.Log_yellow("累计获得爱心_" + GameData.accumulativeGetLoveNum);
            debug.Log_yellow("累计捐献爱心_" + GameData.curDonateLoveNum);
            //if (Application.platform == RuntimePlatform.WindowsEditor)
            //{
            //    GameData.curLoveNum = 100;
            //    GameData.accumulativeGetLoveNum = 100;
            //    GameData.curDonateLoveNum = 100;
            //}

            //爱心工坊：默认解锁 累计孵化<5颗爱心
            if (GameData.accumulativeGetLoveNum < 5 && GameData.curDonateLoveNum < 10)
            {
                btn_Love.onClick.AddListener(OnBtnLoveClick);
                SetLockBtnUI(btn_Love);
                btn_Donate.onClick.AddListener(OnBtnUnLockClick);
                btn_Honor.onClick.AddListener(OnBtnUnLockClick);
                btn_Game.onClick.AddListener(OnBtnUnLockClick);
                btn_Shop.onClick.AddListener(OnBtnUnLockClick);
                btn_Lucky.onClick.AddListener(OnBtnUnLockClick);
            }

            //希望灯塔：爱心工坊 累计孵化>=5颗爱心
            else if (GameData.accumulativeGetLoveNum >= 5 && GameData.curDonateLoveNum < 10)
            {
                btn_Love.onClick.AddListener(OnBtnLoveClick);
                SetLockBtnUI(btn_Love);
                btn_Donate.onClick.AddListener(OnBtnDonateClick);
                SetLockBtnUI(btn_Donate);
                btn_Honor.onClick.AddListener(OnBtnUnLockClick);
                btn_Game.onClick.AddListener(OnBtnUnLockClick);
                btn_Shop.onClick.AddListener(OnBtnUnLockClick);
                btn_Lucky.onClick.AddListener(OnBtnUnLockClick);
            }

            //荣誉展厅：希望灯塔 累计捐献>=10颗爱心
            else if (GameData.curDonateLoveNum >= 10 && GameData.curDonateLoveNum < 15)
            {
                btn_Love.onClick.AddListener(OnBtnLoveClick);
                SetLockBtnUI(btn_Love);
                btn_Donate.onClick.AddListener(OnBtnDonateClick);
                SetLockBtnUI(btn_Donate);
                btn_Honor.onClick.AddListener(OnBtnHonorClick);
                SetLockBtnUI(btn_Honor);
                btn_Game.onClick.AddListener(OnBtnUnLockClick);
                btn_Shop.onClick.AddListener(OnBtnUnLockClick);
                btn_Lucky.onClick.AddListener(OnBtnUnLockClick);
            }

            //游戏大厅：希望灯塔 累计捐献>=15颗爱心
            else if (GameData.curDonateLoveNum >= 15 && GameData.curDonateLoveNum < 20)
            {
                btn_Love.onClick.AddListener(OnBtnLoveClick);
                SetLockBtnUI(btn_Love);
                btn_Donate.onClick.AddListener(OnBtnDonateClick);
                SetLockBtnUI(btn_Donate);
                btn_Honor.onClick.AddListener(OnBtnHonorClick);
                SetLockBtnUI(btn_Honor);
                btn_Game.onClick.AddListener(OnBtnGameClick);
                SetLockBtnUI(btn_Game);
                btn_Shop.onClick.AddListener(OnBtnUnLockClick);
                btn_Lucky.onClick.AddListener(OnBtnUnLockClick);
            }



            //兑换商城：希望灯塔 累计捐献>=20颗爱心
            else if (GameData.curDonateLoveNum >= 20 && GameData.curDonateLoveNum < 25)
            {
                btn_Love.onClick.AddListener(OnBtnLoveClick);
                SetLockBtnUI(btn_Love);
                btn_Donate.onClick.AddListener(OnBtnDonateClick);
                SetLockBtnUI(btn_Donate);
                btn_Honor.onClick.AddListener(OnBtnHonorClick);
                SetLockBtnUI(btn_Honor);
                btn_Game.onClick.AddListener(OnBtnGameClick);
                SetLockBtnUI(btn_Game);
                btn_Shop.onClick.AddListener(OnBtnShopClick);
                SetLockBtnUI(btn_Shop);

                btn_Lucky.onClick.AddListener(OnBtnUnLockClick);
            }

            //幸运小屋：希望灯塔 累计捐献>=25颗爱心
            else if (GameData.curDonateLoveNum >= 25)
            {
                btn_Love.onClick.AddListener(OnBtnLoveClick);
                SetLockBtnUI(btn_Love);
                btn_Donate.onClick.AddListener(OnBtnDonateClick);
                SetLockBtnUI(btn_Donate);
                btn_Honor.onClick.AddListener(OnBtnHonorClick);
                SetLockBtnUI(btn_Honor);
                btn_Game.onClick.AddListener(OnBtnGameClick);
                SetLockBtnUI(btn_Game);
                btn_Shop.onClick.AddListener(OnBtnShopClick);
                SetLockBtnUI(btn_Shop);
                btn_Lucky.onClick.AddListener(OnBtnLuckyClick);
                SetLockBtnUI(btn_Lucky);
            }
        }

        void SetLockBtnUI(Button btn)
        {
            btn.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            btn.transform.GetChild(0).gameObject.SetActive(false);
            btn.transform.GetChild(1).gameObject.SetActive(false);
        }

        void OnBtnUnLockClick()
        {
            string btn_Name = EventSystem.current.currentSelectedGameObject.name;

            if (btn_Name == "btn_Love")
            {
            }
            else if (btn_Name == "btn_Donate")
            {
                GameTools.SetTip("<b>希望灯塔</b> 未解锁 \n 解锁条件：累计孵化>=5颗爱心");
            }
            else if (btn_Name == "btn_Honor")
            {
                GameTools.SetTip("<b>荣誉展馆</b> 未解锁 \n 解锁条件：累计捐献>=10颗爱心");
            }
            else if (btn_Name == "btn_Game")
            {
                GameTools.SetTip("<b>游戏大厅</b> 未解锁 \n 解锁条件：累计捐献>=15颗爱心");
            }
            else if (btn_Name == "btn_Shop")
            {
                GameTools.SetTip("<b>兑换商城</b> 未解锁 \n 解锁条件：累计捐献>=20颗爱心");
            }
            else if (btn_Name == "btn_Lucky")
            {
                GameTools.SetTip("<b>幸运小屋</b> 未解锁 \n 解锁条件：累计捐献>=25颗爱心");
            }
        }

    }








}