#language: pt-BR
Funcionalidade: Command Handlers

Cenário: Interceptador de auditoria captura o comando delete
	Dado um comando delete que vai ser executado
    Quando o delete for executado
    Então ele deve ter o tipo "Delete"
    E o id precisa ser diferente de 0
    E a data de criação deve ser preenchida
    E os novos valores devem ser preenchidos
    E deve conter o nome da tabela

#s_UpdateCommandTest
#s_InsertCommandTest