using GanzAdmin.Database;
using GanzAdmin.Utils;
using GanzAdmin.Components.Edit;
using GanzAdmin.Components.DataGrid;
using LiteDB;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanzAdmin.Database.Models;

namespace GanzAdmin.DataHandling
{
    public abstract class DataHandlingController<T> : ComponentBase where T : class, IEntity, new()
    {
        [Inject]
        protected NavigationManager NavMan { get; set; }

        [Inject]
        protected IJSRuntime JS { get; set; }

        protected string BaseLink { get; set; } = "members";
        protected string DataName { get; set; } = "tag";
        protected ILiteCollection<T> m_Collection;

        [Parameter]
        public string PopupDisplay { get; set; }

        protected bool DialogLoading { get; set; } = false;

        protected List<T> ItemList { get; set; }
        protected TemporalObject<T> SelectedItem { get; set; }

        protected abstract string PopupTitle { get; }

        public DataHandlingController()
        {
            this.m_Collection = GanzAdminDbEngine.Instance.GetCollection<T>();
        }

        protected virtual void Init()
        {

        }

        protected virtual void BeforeAdd()
        {

        }

        protected virtual void BeforeEdit()
        {

        }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() =>
            {
                this.ItemList = this.m_Collection.FindAll().ToList();

                this.Init();
            });
        }
        
        protected override async Task OnParametersSetAsync()
        {
            await Task.Run(() =>
            {
                this.SelectedItem = null;

                if (this.PopupDisplay == "add")
                {
                    this.SelectedItem = new TemporalObject<T>(new T());
                }
                else if (this.PopupDisplay != null)
                {
                    int showId = 0;
                    if (int.TryParse(this.PopupDisplay, out showId))
                    {
                        T item = this.ItemList.FirstOrDefault(i => i.Id == showId);
                        this.SelectedItem = new TemporalObject<T>(item);
                    }
                }
            });
        }

        protected void OnDefaultAddEditSubmit()
        {
            Task.Run(() =>
            {
                this.DialogLoading = true;
                if (this.PopupDisplay == "add")
                {
                    this.Add();
                }
                else
                {
                    this.Modify();
                }
                this.DialogLoading = false;
                this.NavMan.NavigateTo($"/{BaseLink}");
            });
        }

        private void Add()
        {
            this.BeforeAdd();

            this.m_Collection.Insert(this.SelectedItem.FoldBack());
            GanzAdminDbEngine.Instance.Transact();

            this.JS.InvokeVoidAsync("alertify.success", $"{this.DataName.ToCapital()} hozzáadva!");
        }

        private void Modify()
        {
            this.BeforeEdit();

            this.m_Collection.Update(this.SelectedItem.FoldBack());
            GanzAdminDbEngine.Instance.Transact();

            this.JS.InvokeVoidAsync("alertify.success", $"{this.DataName.ToCapital()} módosítva!");
        }

        private void Delete()
        {
            GanzAdminDbEngine.Instance.Members.Delete(this.SelectedItem.Original.Id);
            GanzAdminDbEngine.Instance.Transact();

            this.JS.InvokeVoidAsync("alertify.success", $"{this.DataName.ToCapital()} törölve :(");
        }
    }
}
