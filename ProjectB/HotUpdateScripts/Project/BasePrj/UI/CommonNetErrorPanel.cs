using HotUpdateScripts.Project.Common;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class CommonNetErrorPanel : BasePanel
    {
        private Button Ok;
        private Text text_Desc;

        void Awake()
        {
            text_Desc = GameTools.GetByName(transform, "text_Desc").GetComponent<Text>();

            Ok = GameTools.GetByName(transform, "Ok").GetComponent<Button>();
            Ok.onClick.AddListener(() =>
            {
                NetMgr.Instance.ReConnect();
            });
        }

        public override void InitPanel(object o)
        {
            text_Desc.text = o.ToString();
        }
    }
}
