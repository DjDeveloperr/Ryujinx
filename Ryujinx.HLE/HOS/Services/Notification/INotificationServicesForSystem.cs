using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Notification
{
    [Service("notif:s")] // 9.0.0+
    class INotificationServicesForSystem : IpcService
    {
        public INotificationServicesForSystem(ServiceCtx context) { }

        [CommandHipc(520)]
        public ResultCode ListAlarmSettings(ServiceCtx context)
        {
            context.ResponseData.Write((int)0);
            return ResultCode.Success;
        }

        [CommandHipc(1040)]
        public ResultCode OpenNotificationSystemEventAccessor(ServiceCtx context)
        {
            MakeObject(context, new INotificationSystemEventAccessor(context));
            Logger.Stub?.PrintStub(LogClass.Service, "Notification");
            return ResultCode.Success;
        }

        [CommandHipc(1510)]
        public ResultCode GetPresentationSettings(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }
    }
}