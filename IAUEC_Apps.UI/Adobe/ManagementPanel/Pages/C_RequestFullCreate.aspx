<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_RequestFullCreate.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.C_RequestFullCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     
		<!-- import the Jalali Date Class script -->
		<script type="text/javascript" src="../../js/jalali.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../../js/calendar.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../../js/calendar-setup.js"></script>
		
		<!-- import the language module -->
		<script type="text/javascript" src="../../js/lang/calendar-fa.js"></script>

      

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>

    <div>
        <asp:Label ID="lbl_ClassName" runat="server" Text="نام کلاس"></asp:Label>
        <asp:TextBox ID="txt_ClassName" runat="server" Text="" ></asp:TextBox>

        <asp:Label ID="lbl_ClassNameLatin" runat="server" Text="نام لاتین کلاس "></asp:Label>
        <asp:TextBox ID="txt_ClassNameLatin" runat="server" data-original-title="از حروف انگلیسی استفاده نمایید"   
             onKeypress="if ( event.keyCode<48 || (event.keyCode>57 && event.keyCode < 65) || (event.keyCode > 91 && event.keyCode <97) || event.keyCode>122) event.returnValue = false;" >
        </asp:TextBox>
                   
        <br />
        
        <asp:Label ID="lbl_ClassType" runat="server" Text="نوع دوره"></asp:Label>
        <asp:DropDownList ID="ddl_ClassType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_ClassType_SelectedIndexChanged"></asp:DropDownList>

        <asp:Label ID="lbl_UserType" runat="server" Text="مجوز حضور مهمان در کلاس"></asp:Label>
        <asp:DropDownList ID="ddl_UserType" runat="server"></asp:DropDownList>
              
        <div runat="server" id="UploadFile1" visible="true">
            <div >
                <asp:Label ID="lbl_ProfessorGuide" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_ProfessorAdvisor" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_ProfessorRefereeOne" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_ProfessorRefereeTwo" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_Student" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_Type" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_UserId" runat="server" Text="" Visible="false"></asp:Label>
            </div>

            <tr>
                <td style="width: 15%; ">تاریخ دفاع:</td>            
                <td style="width: 20%; ">      
                    <div class="example">			
                        <input id="date_input_3" type="text" runat="server"  /><img id="date_btn_3" src="../../images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "ContentPlaceHolder1_date_input_3",   // id of the input field
                                button: "date_btn_3",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
                        </script>
                        <script type="text/javascript">
                            setActiveStyleSheet(document.getElementById("defaultTheme"), "Aqua");
                        </script>
                    </div>		
                </td>
            </tr>
            <asp:Label ID="Label1" runat="server" Text="روز و ساعت کلاس"></asp:Label>
            <telerik:RadComboBox ID="ddl_ClassDayTime2" runat="server" 
                Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false"
                DataValueField="SupplierID"
                    Skin="Office2010Silver">
            </telerik:RadComboBox>
            <br />

            <asp:Label ID="lbl_SelectStudent" runat="server" Text="انتخاب دانشجو"></asp:Label>
            <asp:Button ID="btn_SelectStudent" runat="server" Text="انتخاب/ویرایش"  OnClick="btn_SelectStudent_Click" />
            <asp:Label ID="lbl_SelectProfessorGuide" runat="server" Text="انتخاب استاد راهنما"></asp:Label>
            <asp:Button ID="btn_SelectProfessorGuide" runat="server" Text="انتخاب/ویرایش" OnClick="btn_SelectProfessorGuide_Click" />
            <asp:Label ID="lbl_SelectProfessorAdvisor" runat="server" Text="انتخاب استاد مشاور"></asp:Label>
            <asp:Button ID="btn_SelectProfessorAdvisor" runat="server" Text="انتخاب/ویرایش"  OnClick="btn_SelectProfessorAdvisor_Click" />
            <asp:Label ID="lbl_SelectProfessorRefereeOne" runat="server" Text="انتخاب داور شماره یک"></asp:Label>
            <asp:Button ID="btn_SelectProfessorRefereeOne" runat="server" Text="انتخاب/ویرایش"  OnClick="btn_SelectProfessorRefereeOne_Click" />
            <asp:Label ID="lbl_SelectProfessorRefereeTwo" runat="server" Text="انتخاب داور شماره دو "></asp:Label>
            <asp:Button ID="btn_SelectProfessorRefereeTwo" runat="server" Text="انتخاب/ویرایش"  OnClick="btn_SelectProfessorRefereeTwo_Click" />            
            <br />

            <telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Default"  >        
                <MasterTableView Dir="RTL">
                    <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                    <HeaderStyle Font-Names="tahoma" />
                    <AlternatingItemStyle Font-Names="tahoma" />
                        
                    <Columns >
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="UserId" HeaderText="شماره کاربر"  >
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="نام کاربر"  >
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Family" HeaderText="نام خانوادگی">
                        </telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn DataField="FatherName" HeaderText="نام پدر">
                        </telerik:GridBoundColumn>    
                        <telerik:GridBoundColumn DataField="NationalCode" HeaderText="شماره ملی">
                        </telerik:GridBoundColumn>        
                        <telerik:GridBoundColumn DataField="Mobile" HeaderText="تلفن همراه">
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn DataField="Email" HeaderText="پست الکترونیکی">
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn DataField="Field" HeaderText="رشته تحصیلی">
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn DataField="Type" HeaderText="نوع کاربر">
                        </telerik:GridBoundColumn>  
              
                        <telerik:GridHyperLinkColumn DataNavigateUrlFields= "ProfessorGuide,ProfessorAdvisor,ProfessorRefereeOne,ProfessorRefereeTwo,Student,Type2,UserId" Text="حذف"
                            DataNavigateUrlFormatString= "C_RequestFullCreate.aspx?ProfessorGuide={0}&ProfessorAdvisor={1}&ProfessorRefereeOne={2}&ProfessorRefereeTwo={3}&Student={4}&Type={5}&UserId={6}">
                        </telerik:GridHyperLinkColumn> 
                    </Columns>

                </MasterTableView>
            </telerik:RadGrid>

        </div>

        <div runat="server" id="UploadFile2" visible="false">
            <tr>
                <td style="width: 15%; ">تاریخ شروع:</td>            
                <td style="width: 20%; ">      
                    <div class="example">			
                        <input id="date_input_1" type="text" runat="server"  /><img id="date_btn_1" src="../../images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "ContentPlaceHolder1_date_input_1",   // id of the input field
                                button: "date_btn_1",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
                        </script>
                        <script type="text/javascript">
                            setActiveStyleSheet(document.getElementById("defaultTheme"), "Aqua");
                        </script>
                    </div>		
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 50px;">تاریخ پایان:</td>
                <td >
                    <div >			
                        <input id="date_input_2" type="text" runat="server"  /><img id="date_btn_2" src="../../images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "ContentPlaceHolder1_date_input_2",   // id of the input field
                                button: "date_btn_2",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
                        </script>
                    </div>
                </td>
            </tr>        
            <div>     
                <asp:Label ID="lbl_ClasDayTime" runat="server" Text="روز و ساعت کلاس"></asp:Label>
                <telerik:RadComboBox ID="ddl_ClassDayTime" Runat="server" CheckBoxes="True" 
                      EmptyMessage="  انتخاب نمایید "  Font-Names="Tahoma" Width="100%" Skin="Windows7">
                        <Localization AllItemsCheckedString="همه گزینه ها انتخاب شد" 
                            ItemsCheckedString="تعداد گزینه های انتخاب شده" 
                            ShowMoreFormatString="گزینه ها &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; out of &lt;b&gt;{1}&lt;/b&gt;" />
                </telerik:RadComboBox>
                <br />           
                <asp:Label ID="lbl_ClassCount" runat="server" Text="تعداد جلسات"></asp:Label>
                <asp:DropDownList ID="ddl_ClassCount" runat="server"></asp:DropDownList>
                <asp:Label ID="lbl_UserCount" runat="server" Text="تعداد کاربران"></asp:Label>
                <asp:RangeValidator id="id_UserCount" runat="server" ErrorMessage=""
                    ControlToValidate="txt_UserCount" MaximumValue="100" MinimumValue="1"
                    Type="Integer"> </asp:RangeValidator>
                <input type="text" runat="server" id="txt_UserCount"
                        data-original-title="از اعداد استفاده نمایید" 
                     onKeypress="if ( event.keyCode<48 || event.keyCode>57  ) event.returnValue = false;" >                
            </div>

            <div >
                <asp:Label ID="lbl_ProfUser" runat="server" Text="ارسال فایل اکسل کابران استاد"></asp:Label>
                <INPUT id="fi_ProfUsers"  type="file"  name="fi_ProfUsers" runat="server" 
                    önchange="checkfile(this);"  /> 
                <br />
                <asp:Label ID="lbl_Note12" runat="server" Text="توصیه می شود، برای مدیریت بهتر کلاس درس، تنها یک کاربر با دسترسی استاد در نظر گرفته شود"></asp:Label>
            </div>

            <br /><br /> 
            <div>
                <asp:Label ID="lbl_StudentUser" runat="server" Text="ارسال فایل اکسل کابران دانشجو"></asp:Label>
                <INPUT id="fi_StudentUser"  type="file"  name="fi_StudentUser" runat="server" 
                    önchange="checkfile(this);"/>        
            </div>
        </div>
        





        <br />
        <asp:Button ID="btn_CreateRequest" runat="server" Text="ثبت درخواست" OnClick="btn_CreateRequest_Click" />
    </div>


    <telerik:RadGrid ID="RadGrid1" runat="server" Visible="true" AutoGenerateColumns="False"  Skin="MyCustomSkin" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" >    
        <MasterTableView Dir="RTL">            
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />                     
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>  
                <telerik:GridBoundColumn DataField="Name" HeaderText="نام">
                </telerik:GridBoundColumn>             
                <telerik:GridBoundColumn DataField="Family" HeaderText="نام خانوادگی">
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="NationalCode" HeaderText="شماره ملی">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Access" HeaderText="سطح کاربر">
                </telerik:GridBoundColumn>            
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>



</asp:Content>
