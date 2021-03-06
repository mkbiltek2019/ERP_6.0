﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDefectiveReturnOutForm.aspx.cs" Inherits="ERP.UI.Web.Windows.EditDefectiveReturnOutForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .warehousecss div {
            float: left;
            padding-right: 5px;
        }

        .lbl_strPanel {
            margin-top: 4px;
        }
    </style>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <rad:RadScriptManager ID="RSM" runat="server">
        </rad:RadScriptManager>
        <rad:RadScriptBlock ID="RSB" runat="server">
            <script src="../JavaScript/jquery.js"></script>
            <script type="text/javascript" src="../JavaScript/telerik.js"></script>
            <script type="text/javascript" src="../JavaScript/common.js"></script>
            <script language="javascript" type="text/javascript">
                function AddHFSonGoods(sonGoods) {
                    if (document.getElementById("<%=HFSonGoods.ClientID %>").value == "")
                        document.getElementById("<%=HFSonGoods.ClientID %>").value += sonGoods;
                    else
                        document.getElementById("<%=HFSonGoods.ClientID %>").value += "@" + sonGoods;

                }
                function DelHFSonGoods(sonGoods) {
                    var str = document.getElementById("<%=HFSonGoods.ClientID %>").value;
                    if (str != "") {
                        if (str.indexOf("@") != -1) {
                            if (str.indexOf(sonGoods + "@") != -1)
                                str = str.replace(sonGoods + "@", "");
                            else
                                str = str.replace("@" + sonGoods, "");
                        } else {
                            str = str.replace(sonGoods, "");
                        }
                    }
                    document.getElementById("<%=HFSonGoods.ClientID %>").value = str;
                }

                function AddFieldToHidden(value, fieldOrderIndex, hiddenClientId) {
                    var hiddenValue = document.getElementById(hiddenClientId).value;
                    var hiddenArray = new Array();
                    hiddenArray = hiddenValue.split(",");
                    hiddenArray[fieldOrderIndex] = value;
                    document.getElementById(hiddenClientId).value = hiddenArray.join(",");
                }
                function SumPrice(quantityId, unitPriceId, sumPriceId) {
                    var quantity = document.getElementById(quantityId).value;
                    var unitPrice = document.getElementById(unitPriceId).value;
                    document.getElementById(sumPriceId).value = Math.round(quantity * 100 * unitPrice * 100) / 10000;
                    Total();
                }
                function onDropDownClosing() {
                    cancelDropDownClosing = false;
                }
                function StopPropagation(e) {
                    //cancel bubbling
                    e.cancelBubble = true;
                    if (e.stopPropagation) {
                        e.stopPropagation();
                    }
                }
                function onCheckBoxClick(chk, cId) {
                    var combo = document.getElementById(cId);
                    //prevent second combo from closing
                    cancelDropDownClosing = true;
                    //holds the text of all checked items
                    var text = "";
                    //holds the values of all checked items
                    var values = "";
                    //enumerate all items
                }

                function OnClientDropDownClosing(CId) {
                    // debugger;
                    var objTable = document.getElementById(CId);
                    var aInput = objTable.getElementsByTagName("input");

                    for (var j = 1; j < aInput.length; j += 1) {
                        //totalNumber += Number(aInput[j].value);

                        alert(aInput[j].value);
                    }

                }
                function onCheckBoxClick(chk, cid) {
                    var objCid = document.getElementById(chk);
                    var objParment = objCid.parentElement.parentElement.parentElement.parentElement.parentElement;
                    // debugger;
                    if (objCid.checked)
                        AddHFSonGoods(chk + "|" + cid);
                    else
                        DelHFSonGoods(chk + "|" + cid);


                }
                //判断数组是否存在 增加元素数组
                function AddElementToArray(element) {
                    if (isUndefined(FieldArray)) {
                        var FieldArray = new Array();
                    }
                    FieldArray.push(element);
                }

                function SelectGoods() {
                    var warehouseId = $("#RCB_Warehouse").val();
                    if (warehouseId.trim() == "") {
                        alert("请选择出库仓库!");
                    } else {
                        ShowObject('GoodsPanel');
                    }
                    return false;
                }

                function isUndefined(variable) {
                    return typeof variable == 'undefined' ? true : false;
                }

                function Total() {
                    var objTable = $find("<%=RGGoods.ClientID%>");
                    var mstview = objTable.get_masterTableView();
                    var aInput = mstview.get_element().getElementsByTagName("input");
                    var totalNumber = 0;
                    for (var j = 0; j < aInput.length; j += 1) {
                        if (aInput[j].name.indexOf("$TB_Quantity") !== -1) {
                            totalNumber += Number(aInput[j].value);
                        }
                    }
                    document.getElementById("<%=Lab_TotalNumber.ClientID %>").innerHTML = totalNumber;
            }
            </script>
        </rad:RadScriptBlock>
        <asp:Panel runat="server">
            <div class="StagePanel">
                <table class="PanelArea">
                    <tr>
                        <td class="AreaRowTitle">申请时间：
                        </td>
                        <td class="AreaRowInfo">
                            <asp:TextBox ID="txt_DateCreated" runat="server" ReadOnly="true" SkinID="LongInput"></asp:TextBox>
                        </td>
                        <td class="AreaRowTitle">操 作 人：
                        </td>
                        <td class="AreaRowInfo">
                            <asp:TextBox ID="txt_Transactor" runat="server" ReadOnly="true" SkinID="LongInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="AreaRowTitle">出库仓储：
                        </td>
                        <td class="AreaRowInfo warehousecss">
                            <rad:RadComboBox ID="RCB_Warehouse" runat="server" MarkFirstMatch="True" ShowToggleImage="True" OnSelectedIndexChanged="RcbInStockOnSelectedIndexChanged" AutoPostBack="true"
                                Width="123px" Enabled="False">
                            </rad:RadComboBox>
                            <rad:RadComboBox ID="RCB_StorageAuth" runat="server" AccessKey="T" ShowToggleImage="True"
                                DataTextField="StorageTypeName" DataValueField="StorageType" Width="121px" Enabled="False" OnSelectedIndexChanged="RcbStorageAuthOnSelectedIndexChanged" AutoPostBack="true">
                            </rad:RadComboBox>
                            <div class="lbl_strPanel">
                                <asp:Label ID="lbl_str" runat="server"></asp:Label>
                            </div>

                        </td>
                        <td class="AreaRowTitle">物流配送公司：
                        </td>
                        <td class="AreaRowInfo">
                            <rad:RadComboBox ID="RCB_HostingFilialeAuth" runat="server" AccessKey="T" ShowToggleImage="True"  AutoPostBack="True"
                                DataTextField="HostingFilialeName" DataValueField="HostingFilialeId" Enabled="False" OnSelectedIndexChanged="RcbFilialeAuthOnSelectedIndexChanged"
                                Width="250px">
                            </rad:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="AreaRowTitle">供 应 商：
                        </td>
                        <td class="AreaRowInfo" colspan="3">
                            <rad:RadComboBox ID="RCB_CompanyId" runat="server" AccessKey="T" MarkFirstMatch="True"
                                Filter="Contains" DataTextField="CompanyName" DataValueField="CompanyId" Width="250px"
                                Height="200px">
                            </rad:RadComboBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="AreaRowTitle">备注说明：
                        </td>
                        <td class="AreaRowInfo" colspan="3">
                            <asp:TextBox ID="txt_Description" runat="server" Width="74%" MaxLength="100" Height="30px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="AreaRowInfo" colspan="4">
                            <hr />
                        </td>
                    </tr>
                </table>

                <rad:RadGrid ID="RGGoods" AllowPaging="false" runat="server" SkinID="Common" Height="266px"
                    OnNeedDataSource="RgGoods_NeedDataSource" OnDeleteCommand="RgGoods_DeleteCommand">
                    <ClientSettings>
                        <Resizing AllowColumnResize="True"></Resizing>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <ClientMessages DragToResize="调整大小" />
                    </ClientSettings>
                    <MasterTableView ClientDataKeyNames="RealGoodsId" DataKeyNames="RealGoodsId,UnitPrice,GoodsId">
                        <CommandItemTemplate>
                        <asp:LinkButton ID="lbtnSelectGoods" OnClientClick="return SelectGoods();" runat="server">
                            <asp:Image ID="AddRecord" runat="server" ImageAlign="AbsMiddle" SkinID="AddImageButton" />
                            选择商品
                        </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButtonRefresh" runat="server" CommandName="RebindGrid">
                        <asp:Image ID="Refresh" runat="server" ImageAlign="AbsMiddle" SkinID="RefreshImageButton" />刷新
                    </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle HorizontalAlign="Right" Height="24px" />
                        <Columns>
                            <rad:GridBoundColumn DataField="GoodsCode" HeaderText="编号" UniqueName="GoodsCode">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </rad:GridBoundColumn>
                            <rad:GridBoundColumn DataField="GoodsName" HeaderText="商品名称" UniqueName="GoodsName">
                                <HeaderStyle Width="205px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridBoundColumn>

                            <rad:GridBoundColumn DataField="Specification" HeaderText="SKU" UniqueName="Specification">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridBoundColumn>
                            <rad:GridBoundColumn DataField="Units" HeaderText="计量单位" UniqueName="Units">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridBoundColumn>
                            <rad:GridTemplateColumn DataField="UnitPrice" HeaderText="单价" UniqueName="UnitPrice">
                                <ItemTemplate>
                                    <asp:TextBox ID="lbl_UnitPrice" runat="server" Font-Bold="true" Text='<%# ERP.UI.Web.Common.WebControl.RemoveDecimalEndZero(Convert.ToDecimal(Eval("UnitPrice").ToString())) %>'
                                        SkinID="ShortInput"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridTemplateColumn>

                            <rad:GridTemplateColumn DataField="Quantity" HeaderText="退货数" UniqueName="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="TB_Quantity" runat="server" Font-Bold="true" Text='<%# Math.Abs(Convert.ToDouble(Eval("Quantity"))) %>'
                                        SkinID="ShortInput" onblur="Total();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVQuantity" runat="server" ControlToValidate="TB_Quantity"
                                        ErrorMessage="数量必须填写" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REVnums" runat="server" ControlToValidate="TB_Quantity"
                                        ValidationExpression="^\d+$" ErrorMessage="*"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridTemplateColumn>


                            <rad:GridButtonColumn HeaderText="操作" CommandName="Delete" Text="删除" ConfirmText="确实要删除吗？"
                                UniqueName="Delete" ButtonType="ImageButton">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridButtonColumn>
                            <rad:GridBoundColumn DataField="GoodsId" HeaderText="GoodsId" UniqueName="GoodsId"
                                Visible="False">
                            </rad:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
                <table class="PanelArea">
                    <tr>
                        <td class="AreaRowTitle">合计数量：
                        </td>
                        <td class="AreaRowInfo">
                            <asp:Label ID="Lab_TotalNumber" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="GoodsPanel" style="background-color: #FFFFFF; width: 100%; height: 200px; left: 0px; position: absolute; top: 0px; z-index: -1; visibility: hidden">
            <table width="100%">
                <tr>
                    <td style="width: 80px; height: 20px; text-align: right;">选择分类：
                    </td>
                    <td style="width: 230px;">
                        <rad:RadComboBox ID="RCB_GoodsClass" runat="server" CausesValidation="false" AutoPostBack="true"
                            DataValueField="ClassId" DataTextField="ClassName" OnSelectedIndexChanged="GoodsClass_SelectedIndexChanged"
                            Width="220px" Height="200px">
                        </rad:RadComboBox>
                    </td>
                    <td style="width: 80px; text-align: right;">编号搜索：
                    </td>
                    <td style="width: 230px;">
                        <rad:RadComboBox ID="RCB_Goods" runat="server" CausesValidation="false" AutoPostBack="true"
                            AllowCustomText="True" EnableLoadOnDemand="True" DataTextField="GoodsName" DataValueField="GoodsId"
                            OnSelectedIndexChanged="Goods_SelectedIndexChanged" OnItemsRequested="RcbGoodsItemsRequested" Width="220px" Height="200px">
                        </rad:RadComboBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbIncludeUnsale" runat="server" Text="含下架" />
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="Button_SelectGoods" runat="server" Text="添加商品" CssClass="Button"
                            OnClick="SelectGoods_Click" CausesValidation="false" />&nbsp;
                    <input id="CloseGoodsPanel" type="button" value="关闭添加" class="Button" onclick="return HiddenObject('GoodsPanel');" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HFSonGoods" runat="server" />
            <rad:RadGrid runat="server" ID="RGSelectGoods" AutoGenerateColumns="False" MasterTableView-CommandItemDisplay="None"
                Height="175px" SkinID="Common" OnNeedDataSource="RGSelectGoods_NeedDataSource" AllowMultiRowSelection="true"
                AllowPaging="false" OnItemDataBound="RGSelectGoods_ItemDataBound">
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView ClientDataKeyNames="GoodsId" DataKeyNames="GoodsId">
                    <CommandItemStyle HorizontalAlign="Right" Height="0px" />
                    <Columns>
                        <rad:GridClientSelectColumn UniqueName="column">
                            <HeaderStyle Width="50" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </rad:GridClientSelectColumn>
                        <rad:GridBoundColumn DataField="GoodsCode" HeaderText="商品编号" UniqueName="GoodsCode">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </rad:GridBoundColumn>
                        <rad:GridBoundColumn DataField="GoodsName" HeaderText="商品名" UniqueName="GoodsName">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </rad:GridBoundColumn>
                        <rad:GridTemplateColumn DataField="Luminosity" HeaderText="光度" UniqueName="Luminosity">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="Astigmia" HeaderText="散光" UniqueName="Astigmia">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="Axial" HeaderText="轴位" UniqueName="Axial">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </div>
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btnSave" runat="server" Text="  保存  " OnClick="btn_InsterStock" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="  取消  " OnClientClick="return CancelWindow()" />
        </div>
        <rad:RadAjaxManager ID="RAM" runat="server" useembeddedscripts="false">
            <AjaxSettings>
                <rad:AjaxSetting AjaxControlID="RCB_Warehouse">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="RCB_StorageAuth" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                        <rad:AjaxUpdatedControl ControlID="RCB_HostingFilialeAuth" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                        <rad:AjaxUpdatedControl ControlID="RGGoods" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
                <rad:AjaxSetting AjaxControlID="RCB_StorageAuth">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="RCB_HostingFilialeAuth" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                        <rad:AjaxUpdatedControl ControlID="lbl_str"></rad:AjaxUpdatedControl>
                        <rad:AjaxUpdatedControl ControlID="RGGoods" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
                <rad:AjaxSetting AjaxControlID="RCB_HostingFilialeAuth">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="Lab_TotalNumber"></rad:AjaxUpdatedControl>
                        <rad:AjaxUpdatedControl ControlID="RGGoods" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
                <rad:AjaxSetting AjaxControlID="RGGoods">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="Lab_TotalNumber"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
                <rad:AjaxSetting AjaxControlID="RCB_GoodsClass">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="RGSelectGoods" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
                <rad:AjaxSetting AjaxControlID="RCB_Goods">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="RGSelectGoods" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
                <rad:AjaxSetting AjaxControlID="Button_SelectGoods">
                    <UpdatedControls>
                        <rad:AjaxUpdatedControl ControlID="RGGoods" LoadingPanelID="Loading"></rad:AjaxUpdatedControl>
                        <rad:AjaxUpdatedControl ControlID="Lab_TotalNumber"></rad:AjaxUpdatedControl>
                    </UpdatedControls>
                </rad:AjaxSetting>
            </AjaxSettings>
        </rad:RadAjaxManager>
        <rad:RadAjaxLoadingPanel ID="Loading" runat="server" Skin="WebBlue">
        </rad:RadAjaxLoadingPanel>
    </form>
</body>
</html>
