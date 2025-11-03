using HotUpdateScripts.Project.GameA.UI;
using JEngine.Core;
using JEngine.UI;
using JEngine.UI.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.Project.Common
{
    public class UIInit : JBehaviour
    {
        private static string UIAPrefabPath = "UIAPrefab/";


        #region UI声明界面
        //GameA
        public static string UIADonatePagePanelPath = UIAPrefabPath + "UIADonatePagePanel";
        public static string UIADonateDetailPagePanelPath = UIAPrefabPath + "UIADonateDetailPagePanel";
        public static string UIADonatePageItemPath = UIAPrefabPath + "UIADonatePageItem";
        public static string UIAGamePagePanelPath = UIAPrefabPath + "UIAGamePagePanel";
        public static string UIAHomePagePanelPath = UIAPrefabPath + "UIAHomePagePanel";
        public static string UIAHonorPagePanelPath = UIAPrefabPath + "UIAHonorPagePanel";
        public static string UIARankAPagePanelPath = UIAPrefabPath + "UIARankAPagePanel";
        public static string UIAExcShopPagePanelPath = UIAPrefabPath + "UIAExcShopPagePanel";
        public static string UIATaskPagePanelPath = UIAPrefabPath + "UIATaskPagePanel";
        public static string UIATaskPageItemPath = UIAPrefabPath + "UIATaskPageItem";

        //GameB

        #endregion

        private static bool Inited = false;

        public override void Init()
        {
            if (!Inited)
            {
                //UIMgr注册界面（需要的）
                UIMgr.Instance.Register(

                    (UIADonatePagePanelPath, UIADonatePagePanel.Instance),
                    (UIADonateDetailPagePanelPath, UIADonateDetailPagePanel.Instance),
                    (UIAGamePagePanelPath, UIAGamePagePanel.Instance),
                    (UIAHomePagePanelPath, UIAHomePagePanel.Instance),
                    (UIAHonorPagePanelPath, UIAHonorPagePanel.Instance),
                    (UIARankAPagePanelPath, UIARankAPagePanel.Instance),
                    (UIAExcShopPagePanelPath, UIAExcShopPagePanel.Instance),
                    (UIATaskPagePanelPath, UIATaskPagePanel.Instance)

                    );
                Inited = true;
            }

            //初始化UIRoot是需要的
            UIRootView.InitUIRoot();
            UIMgr.Instance.ShowUI(UIAHomePagePanelPath);
        }
    }
}
