using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotUpdateScripts.Project.BasePrj.Data
{
    public class DataBShop
    {

    }

    /// <summary>
    /// 商品列表
    /// </summary>
    public class DataBShopListRes
    {
        public List<ConversionShop> conversionShopList { get; set; }
    }

    public class ConversionShop
    {
        //虚拟道具主键id
        public int productId { get; set; }
        //虚拟道具类型
        public int productType { get; set; }  //1:能量卡 2：加速卡 3：交友卡  4：爱心卡
        //虚拟道具名称
        public String productName { get; set; }
        //虚拟道具描述
        public String productDesc { get; set; }
        //活动期间兑换所需爱心币数量
        public int activityNeedLoveMoney { get; set; }
        //是否是活动商品 1 是 0不是
        public int isActivityProduct { get; set; }
        //已兑换数量
        public int exchangedNum { get; set; }
        //可兑换数量
        public int productNum { get; set; }
        //兑换商品所需要爱心币数量，
        public int needLoveMoney { get; set; }
        //是否是虚拟商品
        public int isVirtual { get; set; }   //0：虚拟商品  1：实物商品

        public string productPicture { get; set; }  //实物商品图片链接
    }

    /// <summary>
    /// 兑换记录
    /// </summary>
    public class DataBShopRecordListRes
    {
        public List<UserConversionRecord> conversionRecordList;
    }
    public class UserConversionRecord
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }
        //用户昵称
        public String nickName { get; set; }
        //虚拟道具id
        public int productId { get; set; }
        //虚拟道具名称
        public String productName { get; set; }
        //兑换时间
        public String coinTime { get; set; }
        //虚拟商品兑换数量
        public int conversionNum { get; set; }
        //是否是虚拟商品  0是虚拟  1不是虚拟（实物）
        public int isVirtual { get; set; }
        //兑换花费的爱心币数量
        public int spendLoveMoney { get; set; }
    }

    /// <summary>
    /// 兑换
    /// </summary>
    public class DataBShopExchangeReq
    {
        //用户id
        public long userId { get; set; }
        //虚拟道具id
        public int productId { get; set; }
        //虚拟商品兑换数量
        public int conversionNum { get; set; }
    }
}
