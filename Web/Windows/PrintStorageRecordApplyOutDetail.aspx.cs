﻿using System;
using System.Globalization;
using System.Linq;
using ERP.BLL.Implement.Inventory;
using ERP.DAL.Implement.Inventory;
using ERP.DAL.Interface.IInventory;
using ERP.Enum;
using ERP.Environment;
using ERP.Model;
using ERP.SAL;
using ERP.SAL.Goods;
using ERP.SAL.Interface;
using ERP.UI.Web.Base;
using ERP.UI.Web.Common;
using Keede.Ecsoft.Model;
using Telerik.Web.UI;

namespace ERP.UI.Web.Windows
{
    /// <summary>出库打印
    /// </summary>
    public partial class PrintStorageRecordApplyOutDetail : System.Web.UI.Page
    {

        static readonly IGoodsCenterSao _goodsCenterSao = new GoodsCenterSao();
        static readonly IStorageRecordDao _storageRecordDao = new StorageRecordDao(GlobalConfig.DB.FromType.Read);
        public StorageRecordInfo StorageRecordInfo = new StorageRecordInfo();
        static readonly ICompanyCussent _companyCussent = new CompanyCussent(GlobalConfig.DB.FromType.Read);
        static readonly StorageManager _storageManager = new StorageManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StorageRecordInfo = _storageRecordDao.GetStorageRecord(StockId);

                if (StorageRecordInfo.StockType == (int)StorageRecordType.SellStockOut)
                {
                    RGGoods.MasterTableView.Columns.FindByUniqueName("UnitPrice").Visible = false;
                    RGGoods.MasterTableView.Columns.FindByUniqueName("TotalPrice").Visible = false;
                }
            }
        }

        #region 列表帮助方法
        protected string GetFilialeName(Guid warehouseId, int storageType, Guid filialeId)
        {
            var str = string.Empty;
            var personinfo = CurrentSession.Personnel.Get();

            var wList = WMSSao.GetWarehouseAuth(personinfo.PersonnelId).FirstOrDefault(p => p.WarehouseId == warehouseId);
            if (wList != null)
            {
                var slist = wList.Storages.FirstOrDefault(p => p.StorageType == storageType);
                if (slist != null)
                {
                    var hlist = slist.Filiales.FirstOrDefault(p => p.HostingFilialeId == filialeId);
                    str = hlist == null ? "" : hlist.HostingFilialeName;
                }
            }
            return str;
        }

        public string GetFilialeNameA(Guid filialeId)
        {
            var info = CacheCollection.Filiale.Get(filialeId) ?? new FilialeInfo();
            return info.Name;
        }

        protected string GetCompanyName(Guid companyId)
        {
            var info = _companyCussent.GetCompanyCussent(companyId) ?? new CompanyCussentInfo();
            return info.CompanyName;
        }

        protected string GetPrintTime()
        {
            string week = "星期" + DateTime.Now.ToString("ddd", new CultureInfo("zh-cn"));
            var printTime = DateTime.Now.ToString("yyyy年MM月dd日") + " " + week.Replace("周", "") + " " + DateTime.Now.ToString("HH:mm:ss");
            return printTime;
        }

        protected string GetWarehouse(Guid warehouseId, int storageType)
        {
            string str = string.Empty;
            var personinfo = CurrentSession.Personnel.Get();
            var wList = WMSSao.GetWarehouseAuth(personinfo.PersonnelId).FirstOrDefault(p => p.WarehouseId == warehouseId);
            if (wList != null)
            {
                str = wList.WarehouseName;
                var slist = wList.Storages.FirstOrDefault(p => p.StorageType == storageType);
                if (slist != null)
                {
                    str += " " + slist.StorageTypeName;
                }
            }
            return str;
        }
        #endregion

        protected void RgGoodsNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var list = _storageManager.GetStorageRecordDetailListByStockId(StockId).ToList();
            var realGoodsIds = list.Select(w => w.RealGoodsId).Distinct().ToList();
            //根据商品id获得商品信息
            var dicGoods = _goodsCenterSao.GetGoodsBaseListByGoodsIdOrRealGoodsIdList(realGoodsIds);
            foreach (var info in list)
            {
                if (dicGoods != null && dicGoods.Count > 0)
                {
                    bool hasKey = dicGoods.ContainsKey(info.RealGoodsId);
                    if (hasKey)
                    {
                        var goodsInfo = dicGoods.FirstOrDefault(w => w.Key == info.RealGoodsId).Value;
                        info.Units = goodsInfo.Units;
                    }
                }
                info.Quantity = Math.Abs(info.Quantity);
            }
            RGGoods.DataSource = list.OrderBy(ent => ent.GoodsName).ThenBy(ent => ent.Specification);
            IsShowPrice();
        }

        private void IsShowPrice()
        {
            td_TotalTitel.Visible = IsPrintPrice;
            td_TotalPrice.Visible = IsPrintPrice;
            RGGoods.MasterTableView.Columns.FindByDataField("UnitPrice").Visible = IsPrintPrice;
            RGGoods.MasterTableView.Columns.FindByUniqueName("TotalPrice").Visible = IsPrintPrice;
        }

        #region 模型
        private Guid StockId
        {
            get
            {
                try
                {
                    if (Request.QueryString["StockId"] == null)
                        throw new ApplicationException("温馨提示：打印出错，请联系技术人员！");
                    return new Guid(Request.QueryString["StockId"]);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        private bool IsPrintPrice
        {
            get
            {
                try
                {
                    var isPrintPrice = Request.QueryString["IsPrintPrice"];
                    if (Request.QueryString["IsPrintPrice"] == null)
                        throw new ApplicationException("温馨提示：打印出错，请联系技术人员！");
                    return isPrintPrice == "1";// 1打印显示价格 0不显示价格
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }
        #endregion
    }
}