<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="loginPage.aspx.cs" Inherits="Casing.loginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
        <div class="row">
            <div class="col-lg-6">
                <a href="HeatNoPage.aspx" style="text-decoration:none; color:black;">
                    <div style="width:200px; border:2px solid #999; float:right; width:296px; height:148px; background:#999999; text-align:center;margin-top:150px; border-radius:5px; box-shadow:5px 5px 5px gray;">
                        <h3 style="margin-top:55px;">
                            Heat Number
                        </h3>
                    </div>
                </a>
            </div>

            <div class="col-lg-3">
                <a href="CasingDrawing.aspx" style="text-decoration:none; color:black;">
                    <div style="width:200px; border:2px solid #999;  width:296px; height:148px; background:#999999; text-align:center;margin-top:150px; border-radius:5px; box-shadow:5px 5px 5px gray;">
                        <h3 style="margin-top:55px;">
                            Casing Drawing
                        </h3>
                    </div>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
