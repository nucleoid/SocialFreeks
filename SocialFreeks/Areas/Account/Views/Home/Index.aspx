<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Single.Master" Inherits="System.Web.Mvc.ViewPage<SocialFreeks.Entities.User>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Profile
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Profile For <%=Model.FirstName%> <%=Model.LastName%></h2>

</asp:Content>
