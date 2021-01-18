<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HeatNoPage.aspx.cs" Inherits="Casing.HeatNoPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="Scripts/jquery-1.4.2.min.js"></script>
    <script>
        function jScript() {
            $('#TextBox1').focus();
            var $inp = $('.cls');
            $inp.bind('keydown', function (e) {
                //var key = (e.keyCode ? e.keyCode : e.charCode);
                var key = e.which;
                if (key == 13) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    if (nxtIdx % 5 == 0) {
                        $(".cls:eq(" + nxtIdx + ")").focus();
                        $(".cls:eq(" + nxtIdx + ")").click();
                        $(".cls:eq(" + nxtIdx + ")").select();
                    }
                }
                if (key == 38) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) - 5;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 40) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 5;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 37) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) - 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 39) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
            });
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">                Sys.Application.add_load(jScript);</script>
            
            <div style="margin-top:50px;">
                <center>
                    <h2>
                        Heat Number Entry
                    </h2>
                </center>
            </div>

    <div style="margin-top:25px;" class="container">
        <asp:GridView ID="GridView1" runat="server" ShowFooter="true" 
                AutoGenerateColumns="false" Width="100%" Font-Size="Small" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-Height="30px">

                <Columns>
                    <asp:TemplateField HeaderText = "Remove Rows" ItemStyle-Width="100" >  
                        <ItemTemplate>  
                            <asp:Button ID="removeBtn" class="btn btn-xs" runat="server" style="background:red;color:White; width:100%;" Text="Remove" onclick="removeBtn_Click" AccessKey="r" />
                        </ItemTemplate>  
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText = "Row No." ItemStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UID">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="uidTxtBox"  runat="server"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Heat Number">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="heatNoTxtBox" runat="server"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="dateTxtBox" runat="server"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Length">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="lengthTxtBox" runat="server"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Diameter">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="diameterTxtBox" runat="server"></asp:TextBox>
                            <%--<asp:LinkButton class="cls" ID="modelPreviewLink" runat="server" OnClick="modelPreviewLink_Click">Preview</asp:LinkButton>--%>

                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Right" />

                        <FooterTemplate>

                         <asp:Button ID="ButtonAdd" class="cls btn btn-xs" runat="server" style="background:green;color:White; width:100%; border:none;" Text="Add new Row" onClick="ButtonAdd_Click" BorderStyle="Outset" AccessKey="n" />

                        </FooterTemplate>

                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="Black" ForeColor="White" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
            </asp:GridView>
        
            <center>
                 <asp:Button ID="saveBtn" runat="server" Text="Save" onclick="saveBtn_Click" class="btn btn-success" Style="width:200px; margin-top:25px; font-size:larger"/>
            </center>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
