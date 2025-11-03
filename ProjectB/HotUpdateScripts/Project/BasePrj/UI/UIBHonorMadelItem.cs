using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;


namespace My.UI.Panel
{
    public class UIBHonorMadelItem : MonoBehaviour
    {
        private Image Icon;
        private GameObject blackMask;
        private Image titleBg;
        private Text Title;
        private Image block;

        private bool isFirst = true;

        private GameObject[] shelfs;

        private bool m_status; //记录解锁状态

        private HonorMedal m_Data;

        void setObj()
        {
            shelfs = new GameObject[3] { GameTools.GetByName(transform, "shelfOne"), GameTools.GetByName(transform, "shelfTwo"), GameTools.GetByName(transform, "shelfThree") };
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            Title = GameTools.GetByName(transform, "Title").GetComponent<Text>();
            block = GameTools.GetByName(transform, "block").GetComponent<Image>();
            blackMask = GameTools.GetByName(transform, "blackMask");

            titleBg = GameTools.GetByName(transform, "titleBg").GetComponent<Image>();

            Icon.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (m_status)
                {
                    UIBHonorHomePanel.OpenUnLockWindow(m_Data);
                }
                else
                {
                    UIBHonorHomePanel.OpenLockWindow(m_Data);
                }
            });

        }
        public void setMadelView(HonorMedal data, int index, bool status)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            m_Data = data;
            m_status = status;

            Title.text = data.medalTitle;

            blackMask.SetActive(!status);

            UIBHonorHomePanel.ModelQieHuan.SetImg(data.medalId - 1, Icon.gameObject);

            //设置锁
            if (status)
            {
                block.gameObject.SetActive(false);
                titleBg.color = new Color(31 / 255f, 165 / 255f, 234 / 255f);
            }
            else
            {
                block.gameObject.SetActive(true);
                titleBg.color = new Color(139 / 255f, 141 / 255f, 142 / 255f);
                UIBHonorHomePanel.imgQiehuan.SetImg(2, block.gameObject);
            }
            //设置货架板
            setShelf(index);
        }

        void setShelf(int index)
        {
            for (int i = 0; i < shelfs.Length; i++)
            {
                if (i == (index - 1)) shelfs[i].SetActive(true);
                else shelfs[i].SetActive(false);
            }
        }
    }
}
