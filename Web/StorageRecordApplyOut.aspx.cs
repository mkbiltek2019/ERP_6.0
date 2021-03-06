﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ERP.BLL.Implement.Inventory;
using ERP.DAL.Factory;
using ERP.DAL.Implement.Inventory;
using ERP.DAL.Interface.IInventory;
using ERP.DAL.Interface.IStorage;
using ERP.Enum;
using ERP.Enum.Attribute;
using ERP.Environment;
using ERP.Model;
using ERP.SAL;
using ERP.SAL.WMS;
using ERP.UI.Web.Base;
using ERP.UI.Web.Common;
using Framework.Common;
using Keede.Ecsoft.Model;
using OperationLog.Core;
using Telerik.Web.UI;
using WebControl = ERP.UI.Web.Common.WebControl;

namespace ERP.UI.Web
{
    /// <summary>
    /// 出库申请
    /// </summary>
    public partial class StorageRecordApplyOut : BasePage
    {
        static readonly IStorageRecordDao _storageRecordDao = new StorageRecordDao(GlobalConfig.DB.FromType.Write);
        static readonly IBorrowLendDao _borrowLendDao = OrderInstance.GetBorrowLendDao(GlobalConfig.DB.FromType.Write);
        static readonly StorageManager _storageManager = new StorageManager();
        static readonly ICompanyCussent _companyCussent = new CompanyCussent(GlobalConfig.DB.FromType.Read);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYears();
                BindWarehouse();
                LoadStorageState();
                BindStorageType();
                BindThirdCompany();
            }
        }

        public Dictionary<Guid,string> CompanyCussentList
        {
            get {
                if (ViewState["CompanyCussentList"] != null)
                    return (Dictionary<Guid, string>)ViewState["CompanyCussentList"];
                var wList = _companyCussent.GetCompanyCussentList(State.Enable);
                wList.Insert(0,new CompanyCussentInfo() { CompanyId=Guid.Empty,CompanyName=""});
                var dics= wList.ToDictionary(k => k.CompanyId, v => v.CompanyName);
                ViewState["CompanyCussentList"] = dics;
                return dics;
            }
            set { ViewState["CompanyCussentList"] = value; }
        }

        #region 下拉框选择事件
        /// <summary>入库仓Changed事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DDLWaerhouse_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Guid warehouseId = string.IsNullOrEmpty(DDL_Waerhouse.SelectedValue)
               ? Guid.Empty
               : new Guid(DDL_Waerhouse.SelectedValue);

            //绑定入库储
            var list = new List<StorageAuth>();
            var slist = CurrentSession.Personnel.WarehouseList;
            if (slist != null && slist.Count > 0)
            {
                var warehouse = slist.FirstOrDefault(act => act.WarehouseId == warehouseId);
                if (warehouse != null && warehouse.Storages != null)
                    list = warehouse.Storages;
            }

            DDL_StorageAuth.DataSource = list;
            DDL_StorageAuth.DataTextField = "StorageTypeName";
            DDL_StorageAuth.DataValueField = "StorageType";
            DDL_StorageAuth.DataBind();
            DDL_StorageAuth.Items.Insert(0, new ListItem("请选择", "0"));
            DDL_StorageAuth.SelectedValue = list.Count == 1 ? list.First().StorageType.ToString() : "0";

            if (list.Count == 1)
            {
                var storage = list.First();
                DDL_StorageAuth.SelectedValue = storage.StorageType.ToString();
                var saleAndHostingFilialeList = MISService.GetAllSaleAndHostingFilialeList();
                DDL_HostingFilialeAuth.DataSource = saleAndHostingFilialeList;
                DDL_HostingFilialeAuth.DataTextField = "Name";
                DDL_HostingFilialeAuth.DataValueField = "Id";
                DDL_HostingFilialeAuth.DataBind();
                DDL_HostingFilialeAuth.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));

                if (saleAndHostingFilialeList.Count == 1)
                {
                    DDL_HostingFilialeAuth.SelectedValue = saleAndHostingFilialeList.First().ID.ToString();
                    DdlHostingFilialeAuthChanged(MISService.GetAllFiliales().Where(ent => ent.ParentId == saleAndHostingFilialeList.First().ID));
                }
                else
                {
                    DdlHostingFilialeAuthChanged(new List<FilialeInfo>());
                    DDL_HostingFilialeAuth.SelectedValue = Guid.Empty.ToString();
                }
            }
            else
            {
                DDL_StorageAuth.SelectedValue = "0";

                DDL_HostingFilialeAuth.DataSource = new List<HostingFilialeAuth>();
                DDL_HostingFilialeAuth.DataBind();

                DdlHostingFilialeAuthChanged(new List<FilialeInfo>());
            }

            RG_StorageRecord.Rebind();
        }

        /// <summary>入库储Changed事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DDLStorageAuth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //仓库id
            Guid warehouseId = string.IsNullOrEmpty(DDL_Waerhouse.SelectedValue)
                ? Guid.Empty
                : new Guid(DDL_Waerhouse.SelectedValue);
            //储位id
            byte storageType = string.IsNullOrEmpty(DDL_StorageAuth.SelectedValue)
                ? default(byte)
                : byte.Parse(DDL_StorageAuth.SelectedValue);

            //绑定物流配送公司
            var list = new List<HostingFilialeAuth>();
            var wlist = CurrentSession.Personnel.WarehouseList;
            if (wlist != null)
            {
                var warehouse = wlist.FirstOrDefault(act => act.WarehouseId == warehouseId);
                if (warehouse != null && warehouse.Storages != null)
                {
                    var storageTypeAuth = warehouse.Storages.FirstOrDefault(p => p.StorageType == storageType);
                    if (storageTypeAuth != null && storageTypeAuth.Filiales != null)
                    {
                        list.AddRange(storageTypeAuth.Filiales);
                    }
                }
            }

            var saleAndHostingFilialeList = MISService.GetAllSaleAndHostingFilialeList();
            DDL_HostingFilialeAuth.DataSource = saleAndHostingFilialeList;
            DDL_HostingFilialeAuth.DataTextField = "Name";
            DDL_HostingFilialeAuth.DataValueField = "Id";
            DDL_HostingFilialeAuth.DataBind();
            DDL_HostingFilialeAuth.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));
            DDL_HostingFilialeAuth.SelectedValue = Guid.Empty.ToString();

            var filialeList = MISService.GetAllFiliales().ToList();
            var dataFiliale = new List<FilialeInfo>();
            foreach (var filialeId in list)
            {
                dataFiliale.AddRange(filialeList.Where(w => w.ParentId == filialeId.HostingFilialeId));
            }
            DdlHostingFilialeAuthChanged(dataFiliale);

            RG_StorageRecord.Rebind();
        }

        /// <summary>物流配送公司Changed事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DDLHostingFilialeAuth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Guid hostingFilialeId = string.IsNullOrEmpty(DDL_HostingFilialeAuth.SelectedValue)
                  ? Guid.Empty
                  : new Guid(DDL_HostingFilialeAuth.SelectedValue);
            var filialeList = MISService.GetAllFiliales().ToList();
            DdlHostingFilialeAuthChanged(hostingFilialeId != Guid.Empty ? filialeList.Where(act => act.ParentId == hostingFilialeId) : new List<FilialeInfo>());
            RG_StorageRecord.Rebind();
        }


        private void DdlHostingFilialeAuthChanged(IEnumerable<FilialeInfo> dataFiliale)
        {
            var dics = dataFiliale != null && dataFiliale.Any() ? dataFiliale.ToDictionary(dic => dic.ID, dic => dic.Name) : new Dictionary<Guid, string>();
            DDL_SaleFiliale.DataSource = dics;
            DDL_SaleFiliale.DataTextField = "Value";
            DDL_SaleFiliale.DataValueField = "Key";
            DDL_SaleFiliale.DataBind();
            DDL_SaleFiliale.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));
            DDL_SaleFiliale.SelectedIndex = 0;
        }

        protected void DdlYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            YearsSelectedIndex();
        }

        private void YearsSelectedIndex()
        {
            if (DDL_Years.SelectedValue == "0")
            {
                RDP_StartTime.SelectedDate = DateTime.Now.AddDays(-40);
                RDP_EndTime.SelectedDate = DateTime.Now;
            }
            else
            {
                RDP_StartTime.SelectedDate = DateTime.Parse(DDL_Years.SelectedValue + "-01-01");
                RDP_EndTime.SelectedDate = DateTime.Parse(DDL_Years.SelectedValue + "-12-31");
            }
        }
        #endregion

        #region 列表帮助方法
        protected string GetStockType(int stockType)
        {
            return EnumAttribute.GetKeyName((StorageRecordType)stockType);
        }

        protected string GetCompany(Guid companyId, byte storageType)
        {
            if (companyId != Guid.Empty)
            {
                var companyCussentInfo = _companyCussent.GetCompanyCussent(companyId);
                if (companyCussentInfo != null && companyCussentInfo.CompanyId != Guid.Empty)
                {
                    return companyCussentInfo.CompanyName;
                }
                return CacheCollection.Filiale.GetName(companyId);
                
            }
            return "-";
        }

        //protected string GetByPurchasingNo(object obj)
        //{
        //    if (obj == null)
        //        return "";
        //    var pid = new Guid(obj.ToString());
        //    if (Guid.Empty != pid)
        //    {
        //        var purchsingInfo = _purchasing.GetPurchasingById(pid);
        //        if (purchsingInfo != null)
        //            return purchsingInfo.PurchasingNo;

        //        return "";
        //    }
        //    return "";
        //}

        protected string GetStockState(int stockState)
        {
            return EnumAttribute.GetKeyName((StorageRecordState)stockState);
        }
        #endregion

        protected void StockGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            IList<StorageRecordInfo> storageRecordList = new List<StorageRecordInfo>();
            long recordCount = 0;
            var startPage = RG_StorageRecord.CurrentPageIndex + 1;
            int pageSize = RG_StorageRecord.PageSize;
            var warehouseId = new Guid(DDL_Waerhouse.SelectedValue);
            var storageType = string.IsNullOrEmpty(DDL_StorageAuth.SelectedValue)
              ? default(Int32)
              : int.Parse(DDL_StorageAuth.SelectedValue);
            var hostingFilialeId = string.IsNullOrEmpty(DDL_HostingFilialeAuth.SelectedValue)
                ? Guid.Empty
                : new Guid(DDL_HostingFilialeAuth.SelectedValue);
            var thirdCompanyId = string.IsNullOrEmpty(RcbThirdCompany.SelectedValue)
                ? Guid.Empty
                : new Guid(RcbThirdCompany.SelectedValue);

            var shopId = string.IsNullOrEmpty(DDL_SaleFiliale.SelectedValue)
                ? Guid.Empty
                : new Guid(DDL_SaleFiliale.SelectedValue);

            DateTime startTime = RDP_StartTime.SelectedDate ?? DateTime.MinValue;
            DateTime endTime = RDP_EndTime.SelectedDate ?? DateTime.Now;

            var storageRecordTypes = new List<StorageRecordType>();
            var storageRecordStates = new List<StorageRecordState>();
            if (!string.IsNullOrEmpty(DDL_StorageType.SelectedValue))
            {
                storageRecordTypes.Add((StorageRecordType)Convert.ToInt32(DDL_StorageType.SelectedValue));
            }
            else
            {
                storageRecordTypes.Add(StorageRecordType.LendOut);
                storageRecordTypes.Add(StorageRecordType.AfterSaleOut);
                storageRecordTypes.Add(StorageRecordType.BuyStockOut);
                storageRecordTypes.Add(StorageRecordType.SellStockOut);
                storageRecordTypes.Add(StorageRecordType.BorrowOut);
                storageRecordTypes.Add(StorageRecordType.InnerPurchase);
            }
            if (!string.IsNullOrEmpty(DD_StorageState.SelectedValue))
            {
                storageRecordStates.Add((StorageRecordState)Convert.ToInt32(DD_StorageState.SelectedValue));
            }
            else
            {
                storageRecordStates.Add(StorageRecordState.WaitAudit);
                storageRecordStates.Add(StorageRecordState.Refuse);
                storageRecordStates.Add(StorageRecordState.Approved);
                storageRecordStates.Add(StorageRecordState.Finished);
                storageRecordStates.Add(StorageRecordState.Canceled);
            }

            if (warehouseId != Guid.Empty)
            {
                storageRecordList = _storageRecordDao.GetStorageRecordListToPages(warehouseId, shopId!=Guid.Empty?shopId:thirdCompanyId, 
                    String.Empty, RTB_No.Text, storageRecordTypes, storageRecordStates, storageType, hostingFilialeId, startTime, endTime, 0, GlobalConfig.KeepYear, startPage, pageSize, out recordCount);
            }
            RG_StorageRecord.DataSource = storageRecordList;
            RG_StorageRecord.VirtualItemCount = (int)recordCount;
        }

        /// <summary>搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LB_Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DDL_Waerhouse.SelectedValue) || DDL_Waerhouse.SelectedValue == Guid.Empty.ToString())
            {
                RAM.Alert("请选择仓库！");
                return;
            }
            RG_StorageRecord.CurrentPageIndex = 0;
            RG_StorageRecord.Rebind();
        }

        protected void RamAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            WebControl.RamAjajxRequest(RG_StorageRecord, e);
        }

        /// <summary>Grid操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SemiStockGrid_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item != null)
            {
                var dataItem = item;
                var stockId = new Guid(dataItem.GetDataKeyValue("StockId").ToString());
                var storageInfo = _storageRecordDao.GetStorageRecord(stockId);

                //作废
                if (e.CommandName == "Cancellation")
                {
                    #region --> Cancellation
                    var personnelInfo = CurrentSession.Personnel.Get();

                    if (storageInfo.StockType == (int)StorageRecordType.LendOut)
                    {
                        string description = "[借出单作废] " + personnelInfo.RealName + "[" +
                                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";

                        var borrowLendInfo = _borrowLendDao.GetBorrowLendInfo(stockId);
                        if (borrowLendInfo != null)
                        {
                            var outStorageInfo = _storageRecordDao.GetStorageRecord(borrowLendInfo.BorrowLendId);
                            if (outStorageInfo != null)
                            {
                                //作废借出返回单
                                _storageManager.NewSetStateStorageRecord(outStorageInfo.StockId,
                                    StorageRecordState.Canceled,
                                    description);
                            }
                        }
                        //作废借出单
                        _storageManager.NewSetStateStorageRecord(stockId, StorageRecordState.Canceled, description);

                        //入库单作废操作记录添加
                        WebControl.AddOperationLog(personnelInfo.PersonnelId, personnelInfo.RealName, stockId,
                            storageInfo.TradeCode,
                            OperationPoint.StorageInManager.Cancel.GetBusinessInfo(), string.Empty);
                    }
                    else
                    {
                        string description = "[出库作废] " + CurrentSession.Personnel.Get().RealName + "[" +
                                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                        _storageManager.NewSetStateStorageRecord(stockId, StorageRecordState.Canceled, description);
                        //入库单作废操作记录添加
                        WebControl.AddOperationLog(personnelInfo.PersonnelId, personnelInfo.RealName, stockId,
                            storageInfo.TradeCode,
                            OperationPoint.StorageInManager.Cancel.GetBusinessInfo(), string.Empty);
                    }

                    #endregion
                }
                else if (e.CommandName == "Validation")
                {
                    //售后确认
                    #region  -->Validation

                    StorageRecordInfo storageRecordInfo = _storageRecordDao.GetStorageRecord(stockId);
                    var stockValidation = (CheckBox)dataItem.FindControl("CB_StockValidation");
                    bool stockValidationType = stockValidation.Checked;
                    _storageRecordDao.UpdateStorageRecordDetailByStockId(stockId, stockValidationType);

                    var personnelInfo = CurrentSession.Personnel.Get();
                    //出库确认添加操作记录添加
                    WebControl.AddOperationLog(personnelInfo.PersonnelId, personnelInfo.RealName, stockId, storageRecordInfo.TradeCode, OperationPoint.StorageOutManager.Confirm.GetBusinessInfo(), string.Empty);

                    #endregion
                }

                RAM.ResponseScripts.Add("setTimeout(function(){ refreshGrid(); }, " + GlobalConfig.PageAutoRefreshDelayTime + ");");
            }
        }

        protected void SemiStockGrid_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var dataItem = (GridDataItem)e.Item;

                var stockType = int.Parse(dataItem.GetDataKeyValue("StockType").ToString());
                var stockState = int.Parse(dataItem.GetDataKeyValue("StockState").ToString()); //RelevanceFilialeId
                var thirdCompanyId = new Guid(dataItem.GetDataKeyValue("ThirdCompanyID").ToString());
                var ibtnDelete = (ImageButton)dataItem.FindControl("IB_Delete");
                var ibtnModifyOrder = (ImageButton)dataItem.FindControl("IB_ModifyOrder");
                var ibtnApproved = (ImageButton)dataItem.FindControl("IB_Approved");
                var btnModifyOrder = (Button)dataItem.FindControl("btn_ModifyOrder");
                //当单据类型为借出返回时，不能作废，修改
                if (stockType == (int)StorageRecordType.BorrowOut)
                {
                    ibtnDelete.Visible = false;
                    ibtnModifyOrder.Visible = false;
                    var linkTradeId = new Guid(dataItem.GetDataKeyValue("LinkTradeID").ToString());
                    var storageRecordInfo = _storageRecordDao.GetStorageRecord(linkTradeId);
                    if (stockState == (int)StorageRecordState.WaitAudit && storageRecordInfo.StockState == (int)StorageRecordState.Finished)
                    {
                        ibtnApproved.Visible = GetPowerOperationPoint("BorrowOutApprove");
                    }
                    else
                    {
                        ibtnApproved.Visible = false;
                    }
                    if (stockState == (int)StorageRecordState.Refuse && storageRecordInfo.StockState == (int)StorageRecordState.Finished)
                    {
                        btnModifyOrder.Visible = true;
                    }
                    else
                    {
                        btnModifyOrder.Visible = false;
                    }
                    return;
                }
                var normal = _companyCussent.GetCompanyIdByRelevanceFilialeId(thirdCompanyId)==Guid.Empty;
                ibtnDelete.Visible = stockState == (int)StorageRecordState.WaitAudit;
                if (stockState == (int)StorageRecordState.WaitAudit)
                {
                    ibtnModifyOrder.Visible = normal;
                    btnModifyOrder.Visible = false;
                    if (stockType == (int)StorageRecordType.InnerPurchase)
                    {
                        ibtnApproved.Visible = GetPowerOperationPoint("InnerPurchaseApprove");
                    }
                    else
                    {
                        ibtnApproved.Visible = GetPowerOperationPoint("Auditing");
                    }
                    
                }
                else if (stockState == (int)StorageRecordState.Refuse)
                {
                    ibtnModifyOrder.Visible = false;
                    btnModifyOrder.Visible = normal;
                    ibtnDelete.Visible = string.IsNullOrEmpty(btnModifyOrder.CommandName);//核退没有出货单时，显示“作废”按钮 zal 2016-12-22
                    ibtnApproved.Visible = false;
                }
                else
                {
                    ibtnModifyOrder.Visible = false;
                    btnModifyOrder.Visible = false;
                    ibtnApproved.Visible = false;
                }
            }
        }

        #region 取得用户操作权限

        /// <summary>
        /// 取得用户操作权限
        /// </summary>
        protected bool GetPowerOperationPoint(string powerName)
        {
            string pageName = WebControl.FileName;
            if (powerName == "InnerPurchaseApprove")
            {
                var dd = WebControl.GetPowerOperationPoint(pageName, powerName);
            }
            
            return WebControl.GetPowerOperationPoint(pageName, powerName);
        }

        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 绑定入库仓储
        /// </summary>
        private void BindWarehouse()
        {
            var wList = CurrentSession.Personnel.WarehouseList;
            DDL_Waerhouse.DataSource = wList;
            DDL_Waerhouse.DataTextField = "WarehouseName";
            DDL_Waerhouse.DataValueField = "WarehouseId";
            DDL_Waerhouse.DataBind();
            DDL_Waerhouse.Items.Insert(0, new ListItem("请选择", Guid.Empty.ToString()));
            DDL_Waerhouse.SelectedValue = wList.Count == 1 ? wList.First().WarehouseId.ToString() : Guid.Empty.ToString();
        }

        private void BindThirdCompany()
        {
            RcbThirdCompany.DataSource = CompanyCussentList;
            RcbThirdCompany.DataBind();
        }

        /// <summary>
        /// 绑定年份
        /// </summary>
        private void BindYears()
        {
            var yearlist = new List<int>();
            for (var i = 2007; i <= (DateTime.Now.Year - GlobalConfig.KeepYear); i++)
            {
                yearlist.Add(i);
            }
            DDL_Years.DataSource = yearlist.OrderByDescending(y => y);
            DDL_Years.DataBind();
            DDL_Years.Items.Add(new ListItem(GlobalConfig.KeepYear + "年内数据", "0"));
            DDL_Years.SelectedValue = "0";

            YearsSelectedIndex();
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        private void LoadStorageState()
        {
            var list = EnumAttribute.GetDict<StorageRecordState>();
            DD_StorageState.DataSource = list;
            DD_StorageState.DataTextField = "Value";
            DD_StorageState.DataValueField = "Key";
            DD_StorageState.DataBind();
            DD_StorageState.Items.Insert(0, new ListItem("", ""));
            if (DD_StorageState.Items.Count > 1)
            {
                DD_StorageState.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// 单据类型
        /// </summary>
        private void BindStorageType()
        {
            var dicStorageType = new Dictionary<int, string>
            {
                {(int) StorageRecordType.LendOut, EnumAttribute.GetKeyName((StorageRecordType.LendOut))},
                {(int) StorageRecordType.AfterSaleOut, EnumAttribute.GetKeyName((StorageRecordType.AfterSaleOut))},
                {(int) StorageRecordType.BuyStockOut, EnumAttribute.GetKeyName((StorageRecordType.BuyStockOut))},
                {(int) StorageRecordType.SellStockOut, EnumAttribute.GetKeyName((StorageRecordType.SellStockOut))},
                {(int) StorageRecordType.BorrowOut, EnumAttribute.GetKeyName((StorageRecordType.BorrowOut))},
                {(int) StorageRecordType.InnerPurchase, EnumAttribute.GetKeyName((StorageRecordType.InnerPurchase))}
            };
            DDL_StorageType.DataSource = dicStorageType;
            DDL_StorageType.DataTextField = "Value";
            DDL_StorageType.DataValueField = "Key";
            DDL_StorageType.DataBind();
            DDL_StorageType.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        protected void Lb_Reload_Click(object sender, EventArgs e)
        {
            RG_StorageRecord.Rebind();
        }

        protected void RcbThirdCompany_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var combo = (RadComboBox)sender;
            combo.Items.Clear();
            if (!string.IsNullOrEmpty(e.Text) && e.Text.Length >= 1)
            {
                var wList = CompanyCussentList.Where(ent => ent.Value.Contains(e.Text)).ToDictionary(k=>k.Key,v=>v.Value);
                Int32 totalCount = wList.Count;
                if (e.NumberOfItems >= totalCount)
                    e.EndOfItems = true;
                else
                {
                    combo.Items.Add(new RadComboBoxItem { Text = "", Value = string.Format("{0}", Guid.Empty) });
                    foreach (var companyCussent in wList)
                    {
                        var item = new RadComboBoxItem { Text = companyCussent.Value, Value = string.Format("{0}", companyCussent.Key) };
                        combo.Items.Add(item);
                    }
                }
            }
            else if (string.IsNullOrEmpty(e.Text))
            {
                foreach (var companyCussent in CompanyCussentList)
                {
                    var item = new RadComboBoxItem { Text = companyCussent.Value, Value = string.Format("{0}", companyCussent.Key) };
                    combo.Items.Add(item);
                }
            }
        }
    }
}