using System;
using System.Data.Entity.Core.Common.CommandTrees;
using NUnit.Framework;
using VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers;

namespace VisualStudioSummitDemo.Tests
{
    [TestFixture]
    public class CommandTreeFactoryTest
    {
        [TestCase(DbCommandTreeKind.Delete, typeof(DbDeleteCommandTreeHandler))]
        [TestCase(DbCommandTreeKind.Insert, typeof(DbInsertCommandTreeHandler))]
        [TestCase(DbCommandTreeKind.Query, typeof(DbSelectCommandTreeHandler))]
        [TestCase(DbCommandTreeKind.Update, typeof(DbUpdateCommandTreeHandler))]
        public void FactoryRetornaHandlerCorreto(DbCommandTreeKind dbCommandTreeKind, Type type)
        {
            var commandTreeHandler = CommandTreeFactory.GetCommandTreeHandler(dbCommandTreeKind);

            Assert.IsInstanceOf(type, commandTreeHandler);
        }
    }
}
