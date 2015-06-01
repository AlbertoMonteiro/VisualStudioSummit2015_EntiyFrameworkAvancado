namespace VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers
{
    public interface ICommandHandler
    {
        void SetNext(ICommandHandler nextCommandHandler);
        AuditEntry HandleCommand(IDbCommandWraper command);
    }
}