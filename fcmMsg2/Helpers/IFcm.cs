using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmMsg2.Helpers
{
    public interface IFcm
    {
        Task<bool> MessageNotificationAsync(string to, string title, string body, bool includeData);
    }
}
