<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="FaleConosco.aspx.cs" Inherits="Projeto3.FaleConosco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="margin-top-60">
        <div class="row">
            <div class="col-6">
                <div class="box border margin-right-20">
                    <h2>Fale Conosco</h2>
                    <br />
                    <asp:Label ID="MensagemErro" ForeColor="red" Font-Size="16px" runat="server"></asp:Label>
                    <label>DIGITE SEU NOME</label>
                    <asp:TextBox ID="NomeCompleto" MaxLength="60" runat="server"></asp:TextBox>
                    <label>E-MAIL</label>
                    <asp:TextBox ID="SeuEmail" MaxLength="256" runat="server"></asp:TextBox>
                    <label>MENSAGEM </label>
                    <asp:TextBox ID="Mensagem" MaxLength="256" TextMode="MultiLine" Rows="6"  runat="server"></asp:TextBox>

                    <asp:Button ID="Enviar" OnClick="Enviar_Click" CssClass="botao-inserir" runat="server" Text="Enviar" />
                </div>
            </div>
            <div class="col-6">
            </div>
        </div>
    </div>
</asp:Content>
