<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="CRUD.EmployeeDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/@mdi/font/css/materialdesignicons.min.css" rel="stylesheet" />
    <style>
        /* Custom styles for the Material Design theme */
        .form-control {
            border-radius: 0.5rem; /* Rounded corners */
            box-shadow: none; /* Remove default shadow */
            transition: border-color 0.3s ease, box-shadow 0.3s ease; /* Smooth transition for border and shadow */
        }

        .form-control:hover, .form-control:focus {
            border-color: #3F51B5; /* Change border color on hover/focus */
            box-shadow: 0 0 5px rgba(63, 81, 181, 0.5); /* Light shadow effect on focus */
        }

        .table {
            transition: background-color 0.3s ease; /* Smooth transition for background color */
        }

        /* Styles for GridView rows */
        .gridview-row {
            transition: background-color 0.3s ease; /* Smooth transition for background color */
        }

        .gridview-row:hover {
            background-color: #e3f2fd; /* Light blue background for GridView row hover */
        }

        .form-label {
            font-weight: 500; /* Bold labels */
        }

        .btn {
            border-radius: 0.5rem; /* Rounded button */
        }

        .form-group {
            margin-bottom: 1.5rem; /* Spacing between form groups */
        }

        .container {
            margin-top: 50px; /* Top margin for the container */
        }

        .error-message {
            color: red; /* Red color for error messages */
            font-size: 0.9rem; /* Slightly smaller font size */
        }

        .feedback-message {
            color: green; /* Green color for success messages */
            font-weight: bold; /* Bold text for feedback */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> <!-- Added ScriptManager -->
        <div class="container-fluid">
            <h2 class="text-center">CRUD Operation on Employee Details</h2>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-md-10 col-lg-8">
                            <div id="formError" class="alert alert-danger d-none" role="alert"></div>
                            
                            <!-- Employee Form -->
                            <div class="form-group">
                                <label for="txtFirstName" class="form-label">First Name</label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name" />
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" 
                                    ErrorMessage="First Name is required." CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtLastName" class="form-label">Last Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name" />
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" 
                                    ErrorMessage="Last Name is required." CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtDOB" class="form-label">Date of Birth</label>
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" />
                                <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB" 
                                    ErrorMessage="Date of Birth is required." CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtPosition" class="form-label">Position</label>
                                <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" placeholder="Enter Position" />
                                <asp:RequiredFieldValidator ID="rfvPosition" runat="server" ControlToValidate="txtPosition" 
                                    ErrorMessage="Position is required." CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" />
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" 
                                    ErrorMessage="Email is required." CssClass="error-message" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" 
                                    ErrorMessage="Please enter a valid email address." CssClass="error-message" 
                                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtPhone" class="form-label">Phone</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter Phone" />
                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" 
                                    ErrorMessage="Phone number is required." CssClass="error-message" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone"
                                    ErrorMessage="Phone number must be exactly 10 digits." 
                                    ValidationExpression="^\d{10}$" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtAddress" class="form-label">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" />
                                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" 
                                    ErrorMessage="Address is required." CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="d-flex justify-content-between flex-wrap">
                                <asp:Button ID="btnInsert" runat="server" Text="Insert" CssClass="btn btn-primary mb-2" OnClick="btnInsert_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning mb-2" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger mb-2" OnClick="btnDelete_Click" />
                            </div>
                            
                            <!-- Feedback Message Label -->
                            <asp:Label ID="lblFeedback" runat="server" CssClass="feedback-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Employee GridView -->
                    <div class="mt-4">
                        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" 
                            OnSelectedIndexChanged="gvEmployees_SelectedIndexChanged" 
                            DataKeyNames="EmployeeID" 
                            OnRowDataBound="gvEmployees_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                <asp:TemplateField HeaderText="Date of Birth">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DateOfBirth", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Position" HeaderText="Position" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>