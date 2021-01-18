<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CasingDrawing.aspx.cs" Inherits="Casing.CasingDrawing" %>
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
                    if (nxtIdx % 15 == 0) {
                        $(".cls:eq(" + nxtIdx + ")").focus();
                        $(".cls:eq(" + nxtIdx + ")").click();
                        $(".cls:eq(" + nxtIdx + ")").select();
                    }
                }
                if (key == 38) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) - 15;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 40) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 15;
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="true">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">                Sys.Application.add_load(jScript);</script>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" EnableViewState="true">
        <ContentTemplate>
    

    <div class="topArea">
    <center>
            <div class="form-group">
                <label for="exampleInputName2">Ref. Steel Cut</label>
                <asp:TextBox ID="refTxtBox" CssClass="form-control" runat="server" style="width:80px;" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="exampleInputEmail2">Date</label>
                <asp:TextBox ID="currentDateTxtBox" CssClass="form-control" runat="server" style="width:120px;" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="exampleInputName2">Article No</label>
                <asp:TextBox ID="articleNumTxtBox" CssClass="form-control" runat="server" style="width:150px;" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="exampleInputEmail2">Description</label>
                    <asp:TextBox ID="descriptionTxtBox" CssClass="form-control" runat="server" style="width:200px;" required></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="printBtn" CssClass="btn" style="background:#D3D3D3; color:black;" runat="server" Text="Print Drawing" OnClick="printBtn_Click"></asp:Button>
            </div>
            <div class="form-group">
                <asp:Button ID="printSteelIndentBtn" CssClass="btn" style="background:#D3D3D3; color:black;" runat="server" Text="Print Steel Indent" OnClick="printSteelIndentBtn_Click"></asp:Button>
            </div>
            <div class="form-group">
                <asp:Button ID="prinBandsaJobCardBtn" CssClass="btn" style="background:#D3D3D3; color:black;" runat="server" Text="Print Bandsaw Job Card" OnClick="prinBandsaJobCardBtn_Click"></asp:Button>
            </div>
        </center>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
           
    <div class="col-lg-8">

        
            <script type="text/javascript" language="javascript">Sys.Application.add_load(jScript);</script>

        <asp:GridView ID="GridView1" runat="server" ShowFooter="true" 
                AutoGenerateColumns="false" Width="100%" Font-Size="Small" OnRowCommand="GridView1_RowCommand" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">

                <Columns>
                    <asp:TemplateField HeaderText = "Remove Rows" ItemStyle-Width="70" >  
                        <ItemTemplate>  
                            <asp:Button ID="removeBtn" class="btn btn-xs" UseSubmitBehavior="false" runat="server" style="background:red;color:White; width:100%;" Text="Remove" onclick="removeBtn_Click" AccessKey="r" />
                        </ItemTemplate>  
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText = "Row No." ItemStyle-Width="20">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UID">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox1"  runat="server" Width="70px" OnTextChanged="OnUidChanged" required AutoPostBack="true" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" AccessKey="u"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Element">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox2"  OnTextChanged="OnElementChanged" AutoPostBack="true" runat="server" Width="100%" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Multiplier">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox3" runat="server" Width="100%" OnTextChanged="MultiPlier_change" AutoPostBack="true" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Qty">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox4" runat="server" Width="30px" CommandName="Select" required CommandArgument="<%# Container.DataItemIndex %>" AutoPostBack="true" OnTextChanged="Qty_change"  AccessKey="b"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox5" runat="server" Width="35px" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Make">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox6" runat="server" Width="40px" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Grade">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox7" runat="server" Width="40px" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hardness">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox8" runat="server" Width="60px" AutoPostBack="true" OnTextChanged="onHardnessText_Changed" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="Marking">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox9" runat="server" Width="100px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Model">

                        <ItemTemplate>

                            <%--<asp:TextBox class="cls" ID="TextBox10" runat="server" Width="35px"></asp:TextBox>--%>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="50px" class="cls" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">

                            </asp:DropDownList>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="O.D.">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox11" runat="server" Width="40px" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Length">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="TextBox12" runat="server" Width="50px" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="I.D.">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox13" runat="server" Width="40px" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dimen 4">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox14" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                   <%-- <asp:TemplateField HeaderText="Dimen 5">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox15" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>--%>

                    <%--<asp:TemplateField HeaderText="Dime 6">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox16" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dimen 7">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox17" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dimen 8">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox18" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dimen 9">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox19" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dimen 10">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox20" runat="server" Width="60px"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Dimen 5">

                        <ItemTemplate>

                             <asp:TextBox class="cls" ID="TextBox15" runat="server" Width="60px" ></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Preview Models">

                        <ItemTemplate>

                        <%--<asp:TextBox class="cls" ID="TextBox16" runat="server" Width="60px" AccessKey="a"></asp:TextBox>--%>
                            <asp:LinkButton class="cls" ID="modelPreviewLink" runat="server" OnClick="modelPreviewLink_Click">Preview</asp:LinkButton>

                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Right" />

                        <FooterTemplate>

                         <asp:Button ID="ButtonAdd" class="cls btn btn-xs" runat="server" style="background:green;color:White; width:100%; border:none;" Text="Add Row" onClick="ButtonAdd_Click" BorderStyle="Outset" AccessKey="n" />

                        </FooterTemplate>

                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="Black" ForeColor="White" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
            </asp:GridView> 
            
        <center>
        <asp:Button ID="submitButton" runat="server" Text="Submit" class="btn btn-success" Style="width:200px; margin-top:25px; font-size:larger" onclick="submitButton_Click" />
    </center>
    </div>

    <div class="col-lg-4" >
        <%--<img src="images/ANU LOGO.jpg" width="100%">--%>
        <center><asp:Label ID="modelName" runat="server" Font-Bold="True"></asp:Label></center>
        <asp:Image ID="Image1" runat="server" width="100%"/>
    </div></ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>