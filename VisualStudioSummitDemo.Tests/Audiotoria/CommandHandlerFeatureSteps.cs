using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TechTalk.SpecFlow;
using VisualStudioSummitDemo.Interceptors.Auditoria;
using VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers;

namespace VisualStudioSummitDemo.Tests.Audiotoria
{
    [Binding]
    public class CommandHandlerFeatureSteps
    {
        private AuditEntry _auditEntry;
        private DeleteCommandHandler deleteCommandHandler;
        private IDbCommandWraper _dbCommandWraper;

        [Given(@"um comando delete que vai ser executado")]
        public void DadoUmComandoDeleteQueVaiSerExecutado()
        {
            #region Comando Delete
            const string COMANDO_DELETE = "DELETE [dbo].[Contato]\nWHERE ([Id] = @0)";
            #endregion

            var dbCommandWraper = Substitute.For<IDbCommandWraper>();
            var dbTransaction = Substitute.For<DbTransaction>();
            _dbCommandWraper = dbCommandWraper;
            _dbCommandWraper.CommandText.Returns(COMANDO_DELETE);
            _dbCommandWraper.Transaction.Returns(dbTransaction);
            var dbParameters = new List<DbParameter>
            {
                new SqlParameter {ParameterName = "@0", Value = 3}
            };
            _dbCommandWraper["@0"].Returns(dbParameters.Last());
            _dbCommandWraper.Parameters.Returns(dbParameters);

            var dbCommandInterceptionContext = new DbCommandInterceptionContext<int>();
            deleteCommandHandler = new DeleteCommandHandler(dbCommandInterceptionContext);
        }

        [When(@"o delete for executado")]
        public void QuandoODeleteForExecutado()
        {
            _auditEntry = deleteCommandHandler.HandleCommand(_dbCommandWraper);
        }

        [Then(@"ele deve ter o tipo ""(.*)""")]
        public void EntaoEleDeveTerOTipoDelete(string tipo)
        {
            AuditEntryKind valor;
            AuditEntryKind.TryParse(tipo, out valor);
            Assert.AreEqual(valor, _auditEntry.Kind);
        }

        [Then(@"o id precisa ser diferente de (.*)")]
        public void EntaoOIdPrecisaSerDiferenteDe(int p0)
        {
            Assert.AreEqual(3, _auditEntry.EntityId);
        }

        [Then(@"a data de criação deve ser preenchida")]
        public void EntaoADataDeCriccaoDeveSerPreenchida()
        {
            Assert.AreNotEqual(DateTime.MinValue, _auditEntry.Created);
        }

        [Then(@"os novos valores devem ser preenchidos")]
        public void EntaoOsNovosValoresDevemSerPreenchidos()
        {
            Assert.AreEqual("", _auditEntry.NewValue);
        }

        [Then(@"deve conter o nome da tabela")]
        public void EntaoDeveConterONomeDaTabela()
        {
            Assert.AreEqual("Contato", _auditEntry.Table);
        }
    }
}
