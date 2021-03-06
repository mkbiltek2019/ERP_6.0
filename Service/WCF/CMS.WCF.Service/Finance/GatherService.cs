﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Enum;
using ERP.Model.Finance;
using ERP.DAL.Finance.Interface;
using ERP.Environment;
using ERP.DAL.Finance;
using ERP.Model.Finance.Gathering;
using ERP.BLL.Implement;

namespace ERP.Service.Implement
{
    public partial class Service
    {
        private readonly static IGatheringDAL _gatheringDAL = new GatheringDAL(GlobalConfig.DB.FromType.Read);

        #region 收款单

        /// <summary>
        /// 获取收款单：往来单位收付款（收付款类型=收款单、劳务类型）
        /// 功能点：ERP往来单位收/付款=》完成
        /// 业务点：往来收款（包含劳务类型）
        /// 数据库：lmshop_CompanyFundReceipt
        /// </summary>
        /// <returns></returns>
        public IList<CompanyFundReceiptDTO> GetGathering_CompanyFundReceipt(DateTime StartTime, DateTime EndTime)
        {
            return _gatheringDAL.GetGathering_CompanyFundReceipt(StartTime, EndTime);
        }

        /// <summary>
        /// 获取收款单：费用申报（申报类型=费用收入）
        /// 功能点：ERP费用申报
        /// 业务点：费用收入
        /// 数据库：lmShop_CostReport
        /// </summary>
        /// <returns></returns>
        public IList<CostReportDTO> GetGathering_CostReport(DateTime StartTime, DateTime EndTime)
        {
            return _gatheringDAL.GetGathering_CostReport(StartTime, EndTime);
        }

        /// <summary>
        /// 获取收款单：资金流（转账）。百秀兰溪》上海百秀旗舰店转账。
        /// 功能点：ERP资金流=》转账
        /// 业务点：上海百秀旗舰店天猫帮忙卖 百秀兰溪商品（财务手工做一次转账-ERP资金流转账）
        /// 数据库：lmShop_WasteBook
        /// </summary>
        /// <returns></returns>
        public IList<WasteBookDTO> GetGathering_WasteBook(DateTime StartTime, DateTime EndTime)
        {
            string LogisticsCompany = ConfigManage.GetConfigValue("BaiXiu_Lanxi_ID");//物流公司
            string SalesCompany = ConfigManage.GetConfigValue("BaiXiu_Shanghai_ID");//销售公司
            return _gatheringDAL.GetGathering_WasteBook(StartTime, EndTime, LogisticsCompany, SalesCompany);
        }

        /// <summary>
        /// 获取收款单：客户付款-款到发货（排除 补偿、赠送 ）、余额支付（排除 补偿、赠送 ）
        /// 功能点：ERP自动任务（完成订单）注释：WMS完成发货时向异步订单完成表中插入一条订单信息，ERP完成订单自动任务
        /// 业务点：销售公司、卖给客户=》客户付款-款到发货（排除 补偿、赠送 ）
        /// 数据库：lmshop_GoodsOrder
        /// </summary>
        /// <returns></returns>
        public IList<GoodsOrderDTO> GetGathering_GoodsOrder(DateTime StartTime, DateTime EndTime)
        {
            return _gatheringDAL.GetGathering_GoodsOrder(StartTime, EndTime);
        }

        #endregion 收款单

        #region 收款单（负数）

        /// <summary>
        /// 获取收款单（负数)：商品检查
        /// 功能点：ERP资金流=》转账
        /// 业务点：上海百秀旗舰店天猫帮忙卖 百秀兰溪商品（财务手工做一次转账-ERP资金流转账）
        /// 数据库：调用加盟店的wcf服务。 GetReturnMoneyByOrderId(Guid orderId);
        /// </summary>
        /// <returns></returns>
        public IList<GatheringDTO> GetGathering_ReturnMoney(DateTime StartTime, DateTime EndTime)
        {
            return _gatheringDAL.GetGathering_ReturnMoney(StartTime, EndTime);
        }

        #endregion 收款单（负数）
        
    }
}