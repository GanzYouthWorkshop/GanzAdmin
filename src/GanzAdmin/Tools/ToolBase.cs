using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GanzAdmin.Tools
{
    public abstract class ToolBase : ComponentBase
    {
        [Inject]
        public ToolService ToolProvider { get; set; }

        public string Icon { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public List<Type> SupportedEntities { get; protected set; }

        [Parameter]
        public List<IEntity> Entities { get; set; }
    }
}
