﻿namespace DotNet.Tool.Version.Project
{
    class WorkingCopyVersion
    {
        public uint RevisionNumber { get; }
        public string RevisionId { get; }
        public bool HasLocalChanges { get; }

        public WorkingCopyVersion(uint revNumber, string revId, bool hasLocalChanges)
        {
            this.RevisionNumber = revNumber;
            this.RevisionId = revId ?? revNumber.ToString();
            this.HasLocalChanges = hasLocalChanges;
        }

        public System.Version ToVersion(int major, int minor)
        {
            var calculatedBuildNumberHigh = (ushort)(this.RevisionNumber >> 16);
            var calculatedBuildNumberLow = (ushort)this.RevisionNumber;

            if (this.HasLocalChanges)
            {
                calculatedBuildNumberHigh |= 32768;
            }

            return new System.Version(major, minor, calculatedBuildNumberHigh, calculatedBuildNumberLow);
        }

        public string ToVersionString(int major, int minor, int patch, string releaseMarker)
        {
            var dirtyMarker = this.HasLocalChanges ? "-dirty" : string.Empty;

            if (releaseMarker != null)
            {
                releaseMarker = "-" + releaseMarker;
            }

            return $"{major}.{minor}.{patch}.{this.RevisionNumber}{releaseMarker}+{this.RevisionId}{dirtyMarker}";
        }
    }
}