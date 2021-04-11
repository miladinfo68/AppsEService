<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationSeenChat.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationSeenChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="/Contact/Theme/css/bootstrap.css" rel="stylesheet" />
    <link href="/Contact/Theme/fonts/font-awesome.min.css" rel="stylesheet" />
    <link href="/Contact/css/custom.css" rel="stylesheet" />
    <link href="/Contact/css/messsages.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Label ID="LblSearchCount" runat="server" Text="0"></asp:Label>
    <asp:Label ID="LblSearchText" runat="server" Text=""></asp:Label>
    <asp:Label ID="LblCurCount" runat="server" Text="0"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
               <asp:UpdatePanel ID="ButtonClick" runat="server">
                            <ContentTemplate>
<div class="row" dir="rtl">

        <asp:UpdateProgress ID="updateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress123" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
        <div class="panel panel-primary" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-heading">
                <h3 class="panel-title">جستجو دانشجو برای نمایش  اتاق گفتگو</h3>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-10">
                        <span>
                        <label style="width: 92px; text-align: left">شماره دانشجویی:</label>
                        <asp:TextBox ID="txtStCode" CssClass="textBoxInput pdate disabled" OnTextChanged="txtStCode_TextChanged" runat="server">
  
                        </asp:TextBox>
                              </span>
                        <span>
                                <asp:Label ID="lblStudentName" runat="server" Font-Size="Medium" Text="" Visible="false" CssClass="label label-info"></asp:Label>
                        </span>

                    </div>
                </div>
            </div>
            <div class="panel-footer">
                <div class="row" dir="ltr">
                    <div class="col-md-7"></div>
                    <div class="col-md-5">
                 

                                <asp:Button ID="ClearBtn" runat="server" Text="پاک کردن" CssClass="btn btn-danger btnWith" OnClick="ClearBtn_Click" />
                                <asp:Button ID="SearchBtn" runat="server" Text="جستجو" CssClass="btn btn-success btnWith" OnClick="SearchBtn_Click" ValidationGroup="groupValidate" OnClientClick="return scroll()"></asp:Button>


                    </div>

                </div>
            </div>
        </div>
    </div>
                                <asp:Panel ID="PnlChat" runat="server" Visible="false">
    <!-- page content -->
    <div class="right_col" role="main" style="width:100%">

        <!-- top tiles -->

        <!-- /top tiles -->

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="dashboard_graph" dir="rtl">
                    <div class="container-fluid">
                        <div class="container ng-scope">
                            <div class="block-header">
                                <h2></h2>
                                <asp:Label ID="LblIdUser" runat="server" CssClass="hide" Text="Label"></asp:Label>
                                <asp:Label ID="LblIdGrp" runat="server" CssClass="hide" Text=""></asp:Label>
                            </div>
                            <div class="card m-b-0" id="messages-main" style="box-shadow: 0 0 40px 1px #c9cccd;height:500px">
                                <div class="ms-menu menuContact" style="overflow: scroll; overflow-x: hidden;" id="ms-scrollbar">
                                    <div class="ms-block">
                                        <div class="ms-user">
                                        </div>
                                    </div>

                                    <div class="ms-block">
                                        <div class="text-center" style="margin-bottom: 8px">
                                            <input type="text" id="findField" size="10" />
                                            <input type="button" class="FindNext" value="جستجو" />


                                        </div>
                                        <asp:LinkButton ID="BtnGrp" runat="server" OnClick="BtnGrp_Click"
                                            CssClass="btn btn-primary btn-block ms-new btnGrp text-center " Font-Size="Large">
                                     <i class="fa fa-users" aria-hidden="true"></i><span class="mr-1" style="margin-right:10px">گفتگو دفاع</span>
                                        </asp:LinkButton>
                                    </div>
                                    <hr />
                                    <div class="listview lv-user">
                                        <asp:DataList ID="DtlstContact" runat="server" OnItemCommand="DtlstContact_ItemCommand" CssClass="disabled" >
                                            <ItemTemplate>
                                                <div class="media" style="padding-top: 5px">
                                                    <div class="lv-avatar pull-right">

                                                        <telerik:RadBinaryImage ID="Image" runat="server" VisibleWithoutSource="False" DataValue='<%# Bind("Images") %>'
                                                            Style="width: 30px; height: 30px" />

                                                    </div>
                                                    <div class="media-body ">
                                                        <div class="lv-title">
                                                            <%--<asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>--%>
                                                            <asp:LinkButton ID="LnkNameConatct" ForeColor="Black" runat="server" Text='<%# Bind("FullName") %>' OnClick="LnkNameConatct_Click"
                                                                CommandName="IdName" CommandArgument='<%#Eval("ID")%>'>
   
                                                            </asp:LinkButton>
                                                        </div>
                                                        <%--<div class="lv-small"> Acadnote a world class website is processing surveys for </div>--%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>


                                    </div>
                                </div>
                                <div class="ms-body">
                                    <div class="listview lv-message">
                                        <div class="lv-header-alt clearfix">
                                            <div id="ms-menu-trigger">
                                                <div class="line-wrap">
                                                    <div class="line top">
                                                    </div>
                                                    <div class="line center"></div>
                                                    <div class="line bottom"></div>
                                                </div>
                                            </div>

                                            <div class="lvh-label  ">
                                                <div class=" pull-left" style="margin-right: 5px">
                                                    <asp:TextBox ID="TxtIdOnChat" runat="server" CssClass="hide"></asp:TextBox>
                                                    <asp:Label ID="LblNameOnChat" runat="server" Width="100%" ForeColor="Black"></asp:Label>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="lv-body dtTable" id="ms-scrollbar" style="overflow: scroll; overflow-x: hidden; height: 400px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" width="100%" Height="600px">
                                                <ContentTemplate>

                                                    <asp:DataList ID="DtLstMesages" runat="server" Width="100%" Height="600px">
                                                        <ItemTemplate>
                                                            <div class="lv-item media">
                                                                <asp:Panel ID="PanelReplayed" runat="server" CssClass='<%# Bind("FlagReplayed") %>'>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-sm-5">


                                                                                    <div class="containerReplay msg_ReplayChat">
                                                                                        <h5>پاسخ</h5>

                                                                                        <div class="msg_Replay">
                                                                                            <div class="hidden">
                                                                                                <asp:Label ID="LblReplayId" runat="server" Text='<%# Bind("RplyId") %>'></asp:Label>
                                                                                            </div>
                                                                                            <asp:Label ID="LblReplayMsg" Font-Size="Smaller" runat="server" Text='<%# Eval("RplyMsg").ToString().Replace("@$file send@$"," فایل ").Replace("@$sound voice@$blob"," فایل صوتی ") %>'></asp:Label>
                                                                                        </div>
                                                                                        <div class="left">
                                                                                            <asp:Label ID="Label1" runat="server" Font-Size="X-Small" Text='<%# Bind("FullNameRp") %>'></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <div class="lv-avatar pull-right">



                                                                    <telerik:RadBinaryImage ID="Image4" runat="server" VisibleWithoutSource="False" DataValue='<%# Bind("Images") %>'
                                                                        Style="width: 30px; height: 30px" />

                                                                </div>
                                                                <div class="media-body">
                                                                    <div class="ms-item">
                                   
                                                                        <asp:Label ID="LblChatID" runat="server" CssClass="hide" Text='<%# Bind("ChatID") %>'></asp:Label>

                                                                        <asp:Label ID="Message" runat="server" Font-Size="Smaller" Text='<%#(string) Eval("Message").ToString().Replace("@$file send@$"," فایل ").Replace("@$sound voice@$blob"," فایل صوتی ") %>' CssClass='<%#( ((int)Eval("IDTypeFile") ==1)||((Eval("IsDeleted")!=null)&&((bool)Eval("IsDeleted") ==true))? string.Empty :"hidden")%>'></asp:Label>
                                                                        <asp:Panel ID="Panelaudio" runat="server" CssClass='<%#  ((int)Eval("IDTypeFile") ==2)&&((Eval("IsDeleted")==null)||(((bool)Eval("IsDeleted")) ==false)) ? "" :"hidden"%> '>
                                                                            <audio style="max-width: 200px" controls controlslist="nodownload">
                                                                                <source src='<%# Eval("FormatFile").ToString().Trim()==".wav"?"/Contact/SoundRecorder/"+Eval("ChatID")+".wav":""%>' <%# Eval("FormatFile").ToString().Trim()==".wav"?"type=audio/mpeg":""%>>>
                                                                            </audio>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="PanelFile" runat="server" CssClass='<%#((int)Eval   ("IDTypeFile") ==3)&&((Eval("IsDeleted")==null)||(((bool)Eval("IsDeleted")) ==false)) ? string.Empty :"hidden"%>'>

                                                                            <span>
                                                                                <a class="btn btn-link linkFile" style="width: 100%" href="/Contact/FileSaver/<%# Eval("ChatID")%><%# Eval("FormatFile")%>" download><%# Eval("Message").ToString().Replace("@$file send@$","")%></a>
                                                                            </span>

                                                                        </asp:Panel>

                                                                    </div>

                                                                    <small class="ms-date"><small class="ms-date"><i class="fa fa-clock-o"></i>



                                                                        <span>
                                                                            <asp:Label ID="Date" runat="server" Font-Size="XX-Small" Text='<%# Bind("DatePersianChat") %>'></asp:Label></span>
                                                                        <span>
                                                                            <asp:Label ID="Time" runat="server" Font-Size="XX-Small" Text='<%# Bind("TimeChat") %>'></asp:Label>-</span>
                                                                        <span><i class="fa fa-user"></i></span>
                                                                        <span>
                                                                            <asp:Label ID="LblNameSender" runat="server" Font-Size="XX-Small" Text='<%# Bind("FullNameS") %>'></asp:Label></span>
                                                                        <span class="hidden">
                                                                            <asp:Label ID="LblSenderId" runat="server" Font-Size="XX-Small" Text='<%# Bind("IdNameS") %>'></asp:Label></span>
                                                                    </small>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
                                    </asp:Panel>
                                             </ContentTemplate>
                        </asp:UpdatePanel>
    <script src="/Contact/Script/ContactEdu.js"></script>




</asp:Content>
