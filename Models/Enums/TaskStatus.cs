using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ModelLibrary.Enums
{
    enum TaskStatus
    {
        [Description("Not In Progress")]
        NotInProgress,
        [Description("In Progress")]
        InProgress,
        [Description("Complete")]
        Complete,
        [Description("Partially Complete")]
        PartiallyComplete,
        [Description("Cancelled")]
        Cancelled,
        [Description("Deleted")]
        Deleted
    }
}
