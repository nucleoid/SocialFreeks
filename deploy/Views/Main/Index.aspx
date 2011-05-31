<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Social Freeks
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StyleContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Content/css/imagemap/tooltip.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" src="/Content/js/imagemap/cvi_tip_lib.js"></script>
	<script type="text/javascript" src="/Content/js/imagemap/wz_jsgraphics.js"></script>
	<script type="text/javascript" src="/Content/js/imagemap/maputil.js"></script>
	<script type="text/javascript" src="/Content/js/imagemap/mapper.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="body-box">
        <div align="top" class="centered"><img class="mapper noborder iopacity50 icolorff0000" src="/Content/img/getting_ready.gif" usemap="#getting_ready" width="650" height="366" border="0" alt="" /></div>
		<map id="getting_ready" name="getting_ready">
                <area shape="poly" tooltip="Food Court" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" onmousemove="cvi_tip._move(event);" coords="37,131,34,278,127,280,128,227,164,225,165,132" href="#" />
                <area shape="poly" tooltip="Games" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" onmousemove="cvi_tip._move(event);" coords="183,70,236,125,281,79,226,26" href="#" target="" />
                <area shape="poly" tooltip="Tech" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" onmousemove="cvi_tip._move(event);" coords="371,79,412,123,469,70,425,26" href="#" target="" />
                <area shape="poly" tooltip="Music" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" onmousemove="cvi_tip._move(event);" coords="284,282,226,337,178,291,239,236" href="#" target="" />
                <area shape="poly" tooltip="Television" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" onmousemove="cvi_tip._move(event);" coords="486,134,488,229,615,228,614,76,522,79,524,135" href="#" target="" />
                <area shape="poly" tooltip="News" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" onmousemove="cvi_tip._move(event);" coords="371,282,415,237,473,295,424,341" href="#" target="" />
        </map>
    </div>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterContent" runat="server">
<div id="footer-box" class="clearfix">
	<div class="footer-container">
	    <div class="clearfix bottom-copy">
		    <div class="left"><a href="http://www.socialfreeks.com/" target="_blank">Social Freeks &copy; 2011</a></div>
		    <div class="right"><a href="http://www.socialfreeks.com/" target="_blank">Social Freeks &copy; 2011</a></div>
	    </div>
	</div>
</div>
</asp:Content>