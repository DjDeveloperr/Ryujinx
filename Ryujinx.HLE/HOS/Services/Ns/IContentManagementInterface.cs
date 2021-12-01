using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Ns
{
    struct GetTotalSpaceSizeArgs {
        public byte StorageID;
    }

    [Service("ns:cmi")]
    class IContentManagementInterface : IpcService
    {
        public IContentManagementInterface(ServiceCtx context) { }
        
        [CommandHipc(43)]
        public ResultCode CheckSdCardMountStatus(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.ServiceNs);
            return ResultCode.Success;
        }

        [CommandHipc(47)]
        public ResultCode GetTotalSpaceSize(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.ServiceNs, new GetTotalSpaceSizeArgs {
                StorageID = context.RequestData.ReadByte()
            });
            return ResultCode.Success;
        }

        [CommandHipc(48)]
        public ResultCode GetFreeSpaceSize(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.ServiceNs, new GetTotalSpaceSizeArgs {
                StorageID = context.RequestData.ReadByte()
            });
            return ResultCode.Success;
        }
    }
}