<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="MoviesWebSite.MovieDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:FormView ID="frmMovieView" runat="server"
    DataSourceID="EDSMovies" DataKeyNames="MovieID">
        <ItemTemplate>
            <div class="ContentHead">
            <%# Eval("MovieName") %>
            </div>
            <br />
            <table>
                <tr runat="server">
                    <td runat="server" 
                    style="vertical-align:top;">
                        <img src='Catalog/<%# Eval("MovieImage") %>' 
                        alt='<%# Eval("MovieName") %>' />
                    </td>

                    <td runat="server" 
                    style="vertical-align:top;">
                        <%# Eval("Description") %> 
                    </td>
                 </tr>
           </table>
           
           <span class="UnitCost"><b>Your price is:</b>
           <%# Eval("MovieCost", "{0:c}") %></span>
           <br />
           <a href='AddToCart.aspx?MovieID=<%# 
           Eval("MovieID") %>' >
           <br />
           <img src="Styles/Images/add_to_cart.gif" runat="server"
           alt="Pic" />
           </a>
           
                  
        </ItemTemplate>
    </asp:FormView>


<asp:EntityDataSource ID="EDSMovies" runat="server"
AutoGenerateWhereClause="true"
ConnectionString="name=MoviesSiteDBEntities"
DefaultContainerName="MoviesSiteDBEntities"
EntitySetName="Movies">
    <WhereParameters>
        <asp:QueryStringParameter Name="MovieID"
        QueryStringField="movieId"
        Type="Int32" />
    </WhereParameters>
</asp:EntityDataSource>

</asp:Content>
