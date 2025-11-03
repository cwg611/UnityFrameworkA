using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;
using System.Collections;

namespace My.UI.Panel
{
    public class UIBLoveWindowPanel : BasePanel
    {
        private Button btn_Close;
        private GameObject pan;
        private RectTransform BgContent;
        private GameObject Icon;
        private Text DonateNum, MaxNum;
        private GameObject btn_minus, btn_add;
        private GameObject Btn_Submit, Btn_Cancel;
        private Text txt_Desc;
        private PrizeStore m_Data;
        private int m_UseNum = 1;
        private Material Mat_Grey;

        void Awake()
        {
            BgContent = GameTools.GetByName(transform, "BgContent").GetComponent<RectTransform>();
            DOTweenMgr.Instance.OpenWindowScale(BgContent.gameObject, .3f);
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            pan = GameTools.GetByName(transform, "pan");
            Icon = GameTools.GetByName(transform, "Icon");
            DonateNum = GameTools.GetByName(transform, "DonateNum").GetComponent<Text>();
            MaxNum = GameTools.GetByName(transform, "MaxNum").GetComponent<Text>();
            btn_minus = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_minus"), () =>
            {
                debug.Log_yellow("减去 ");
                if (m_UseNum > 1)
                {
                    m_UseNum -= 1;
                    setNumView();
                }
            });

            btn_add = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_add"), () =>
            {
                debug.Log_yellow("增加 ");
                if (GameData.cardType == CardType.AcculateCard)
                {
                    debug.Log_purple("时间差：" + GameData.CurTimeDistance);
                    if (m_UseNum * 60 <= GameData.CurTimeDistance)
                    {
                        if (m_UseNum < m_Data.productNum)
                            m_UseNum += 1;
                    }
                }
                else
                {
                    if (m_UseNum < m_Data.productNum)
                        m_UseNum += 1;
                }

                setNumView();
            });

            txt_Desc = GameTools.GetByName(transform, "txt_Desc").GetComponent<Text>();

            Btn_Submit = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Submit"), () =>
            {
                if (GameData.cardType == CardType.AcculateCard)
                {
                    //判断加速卡使用时，会不会超出孵化时间
                    if (m_UseNum * 60 > GameData.CurTimeDistance)
                    {
                        //如果选择的数量超出，则自行计算需要使用的数量
                        m_UseNum = GameData.CurTimeDistance / 60 + 1;
                    }
                    debug.Log_Blue("使用了几张加速卡： " + m_UseNum);
                }

                DataMgr.Instance.dataBUseBagCard.userId = GameData.userId;
                DataMgr.Instance.dataBUseBagCard.productId = m_Data.productId;
                DataMgr.Instance.dataBUseBagCard.productNum = m_UseNum;
                NetMgr.Instance.C2S_BagCard_UserCard();
            });

            Btn_Cancel = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Cancel"), OnBtnClockA);

            btn_Close.onClick.AddListener(OnBtnClockA);

            Mat_Grey = btn_minus.GetComponent<Image>().material;

            MsgCenter.RegisterMsg(null, MsgCode.S2C_BagCard_UserCard, S2C_BagCard_UserCardCallBack);
        }

        void Start()
        {
            DonateNum.text = "×" + m_UseNum;
            SetView();
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_BagCard_UserCard, S2C_BagCard_UserCardCallBack);
        }

        void SetView()
        {
            m_Data = GameData.GetBagCardDataByCardType(GameData.cardType);
            MaxNum.text = m_Data.productNum + " 张";
            if (m_Data == null) return;
            //图标
            GameTools.SetDaoJuUIIcon(m_Data.productId, Icon.GetComponent<Image>());
            //描述
            txt_Desc.text = getProductDesc(m_Data.productDesc); 
            txt_Desc.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            LayoutRebuilder.ForceRebuildLayoutImmediate(BgContent);
            setNumView();
        }

        string getProductDesc(string s)
        {
            string desc = "";
            string[] result = s.Split('^');
            desc += result[1] + "\n" + result[2];
            return desc;
        }

        void setNumView()
        {
            DonateNum.text = "×" + m_UseNum;
            if (m_UseNum > 1)
            {
                btn_minus.GetComponent<Image>().material = null;
            }
            else
            {
                btn_minus.GetComponent<Image>().material = Mat_Grey;
            }
            if (m_UseNum >= m_Data.productNum)
            {
                btn_add.GetComponent<Image>().material = Mat_Grey;
            }
            else
            {
                btn_add.GetComponent<Image>().material = null;
            }
        }

        void OnBtnClockA()
        {
            DOTweenMgr.Instance.CloseWindowScale(BgContent.gameObject, 0.2f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBLoveWindowPanel);
            });
        }

        /// <summary>
        /// 使用道具服务器返回
        /// </summary>
        /// <param name="o"></param>
        void S2C_BagCard_UserCardCallBack(object o)
        {
            debug.Log_Blue("使用道具卡成功");
            if (GameData.cardType == CardType.AcculateCard)
            {
                GameData.isUseCard = true;
            }
            OnBtnClockA();
            //刷新爱心工坊首页数据
            NetMgr.Instance.C2S_Love_GetChargeTime(GameData.userId.ToString());
            //StartCoroutine(DelayGetChargeTime());
            
        }
        IEnumerator DelayGetChargeTime()
        {
            yield return new WaitForSeconds(0.5f);
            OnBtnClockA();
            //刷新爱心工坊首页数据
            NetMgr.Instance.C2S_Love_GetChargeTime(GameData.userId.ToString());
        }

    }
}
