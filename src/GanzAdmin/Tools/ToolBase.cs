using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GanzAdmin.Tools
{
    public abstract class ToolBase : ComponentBase
    {
        [Inject]
        public ToolService ToolProvider { get; set; }

        [Inject]
        protected IJSRuntime JS { get; set; }

        public string Icon { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public List<Type> SupportedEntities { get; protected set; }

        [Parameter]
        public List<IEntity> Entities { get; set; }

        protected void OpenNewWindow(string url)
        {
            this.JS.InvokeVoidAsync("window.open", url, "_blank");
        }
    }
}
