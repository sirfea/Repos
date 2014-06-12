<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="MoviesList.aspx.cs" Inherits="MoviesWebSite.MoviesList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" 
runat="server">

    <asp:ListView ID="lstMovies" 
    DataKeyNames="MovieID"
    DataSourceID="EDSMoviesByCategory"
    GroupItemCount="2"
    runat="server">
            <EmptyDataTemplate>
                <table runat="server">
                    <tr>
                        <td>Data Not Found</td>
                    </tr>
                </table>
            </EmptyDataTemplate>

            <EmptyItemTemplate>
                <td runat="server" />
            </EmptyItemTemplate>

            <GroupTemplate>
                <tr id="itemPlaceHolderContainer" runat="server">
                    <td id="itemPlaceHolder" runat="server">
                    </td>
                </tr>
            </GroupTemplate>

            <ItemTemplate>
                <td runat="server">
                    <table width="300" border="0">
                        <tr>
                            <td style="width:25px;">
                            &nbsp;
                            </td>
                            <td style="vertical-align:middle; text-align:right">
                            <a href='MovieDetails.aspx?ProductID=
                            <%# Eval("MovieID") %>'>
                                <img src='Catalog/Images/<%# Eval("MovieImage") %>'
                                width="100" height="70" alt="movie image" style="border:1px"/>
                            </a>&nbsp; &nbsp;
                            </td>
                            <td style="width:250px; vertical-align:middle">
                              <a href='MovieDetails.aspx?ProductID=
                                <%# Eval("MovieID") %>'>
                                <span class="MovieListHead">
                                <%# Eval("MovieName") %>
                                </span></a>
                            <br />
                            <span class="MovieListItem"><b>Our Price:</b>
                                <%# Eval("MovieCost", "{0:c}") %>
                             </span>
                             <br />
                             <a href='AddToCart.aspx?MovieID=
                             <%# Eval("MovieID") %>'>
                             <sapn class="MovieListItem"><b>Add To Cart</b></sapn>
                             </a>
                             </td>
                        </tr>
                    </table>
                </td>
            
            </ItemTemplate>
            
            <LayoutTemplate>
                <table runat="server">
                    <tr>
                        <td>
                            <table id="groupPlaceHolderContaniner"
                            runat="server">
                                <tr id="groupPlaceHolder"
                                runat="server">
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server"></td>
                    </tr>
                </table>

            </LayoutTemplate>



    </asp:ListView>


    <asp:EntityDataSource ID="EDSMoviesByCategory" 
    runat="server" 
    AutoGenerateWhereClause="True"
    ConnectionString="name=MoviesSiteDBEntities" 
        DefaultContainerName="MoviesSiteDBEntities" 
        EnableFlattening="False" 
        EntitySetName="Movies">
            
            <WhereParameters>
                <asp:QueryStringParameter 
                Name="CategoryID"
                QueryStringField="CategoryID"
                Type="Int32" />
            </WhereParameters>

    </asp:EntityDataSource>


</asp:Content>
