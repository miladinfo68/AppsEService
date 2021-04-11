<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageConatctst.Master" AutoEventWireup="true" CodeBehind="ContactStudents.aspx.cs" Inherits="IAUEC_Apps.UI.Contact.ContactStudent.ContactStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="LblSearchCount" runat="server" Text="0"></asp:Label>
    <asp:Label ID="LblSearchText" runat="server" Text=""></asp:Label>
    <asp:Label ID="LblCurCount" runat="server" Text="0"></asp:Label>
    <asp:Label ID="LblLastIdChat" runat="server" CssClass="hidden"></asp:Label>




    <div class="modal fade deleteChat" tabindex="-1" role="dialog" aria-labelledby="deleteChatLabel" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <p>آیا از حذف پیام  مطمئن هستید؟</p>
                    <p id="findChatDelete" class="hidden"></p>
                    <p id="ChatIdDelete" class="hidden"></p>
                    <p id="SenderIdDelete" class="hidden"></p>
                    <p id="ReciverIdDelete" class="hidden"></p>
                    <p id="GroupIdDelete" class="hidden"></p>
                    <p id="dateDelete" class="hidden"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger acceptDelete">تایید</button>
                    <button type="button" class="btn btn-secondary " data-dismiss="modal">منصرف شدم</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade AlertDeleteChat" tabindex="-1" role="dialog" aria-labelledby="deleteChatLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <p id="msgAlertDeleteChat"></p>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary " data-dismiss="modal">تایید</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade AlertUploadFile" tabindex="-1" role="dialog" aria-labelledby="AlertUploadFile" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <p>آیا مایل به ذخیره فایل هستید؟</p>
                    <p id="AlertUploadFile"></p>

                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-secondary acceptFileSave" data-dismiss="modal">تایید</button>

                    <button type="button" class="btn btn-secondary " data-dismiss="modal">منصرف شدم</button>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="container ng-scope">
            <div class="block-header">
                <h2></h2>
                <asp:Label ID="LblIdUser" runat="server" CssClass="hide" Text="Label"></asp:Label>
                <asp:Label ID="LblIdGrp" runat="server" CssClass="hide" Text=""></asp:Label>
            </div>
            <div class="card m-b-0" id="messages-main" style="box-shadow: 0 0 40px 1px #c9cccd;">
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
                        <asp:DataList ID="DtlstContact" runat="server" OnItemCommand="DtlstContact_ItemCommand" CssClass="disabled">
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
                        <div class="lv-body dtTable" id="ms-scrollbar" style="overflow: scroll; overflow-x: hidden; height: 320px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" width="100%">
                                <ContentTemplate>

                                    <asp:DataList ID="DtLstMesages" runat="server" Width="100%">
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


                                                    <%--<img src="./images/bhai.jpg" alt="">--%>
                                                    <telerik:RadBinaryImage ID="Image4" runat="server" VisibleWithoutSource="False" DataValue='<%# Bind("Images") %>'
                                                        Style="width: 30px; height: 30px" />

                                                </div>
                                                <div class="media-body">
                                                    <div class="ms-item">
                                                        <span class="glyphicon glyphicon-triangle-left" style="color: #000000; display: unset"></span>
                                                        <asp:Label ID="LblChatID" runat="server" CssClass="hide" Text='<%# Bind("ChatID") %>'></asp:Label>

                                                        <asp:Label ID="Message" runat="server" Font-Size="Smaller" Text='<%#(string) Eval("Message").ToString().Replace("@$file send@$"," فایل ").Replace("@$sound voice@$blob"," فایل صوتی ") %>' CssClass='<%#( ((int)Eval("IDTypeFile") ==1)||((Eval("IsDeleted")!=null)&&((bool)Eval("IsDeleted") ==true))? string.Empty :"hidden")%>'></asp:Label>
                                                        <asp:Panel ID="Panelaudio" runat="server" CssClass='<%#  ((int)Eval("IDTypeFile") ==2)&&((Eval("IsDeleted")==null)||(((bool)Eval("IsDeleted")) ==false)) ? "" :"hidden"%> '>
                                                            <audio style="max-width: 200px" controls controlslist="nodownload">
                                                                <source src='<%# Eval("FormatFile").ToString().Trim()==".wav"?"../SoundRecorder/"+Eval("ChatID")+".wav":""%>' <%# Eval("FormatFile").ToString().Trim()==".wav"?"type=audio/mpeg":""%>>>
                                                            </audio>
                                                        </asp:Panel>
                                                        <asp:Panel ID="PanelFile" runat="server" CssClass='<%#((int)Eval   ("IDTypeFile") ==3)&&((Eval("IsDeleted")==null)||(((bool)Eval("IsDeleted")) ==false)) ? string.Empty :"hidden"%>'>

                                                            <span>
                                                                <a class="btn btn-link linkFile" style="width: 100%" href="../FileSaver/<%# Eval("ChatID")%><%# Eval("FormatFile")%>" download><%# Eval("Message").ToString().Replace("@$file send@$","")%></a>
                                                            </span>

                                                        </asp:Panel>

                                                    </div>

                                                    <span class="glyphicon  glyphicon-reply ">
                                                        <i class="fa fa-reply replyIcon" data-toggle="tooltip" data-placement="left" title="پاسخ"></i>
                                                        <i class="fa fa-trash-o  trashIcon    
                                                            <%#(((Eval("IsDeleted")!=null)&&(((bool)Eval("IsDeleted")) ==true)) ? "hidden":string.Empty) %>
                                                            
                                                            "
                                                            data-target=".deleteChat" aria-hidden="true" data-toggle="tooltip" data-placement="left" title="حذف"></i>


                                                    </span>

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
                        <div class="clearfix"></div>

                        <div class="row rep" style="display: none" id="rowReply">
                            <div class="col-lg-5">


                                <div class="containerReplay">
                                    <h4 class="left cancelReplay" style="margin-left: 10px">
                                        <button type="button" class="close" data-dismiss="rep">&times;</button></h4>
                                    <h6>پاسخ</h6>

                                    <div class="msg_Replay text-right">
                                        <asp:Label ID="LblReplyMsg" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="LblReplyIdD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="left">
                                        <span><i class="fa fa-user"></i></span><span style="padding-left: 4px">
                                            <asp:Label ID="LabelNameRepaly" Text="fihd" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="lv-footer ms-reply ">
                            <%--<form runat="server">--%>
                            <%--<textarea rows="10" placeholder="Write messages..."></textarea>--%>
                            <div class="float-right">
                                <button runat="server" id="btnInsertMessage" class="button                          btnInsertMesage" tabindex="-10">
                                    <span class="glyphicon ">
                                        <i class="fa fa-pencil"></i>
                                    </span>
                                </button>
                            </div>
                            <div style="width: 75%">
                                <div class="float-right" tabindex="100000000">
                                    <input type="file" class="file hidden" />
                                    <button id="btnInsertFile" class="button btnInsertFile" tabindex="10000">
                                        <span class="glyphicon ">
                                            <i class="fa fa-upload fa-file-upload" aria-hidden="true"></i>
                                        </span>
                                    </button>
                                </div>


                                <div class="float-right">
                                    <button class="button recordVoice" tabindex="10">
                                        <span class="glyphicon ">
                                            <i class="fa fa-microphone" aria-hidden="true"></i>
                                        </span>
                                    </button>
                                    <button class="button stopVoice hidden" tabindex="11">
                                        <span class="glyphicon ">
                                            <i class="fa fa-stop  "></i>
                                        </span>
                                    </button>
                                </div>
                            </div>
                            <div style="width: 75%">
                                <input type="text" class="textarea" id="txtMsg" placeholder="ارسال پیام...." autocomplete="off" />
                                <%--<button class=""><span class="glyphicon glyphicon-send"></span></button>--%>
                                <%--<asp:LinkButton ID="Button1" CssClass="button" runat="server" ><span class="glyphicon glyphicon-send"></span></asp:LinkButton>--%>
                            </div>
                            <%--</form>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="/Contact/Script/ContactSt.js"></script>
</asp:Content>
