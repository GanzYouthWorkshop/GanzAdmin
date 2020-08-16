using GanzAdmin.Database.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Tools
{
    public class ToolService : ComponentBase
    {
        [Inject]
        protected NavigationManager NavMan { get; set; }

        public List<IEntity> CheckedItems { get; set; } = new List<IEntity>();

        public Type CurrentType
        {
            get { return this.currentType; }
            set { this.currentType = value; this.TypeChanged?.Invoke(this, value); }
        }
        private Type currentType = null;

        public bool ShowMenu
        {
            get { return this.showMenu; }
            set
            {
                this.showMenu = value;
                if (this.showMenu)
                {
                    this.Show?.Invoke(this, null);
                }
                else
                {
                    this.Hide?.Invoke(this, null);
                }
            }
        }
        private bool showMenu;

        public event EventHandler Show;
        public event EventHandler Hide;
        public event EventHandler<Type> TypeChanged;

        public List<ToolBase> AvailableTools
        {
            get
            {
                return this.CollectedTools.Where(tool => tool.SupportedEntities.Contains(this.CurrentType)).ToList();
            }
        }

        public List<ToolBase> CollectedTools
        {
            get
            {
                if (!this.toolCollectingFinished)
                {
                    IEnumerable<Type> tools = typeof(ToolBase).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(ToolBase)));
                    foreach (Type tool in tools)
                    {
                        this.collectedTools.Add((ToolBase)Activator.CreateInstance(tool));
                    }
                    this.toolCollectingFinished = true;
                }
                return this.collectedTools;
            }
        }
        private List<ToolBase> collectedTools = new List<ToolBase>();

        private bool toolCollectingFinished = false;

        public ToolService(NavigationManager navman)
        {
            navman.LocationChanged += NavMan_LocationChanged;
        }

        private void NavMan_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            //TODO: [KG] Activate when actually works
            //this.Reset(null);
        }

        public void AddRemove(IEntity entity)
        {
            if (this.CheckedItems.Contains(entity))
            {
                this.CheckedItems.Remove(entity);
            }
            else
            {
                this.CheckedItems.Add(entity);
            }
        }

        public void Reset(Type type)
        {
            this.CheckedItems.Clear();
            this.CurrentType = type;
        }
    }
}
