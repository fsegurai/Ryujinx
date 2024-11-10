using Ryujinx.Common.Logging;
using Ryujinx.Common.Memory;
using Ryujinx.HLE.HOS.Applets;
using Ryujinx.HLE.HOS.Services.Am.AppletAE;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ryujinx.HLE.HOS.Applets.Dummy
{
    internal class DummyApplet : IApplet
    {
        private readonly Horizon _system;
        private AppletSession _normalSession;
        public event EventHandler AppletStateChanged;
        
        private static readonly MemoryStream _sharedStream = MemoryStreamManager.Shared.GetStream();
        private static readonly BinaryWriter _sharedWriter = new(_sharedStream);

        public DummyApplet(Horizon system)
        {
            _system = system;
        }

        public ResultCode Start(AppletSession normalSession, AppletSession interactiveSession)
        {
            _normalSession = normalSession;
            _normalSession.Push(BuildResponse());
            AppletStateChanged?.Invoke(this, EventArgs.Empty);
            _system.ReturnFocus();
            return ResultCode.Success;
        }

        private static T ReadStruct<T>(byte[] data) where T : struct
        {
            return MemoryMarshal.Read<T>(data.AsSpan());
        }

        private static byte[] BuildResponse()
        {
            _sharedStream.SetLength(0); // Reset the stream
            _sharedWriter.Seek(0, SeekOrigin.Begin); // Reset the writer position
            _sharedWriter.Write((ulong)ResultCode.Success);
            return _sharedStream.ToArray();
        }

        public ResultCode GetResult()
        {
            return ResultCode.Success;
        }
    }
}
