using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.UI.Extensions
{
    internal interface IScrollSnap
    {
        void ChangePage(int page);
        void SetLerp(bool value);
        int CurrentPage();
        void StartScreenChange();
    }
}
