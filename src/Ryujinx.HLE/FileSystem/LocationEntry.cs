using LibHac.Tools.FsSystem.NcaUtils;

namespace Ryujinx.HLE.FileSystem
{
    public struct LocationEntry
    {
        public string ContentPath { get; private set; }
        public int Flag { get; private set; }
        public ulong TitleId { get; private set; }
        public NcaContentType ContentType { get; private set; }

        public LocationEntry() : this(string.Empty, 0, 0, NcaContentType.Program) { }

        public LocationEntry(string contentPath, int flag, ulong titleId, NcaContentType contentType)
        {
            ContentPath = contentPath;
            Flag = flag;
            TitleId = titleId;
            ContentType = contentType;
        }

        public void SetFlag(int flag)
        {
            Flag = flag;
        }
    }
}
