<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskScore.aspx.cs" Inherits="ECSEL.Candidate.TaskScore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Task Score</h1>

    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <strong>
    <br />
    <br />

    Core Topic Scores For <asp:Label ID="SeminarLabel" runat="server"></asp:Label>:
        </strong>

    <asp:GridView ID="TaskScoreGrid" runat="server" DataSourceID="TaskScoreDataSource" DataKeyNames="RatingID" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="coretopicdesc" HeaderText="Core Topic" />
                <asp:BoundField DataField="Cscore" HeaderText="Conceptual" />
                <asp:BoundField DataField="Sscore" HeaderText="Strategic" />
                <asp:BoundField DataField="Pscore" HeaderText="Personal" />
            </Columns>
            <EmptyDataTemplate>
                No Scores 
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="TaskScoreDataSource" runat="server" TypeName="ledeDB"
            SelectMethod="getTaskRatings">
            <SelectParameters>
                <asp:QueryStringParameter name="versid" QueryStringField="versid"/>
            </SelectParameters>
        </asp:ObjectDataSource> 


    <strong>
    <br />


    Other Core Topic Scores</strong>
        <asp:GridView ID="CoreScoreGrid" runat="server" DataSourceID="CoreScoreDataSource" DataKeyNames="RatingID" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="coretopicdesc" HeaderText="Core Topic" />
                <asp:BoundField DataField="Cscore" HeaderText="Conceptual" />
                <asp:BoundField DataField="Sscore" HeaderText="Strategic" />
                <asp:BoundField DataField="Pscore" HeaderText="Personal" />
            </Columns>
            <EmptyDataTemplate>
                No Scores 
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="CoreScoreDataSource" runat="server" TypeName="ledeDB"
            SelectMethod="getCoreRatings">
            <SelectParameters>
                <asp:QueryStringParameter name="versid" QueryStringField="versid"/>
            </SelectParameters>
        </asp:ObjectDataSource> 
    
    <strong>
    <br />
    
    Impact Scores       
    </strong>       
    <asp:GridView ID="ImpactScoreGrid" runat="server" DataSourceID="ImpactScoreDataSource" DataKeyNames="RatingID" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="sscore" HeaderText="Structures & Operations" />
            <asp:BoundField DataField="pscore" HeaderText="Professional Practices" />
            <asp:BoundField DataField="lscore" HeaderText="Student Learning" />
        </Columns>
        <EmptyDataTemplate>
                No Scores 
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:ObjectDataSource ID="ImpactScoreDataSource" runat="server" TypeName="ledeDB"
            SelectMethod="getImpactGrid">
        <SelectParameters>
            <asp:QueryStringParameter name="versid" QueryStringField="versid"/>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
