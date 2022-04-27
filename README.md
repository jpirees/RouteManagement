<h1>
  Sistema de Gerenciamento de Rotas
  </h1>
  
  <p>Com o objetivo de desenvolver um Projeto para "Cadastrar" rotas de serviço de uma determinada empresa, a partir das informações fornecidas através de um arquivo excel (.xlsx) e assim determinar as equipes para os serviços em determinadas regiões e gerar um relatório Word (.doc) a respeito do dia atual, mostrando todas as rotas a ser realizadas naquele determinado dia!!</p>
  
 
<h2>Bibliotecas utilizadas</h2>
 
<li>
  EPPlus (Ler arquivos Excel)
 
<h2>
  Como utilizar:
</h2>
  
<li>
  Tenha um servidor rodando MongoDB na porta padrão para utilização.
<li>
  Selecione  o projeto "RoutesManagement" e rode o comando <b>update-database</b> no console de gerenciamento de pacotes para criar o banco de dados SQL, que será utilizado para realizar autenticação.
<li>
  Para criar um usuário que é capaz de criar outros usuários (administrador), utilize o email "admin@mail.com" (só pode haver um administrador na plataforma).
<li>
  Depois que efetuar o Login (entrar na plataforma). Para que o sistema funcione corretamente cadastre na seguinte ordem: Pessoas => Cidades => Times.
  
  
<h2>
  Feito até o momento:
</h2>


<li>
  API de Pessoas
<li>
  API de Times
<li>
  API de Cidades
<li>
  MVC de Pessoa (Listagem, Criação, Edição, Exclusão)
<li>
  MVC de Time (Listagem, Criação, Edição, Exclusão)
<li>
  MVC de Cidades (Listagem, Criação, Edição, Exclusão)
<li>
  MVC de Documento (Upload)
<li>
  Leitura do Arquivo Excel (.xlsx)
<li>
  Selecionar os dados da planilha que serão exibidos
<li>
  Selecionar o tipo de serviço disponível
<li>
  Gerar documento Word (.docx) com os times e suas rotas


