using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Pages.Print
{
    public class PrintUtils
    {
        public static void PrintContent(IJSRuntime runtime, string content)
        {
            string formData = $"{{ \"print\": \"{content}\" }}";
            runtime.InvokeVoidAsync("createPost", "./print", formData);
        }
    }
}
