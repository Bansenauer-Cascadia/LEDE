<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Site1.Master" AutoEventWireup="true" CodeBehind="TaskScore.aspx.cs" Inherits="LEDE_MVC.WebForms.TaskScore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Task Score</h1>

    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <strong>
    <br />

    Core Topic Scores
        </strong>
        <asp:GridView ID="CoreScoreGrid" runat="server" DataSourceID="CoreScoreDataSource" DataKeyNames="RatingID" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="coretopicdesc" HeaderText="Core Topic" />
                <asp:BoundField DataField="Cscore" HeaderText="C Score" />
                <asp:BoundField DataField="Pscore" HeaderText="P Score" />
                <asp:BoundField DataField="Sscore" HeaderText="S Score" />
            </Columns>
            <EmptyDataTemplate>
                No Scores 
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="CoreScoreDataSource" runat="server" TypeName="ledeDB"
            SelectMethod="getCoreScores">
            <SelectParameters>
                <asp:QueryStringParameter name="versid" QueryStringField="versid"/>
                <asp:Parameter Name="userid"/>
            </SelectParameters>
        </asp:ObjectDataSource> 
    
    <strong>
    <br />
    
    Impact Scores       
    </strong>       
    <asp:GridView ID="ImpactScoreGrid" runat="server" DataSourceID="ImpactScoreDataSource" DataKeyNames="RatingID" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="sscore" HeaderText="S Score" />
            <asp:BoundField DataField="pscore" HeaderText="P Score" />
            <asp:BoundField DataField="lscore" HeaderText="L Score" />
        </Columns>
        <EmptyDataTemplate>
                No Scores 
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:ObjectDataSource ID="ImpactScoreDataSource" runat="server" TypeName="ledeDB"
            SelectMethod="getImpactScores">
        <SelectParameters>
            <asp:QueryStringParameter name="versid" QueryStringField="versid"/>
            <asp:Parameter Name="userid"/>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
