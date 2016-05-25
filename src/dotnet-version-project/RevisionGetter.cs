﻿using System;
using System.Collections.Generic;

namespace DotNet.Tool.Version.Project
{
    static class RevisionGetter
    {
        static readonly IDictionary<string, IRevisionGetter> s_InstancesByType = new Dictionary<string, IRevisionGetter>
        {
            ["hg"] = new HgRevisionGetter()
        };

        public static IRevisionGetter ForSourceControl(string sourceControlType)
        {
            if (sourceControlType == null) throw new ArgumentNullException(nameof(sourceControlType));

            IRevisionGetter revGetter;
            if (!s_InstancesByType.TryGetValue(sourceControlType, out revGetter))
            {
                throw new NotSupportedException($"Source control '{sourceControlType}' is not supported.");
            }
            return revGetter;
        }
    }
}