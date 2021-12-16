using ARMeilleure.Memory;
using ARMeilleure.State;
using System;
using System.Diagnostics;

namespace ARMeilleure.Hypervisor
{
    public class AppleHypervisor: IExecutionContext
    {
        public ulong CntfrqEl0 { get; set; }

        public ulong GetCntfrqEl0()
        {
            return CntfrqEl0;
        }

        public void SetCntfrqEl0(ulong value)
        {
            CntfrqEl0 = value;
        }

        public long TpidrEl0 { get; set; }
        public long Tpidr    { get; set; }

        public long GetTpidrEl0()
        {
            return TpidrEl0;
        }

        public void SetTpidrEl0(long value)
        {
            TpidrEl0 = value;
        }

        public long GetTpidr()
        {
            return Tpidr;
        }

        public void SetTpidr(long value)
        {
            Tpidr = value;
        }

        public FPCR Fpcr { get; set; }
        public FPSR Fpsr { get; set; }

        public FPCR GetFpcr()
        {
            return Fpcr;
        }

        public void SetFpcr(FPCR value)
        {
            Fpcr = value;
        }

        public FPSR GetFpsr()
        {
            return Fpsr;
        }

        public void SetFpsr(FPSR value)
        {
            Fpsr = value;
        }

        public bool IsAarch32 { get; private set; }

        public bool GetIsAarch32()
        {
            return IsAarch32;
        }

        public void SetAarch32(bool value)
        {
            IsAarch32 = value;
        }

        internal ExecutionMode ExecutionMode
        {
            get
            {
                if (IsAarch32)
                {
                    return GetPstateFlag(PState.TFlag)
                        ? ExecutionMode.Aarch32Thumb
                        : ExecutionMode.Aarch32Arm;
                }
                else
                {
                    return ExecutionMode.Aarch64;
                }
            }
        }

        public bool Running
        {
            get => false;
            private set {}
        }

        public void SetRunning(bool val)
        {
            Running = val;
        }

        public bool IsRunning()
        {
            return Running;
        }

        public event EventHandler<EventArgs>              Interrupt;
        public event EventHandler<InstExceptionEventArgs> Break;
        public event EventHandler<InstExceptionEventArgs> SupervisorCall;
        public event EventHandler<InstUndefinedEventArgs> Undefined;

        public AppleHypervisor() { }

        public ulong GetX(int index) => 0;
        public void  SetX(int index, ulong value) {}

        public V128 GetV(int index) => V128.Zero;
        public void SetV(int index, V128 value) {}

        public bool GetPstateFlag(PState flag) => false;
        public void SetPstateFlag(PState flag, bool value) { }

        public bool GetFPstateFlag(FPState flag) => false;
        public void SetFPstateFlag(FPState flag, bool value) { }

        public void CheckInterrupt() { }

        public void RequestInterrupt() { }

        public void OnBreak(ulong address, int imm)
        {
            Break?.Invoke(this, new InstExceptionEventArgs(address, imm));
        }

        public void OnSupervisorCall(ulong address, int imm)
        {
            SupervisorCall?.Invoke(this, new InstExceptionEventArgs(address, imm));
        }

        public void OnUndefined(ulong address, int opCode)
        {
            Undefined?.Invoke(this, new InstUndefinedEventArgs(address, opCode));
        }

        public void StopRunning()
        {
            Running = false;
        }

        public void Dispose() { }
    }
}