using Command.Implementation;
using DTO.Interfaces;

namespace DataCommand.Implementation
{
    public abstract class CRUDCommandManager : CommandManager
    {
        private IDTOWrapper _source;

        protected CRUDCommandManager(IDTOWrapper source, IDTOCollection target)
        {
            _source = source;

            AddCommand(CRUDCommands.CreateCommand, new CreateCommandBase(source, target, CanDoCreate));
            AddCommand(CRUDCommands.UpdateCommand, new UpdateCommandBase(source, target, CanDoUpdate));
            AddCommand(CRUDCommands.DeleteCommand, new DeleteCommandBase(source, target, CanDoDelete));
        }

        private bool CanDoCreate()
        {
            return FurtherConditionCreate();
        }

        private bool CanDoUpdate()
        {
            return _source.DataObject != null && FurtherConditionUpdate();
        }

        private bool CanDoDelete()
        {
            return _source.DataObject != null && FurtherConditionDelete();
        }

        protected abstract bool FurtherConditionCreate();
        protected abstract bool FurtherConditionUpdate();
        protected abstract bool FurtherConditionDelete();
    }
}