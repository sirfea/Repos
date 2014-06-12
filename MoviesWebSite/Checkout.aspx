<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master"
     AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="MoviesWebSite.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CheckoutHeader" runat="server" class="ContentHead"> </div>
        <span id="Message" runat="server">
            <asp:Label ID="lblCartHeader" Text="Please check all items" runat="server" />
        </span>
        <asp:GridView ID="grvChckout" AutoGenerateColumns="false" DataKeyNames="MovieID,MovieCost,Quantity"
            DataSourceID="EDSCheckout" CellPadding="4" CssClass="CartListItem"
            ShowFooter="true" OnRowDataBound="grvChckout_RowDataBound" GridLines="Vertical" runat="server" >
            <AlternatingRowStyle CssClass="CartListItemAlt" />
            <Columns>
                <asp:BoundField DataField="MovieID" HeaderText="Movie ID" InsertVisible="False"
                    ReadOnly="True" SortExpression="MovieID" />                
                <asp:BoundField DataField="MovieName" HeaderText="Movie Name" ReadOnly="True" SortExpression="MovieName" />
                <asp:BoundField DataField="MovieCost" HeaderText="Movie Cost" ReadOnly="True" SortExpression="MovieCost" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" SortExpression="Quantity" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Item&nbsp;Total</HeaderTemplate>
                    <ItemTemplate>
                        <%# (Convert.ToDouble(Eval("Quantity")) *
                        Convert.ToDouble(Eval("MovieCost")))%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="CartListFooter" />
            <HeaderStyle CssClass="CartListHead" />
        </asp:GridView>
        <br />
        <asp:ImageButton ID="btnChceckout" 
             ImageUrl="~/Styles/Images/submit.gif" OnClick="btnChceckout_Click"
            runat="server" />
        <asp:EntityDataSource ID="EDSCheckout" 
        runat="server" 
        ConnectionString="name=MoviesSiteDBEntities"
        DefaultContainerName="MoviesSiteDBEntities" 
        AutoGenerateWhereClause="true" 
        EntitySetName="ViewCarts" EnableFlattening="false"
        EntityTypeFilter="" Select="" Where="">
            <WhereParameters>
                <asp:SessionParameter Name="CartID"
                DefaultValue="0" SessionField="MyCartID" />       
            </WhereParameters>

    </asp:EntityDataSource>
    
</asp:Content>

