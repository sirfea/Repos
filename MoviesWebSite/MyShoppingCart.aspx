<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" 
    CodeBehind="MyShoppingCart.aspx.cs" Inherits="MoviesWebSite.MyShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="shoppingCartTitle" runat="server" class="ContentHead">
        Your Shopping Cart</div>
    <asp:GridView ID="grvDetails" DataSourceID="EDSCart" 
    AutoGenerateColumns="False" ShowFooter="True" 
    CellPadding="4"
        DataKeyNames="MovieID,MovieCost,Quantity" 
        CssClass="CartListItem" runat="server">
        <AlternatingRowStyle CssClass="CartListItemAlt" />
        <Columns>
            <asp:BoundField DataField="MovieID" 
            HeaderText="Movie ID" InsertVisible="False"
                ReadOnly="True" SortExpression="MovieID" />
            
            <asp:BoundField DataField="MovieName" 
            HeaderText="Movie Name" ReadOnly="True"
                SortExpression="MovieName" />
            
            <asp:BoundField DataField="MovieCost" 
            HeaderText="Movie Cost" ReadOnly="True"
                SortExpression="MovieCost" />

            <asp:TemplateField>
                <HeaderTemplate>Quantity</HeaderTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity"
                        Width="30"
                        Text='<%# Bind("Quantity") %>'
                        runat="server" />
                    </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>Item&nbsp;Total</HeaderTemplate>
                <ItemTemplate>
                    <%# (Convert.ToDouble(Eval("Quantity")) *
                        Convert.ToDouble(Eval("MovieCost")))%>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>Remove&nbsp;Item</HeaderTemplate>
                <ItemTemplate>
                    <center>
                        <asp:CheckBox ID="chkRemove"
                       runat="server" />
                    </center>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <FooterStyle  CssClass="CartListFooter"/>
        <HeaderStyle CssClass="CartListHead"/>

    </asp:GridView>

    <div style="float:right; width:129px;">
        <strong>
            <asp:Label ID="lblTotalOrder" 
            Text="Payment" runat="server" />
            <br />
            <asp:Label ID="lblTotal" 
            CssClass="NormalBold" 
            Text="Total Order:" 
            EnableViewState="false"
            runat="server" />
        </strong>
    </div>
    <br />
    <br />
    <asp:ImageButton ID="btnUpdate" 
    ImageUrl="~/Styles/Images/update_cart.gif" 
    runat="server" OnClick="btnUpdate_Click"/>

    &nbsp;&nbsp;&nbsp;&nbsp

    <asp:ImageButton ID="btnChcekout" 
    ImageUrl="~/Styles/Images/final_checkout.gif" 
    runat="server"
    PostBackUrl="~/Checkout.aspx" />
    
    
    <asp:EntityDataSource ID="EDSCart" runat="server"
    ConnectionString="name=MoviesSiteDBEntities"
    DefaultContainerName="MoviesSiteDBEntities"
    AutoGenerateWhereClause="true"
    EnableUpdate="true"
    EntitySetName="ViewCarts"
    Select="" EntityTypeFilter="" Where="">

        <WhereParameters>
            <asp:SessionParameter Name="CartID"
            DefaultValue="0" SessionField="MyCartID" />
        </WhereParameters>
    
    </asp:EntityDataSource> 
</asp:Content>
