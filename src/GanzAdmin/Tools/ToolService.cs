using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Tools
{
    public class ToolService
    {
        public List<IEntity> CheckedItems { get; set; } = new List<IEntity>();
        public Type CurrentType { get; set; }

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
            }
        }
        private bool showMenu;
        public event EventHandler Show;

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
