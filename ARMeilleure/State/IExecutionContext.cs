using ARMeilleure.Memory;
using System;
using System.Diagnostics;

namespace ARMeilleure.State
{
    public interface IExecutionContext
    {
        void SetCntfrqEl0(ulong value);
        ulong GetCntfrqEl0();

        long GetTpidrEl0();
        void SetTpidrEl0(long value);

        long GetTpidr();
        void SetTpidr(long value);        

        FPCR GetFpcr();
        void SetFpcr(FPCR value);

        FPSR GetFpsr();
        void SetFpsr(FPSR value);

        bool GetIsAarch32();
        void SetAarch32(bool val);

        bool IsRunning();
        void SetRunning(bool value);

        event EventHandler<EventArgs> Interrupt;
        event EventHandler<InstExceptionEventArgs> Break;
        event EventHandler<InstExceptionEventArgs> SupervisorCall;
        event EventHandler<InstUndefinedEventArgs> Undefined;
    
        ulong GetX(int index);
        void SetX(int index, ulong value);

        V128 GetV(int index);
        void SetV(int index, V128 value);

        bool GetPstateFlag(PState flag);
        void SetPstateFlag(PState flag, bool value);

        bool GetFPstateFlag(FPState flag);
        void SetFPstateFlag(FPState flag, bool value);

        void CheckInterrupt();

        void RequestInterrupt();

        void OnBreak(ulong address, int imm);
        void OnSupervisorCall(ulong address, int imm);
        void OnUndefined(ulong address, int opCode);

        void StopRunning();

        void Dispose();   
    }
}