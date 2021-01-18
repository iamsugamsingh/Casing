<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CasingDrawingPrint.aspx.cs" Inherits="Casing.CasingDrawingPrint" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Height="700px" Width="700px" DocumentMapWidth="0%" Visible="True">
                <LocalReport EnableExternalImages="True">
                </LocalReport>
                
            </rsweb:ReportViewer>
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" BackColor="" 
                ClientIDMode="AutoID" HighlightBackgroundColor="" LinkActiveColor="" 
                LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" 
                PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" 
                PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" 
                SecondaryButtonHoverForegroundColor="" SplitterBackColor="" 
                ToolbarDividerColor="" ToolbarForegroundColor="" 
                ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" 
                ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" 
                ToolBarItemHoverBackColor="" Height="700px" Width="700px" DocumentMapWidth="0%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="CasingDrawingReport.rdlc" enableexternalimages="True">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                TypeName="Casing.CasingDrawingReportDataTableAdapters.">
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
