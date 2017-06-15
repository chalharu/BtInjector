using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtInjector.Test.Models
{
    public interface IErrorHandler
    {
        ILogger Logger { get; }
    }
}
