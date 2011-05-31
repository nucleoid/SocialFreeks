<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Single.Master" Inherits="System.Web.Mvc.ViewPage<SocialFreeks.Entities.User>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Account
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Account For <%=Model.FirstName%> <%=Model.LastName%></h2>

</asp:Content>
