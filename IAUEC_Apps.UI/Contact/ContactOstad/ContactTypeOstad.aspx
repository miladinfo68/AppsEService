<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageContactOS.Master" AutoEventWireup="true" CodeBehind="ContactTypeOstad.aspx.cs" Inherits="IAUEC_Apps.UI.Contact.ContactOstad.ConatctTypeOstad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container ng-scope">
            <div class="block-header">
                <h2></h2>

            </div>
            <div class="card m-b-0" id="messages-main" style="box-shadow: 0 0 40px 1px #c9cccd;">
                <div class="ms-menu " style="overflow: scroll; overflow-x: hidden;" id="ms-scrollbar">
                    <div class="ms-block">
                        <div class="ms-user">
                        </div>
                    </div>

                    <div class="ms-block">
                        <a class="btn btn-primary btn-block ms-new btnGrp" href="#"><span class="glyphicon glyphicon-envelope">
                            <asp:Button ID="BtnGrp" runat="server" OnClick="BtnGrp_Click" Text="گروه" /></span></a>
                        <asp:Label ID="LblGrp" runat="server" Text="1"></asp:Label>
                    </div>
                    <hr />
                    <div class="listview lv-user">
                        <asp:DataList ID="DtlstContact" runat="server" OnItemCommand="DtlstContact_ItemCommand">
                            <ItemTemplate>
                                <div class="media">
                                    <div class="lv-avatar pull-right">

                                        <img src="../Img/avatar5.png" />
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
                                    <asp:TextBox ID="TxtIdOnChat" runat="server"></asp:TextBox>
                                    <asp:Label ID="LblNameOnChat" runat="server" Width="100%" ForeColor="Black"></asp:Label>

                                    <%--<img src="./images/bhai.jpg" alt="">--%>
                                    <%--   <asp:Image ID="Image3" runat="server" />--%>
                                </div>
                                <span class="c-black">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label><span style="margin-left: 8px; position: absolute; margin-top: 12px; width: 8px; height: 8px; line-height: 8px; border-radius: 50%; background-color: #80d3ab;"></span></span>
                            </div>

                            <%--<ul class="lv-actions actions list-unstyled list-inline"> <li> <a href="#" > <i class="fa fa-check"></i> </a> </li><li> <a href="#" > <i class="fa fa-clock-o"></i> </a> </li><li> <a data-toggle="dropdown" href="#" > <i class="fa fa-list"></i></a> <ul class="dropdown-menu user-detail" role="menu"> <li> <a href="">Latest</a> </li><li> <a href="">Oldest</a> </li></ul> </li><li> <a data-toggle="dropdown" href="#" data-toggle="tooltip" data-placement="left" title="Tooltip on left"><span class="glyphicon glyphicon-trash"></span></a> <ul class="dropdown-menu user-detail" role="menu"> <li> <a href="">Delete Messages</a> </li></ul> </li></ul>--%>
                        </div>
                        <div class="lv-body dtTable" id="ms-scrollbar" style="overflow: scroll; overflow-x: hidden; height: 250px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" width="100%">
                                <ContentTemplate>

                                    <asp:DataList ID="DtLstMesages" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <div class="lv-item media">
                                                <asp:Panel ID="PanelReplayed" runat="server" Visible='<%# Bind("FlagReplayed") %>'>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-5">


                                                                    <div class="containerReplay msg_ReplayChat">
                                                                        <h6>پاسخ</h6>

                                                                        <div class="msg_Replay">
                                                                            <asp:Label ID="LblReplayMsg" runat="server" Text='<%# Bind("RplyMsg") %>'></asp:Label>
                                                                            <div class="hidden">
                                                                                <asp:Label ID="LblReplayId" runat="server" Text='<%# Bind("RplyId") %>'></asp:Label>
                                                                            </div>


                                                                        </div>
                                                                        <div class="left">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("FullNameRp") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="lv-avatar pull-right">
                                                    %>
                                              
                                                             <%--<img src="./images/bhai.jpg" alt="">--%>
                                                    <asp:Image ID="Image4" runat="server" ImageUrl='<%# Bind("Image") %>' />

                                                </div>
                                                <div class="media-body">
                                                    <div class="ms-item">
                                                        <span class="glyphicon glyphicon-triangle-left" style="color: #000000;"></span>
                                                        <asp:Label ID="Message" runat="server" Text='<%# Bind("Message") %>'></asp:Label>
                                                        <asp:Label ID="LblChatID" runat="server" Text='<%# Bind("ChatID") %>'></asp:Label>
                                                    </div>
                                                    <span class="glyphicon  glyphicon-reply "><i class="fas fa-reply replyIcon"></i></span>
                                                    <small class="ms-date"><span class="glyphicon glyphicon-time"></span>&nbsp; 
                                                                 <asp:Label ID="Date" runat="server" Text='<%# Bind("TimeChat") %>'></asp:Label>-
                                                                 <asp:Label ID="LblNameSender" runat="server" Text='<%# Bind("FullNameS") %>'></asp:Label>
                                                    </small>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<div class="lv-item media right"> <div class="lv-avatar pull-right"> <img src="./images/avatar.jpg" alt=""> </div><div class="media-body"> <div class="ms-item"> We started this site with clear mission that we want to deliver complete details knowledge of Programming to our audience. We are sharing this knowledge in all areas that you can see in our site. </div><small class="ms-date"><span class="glyphicon glyphicon-time"></span>&nbsp; 05/10/2015 at 09:30</small> </div></div>
                                     <div class="lv-item media"> <div class="lv-avatar pull-left"> <img src="./images/bhai.jpg" alt=""> </div><div class="media-body"> <div class="ms-item"> It's gives the power to synthesis anything anywhere you want to. Its the ultimate tool to solve any problem. And we help you excel in that by working with you. </div><small class="ms-date"><span class="glyphicon glyphicon-time"></span>&nbsp; 20/02/2015 at 09:33</small> </div></div>
                                     <div class="lv-item media right"> <div class="lv-avatar pull-right"> <img src="./images/avatar.jpg" alt=""> </div><div class="media-body"> <div class="ms-item"> The basic essence of life is to learn, explore and synthesis. We provide you with the tools to make your dreams come true.Our website is totally for free and available 24/7 and does not consume your data packs and works like a charm on the supersonic lovely internet. </div><small class="ms-date"><span class="glyphicon glyphicon-time"></span>&nbsp; 05/10/2015 at 10:10</small> </div></div>
                                     <div class="lv-item media"> <div class="lv-avatar pull-left"> <img src="./images/bhai.jpg" alt=""> </div><div class="media-body"> <div class="ms-item"> Acadnote a world class website is processing surveys for every student who wants to do something new and different in the field of academics. so it is a right place for every student to share their opinions about their present academics so this website can provide every single student requirements and it is possible for us to do if every student explains about their academics requirements. Last but not the least tell the needs and collect your study materials which we will provide to you. </div><small class="ms-date"><span class="glyphicon glyphicon-time"></span>&nbsp; 05/10/2015 at 10:24</small> </div></div>--%>
                        </div>
                        <div class="clearfix"></div>

                        <div class="row " style="display: none" id="rowReply">
                            <div class="col-lg-5">


                                <div class="containerReplay">
                                    <h6>پاسخ</h6>

                                    <div class="msg_Replay">
                                        <asp:Label ID="LblReplyMsg" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="LblReplyId" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="left">
                                        <asp:Label ID="LabelNameRepaly" Text="fihd" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="lv-footer ms-reply ">
                            <%--<form runat="server">--%>
                            <%--<textarea rows="10" placeholder="Write messages..."></textarea>--%>


                            <div class="float-right">
                                <button runat="server" id="btnInsertMessage" class="button btnInsertMesage">
                                    <span class="glyphicon ">
                                        <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                    </span>
                                </button>
                            </div>
                            <asp:TextBox CssClass="textarea" ID="txtMsg" placeholder="ارسال پیام...." runat="server"></asp:TextBox>
                            <%--<button class=""><span class="glyphicon glyphicon-send"></span></button>--%>
                            <%--<asp:LinkButton ID="Button1" CssClass="button" runat="server" ><span class="glyphicon glyphicon-send"></span></asp:LinkButton>--%>

                            <%--</form>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</div></div></section>--%>
</asp:Content>


