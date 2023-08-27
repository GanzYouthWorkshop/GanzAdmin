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
using GanzAdmin.Authentication;
using Microsoft.AspNetCore.Http;
using GanzAdmin.Tools;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using GanzNet.Authentication;

namespace GanzAdmin.DataHandling
{
    public abstract class DataHandlingController<T> : ComponentBase where T : class, IEntity, new()
    {
        [Inject]
        protected IAuthProvider AuthManager { get; set; }

        [Inject]
        protected NavigationManager NavMan { get; set; }

        [Inject]
        protected IJSRuntime JS { get; set; }

        [Inject]
        protected IHttpContextAccessor Http { get; set; }

        [Inject]
        protected IWebHostEnvironment WebHost { get; set; }

        [Inject]
        protected ToolService ToolProvider { get; set; }

        protected abstract string BaseLink { get; set; }
        protected abstract string DataName { get; set; }

        protected ILiteCollection<T> m_Collection;

        [Parameter]
        public string PopupDisplay { get; set; }

        protected bool DialogLoading { get; set; } = false;

        protected List<T> ItemList { get; set; }
        protected TemporalObject<T> SelectedItem { get; set; }


        public DataHandlingController()
        {
        }

        #region Közös alap funkcionalitások
        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() =>
            {
                using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
                {
                    this.m_Collection = GanzAdminDbEngine.GetInstance().GetCollection<T>();

                    this.ItemList = this.m_Collection.FindAll().OrderBy(t => t.DisplayValue).ToList();
                    this.ToolProvider.Reset(typeof(T));

                    this.Init();
                }
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
            this.DialogLoading = true;

            Task.Run(() =>
            {
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

        protected void OnDefaultDeleteSubmit()
        {
            this.DialogLoading = true;

            Task.Run(() =>
            {
                this.Delete();

                this.DialogLoading = false;
                this.NavMan.NavigateTo($"/{BaseLink}");
            });

        }

        private void Add()
        {
            if (this.BeforeAdd())
            {
                using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
                {
                    this.m_Collection.Insert(this.SelectedItem.FoldBack());
                    db.Transact();
                }

                this.ItemList.Add(this.SelectedItem.Original);

                this.JS.InvokeVoidAsync("alertify.success", $"{this.DataName.ToCapital()} hozzáadva!");
            }
        }

        private void Modify()
        {
            if (this.BeforeEdit())
            {
                using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
                {
                    this.m_Collection.Update(this.SelectedItem.FoldBack());
                    db.Transact();
                }

                this.JS.InvokeVoidAsync("alertify.success", $"{this.DataName.ToCapital()} módosítva!");
            }
        }

        private void Delete()
        {
            if (this.BeforeDelete())
            {
                using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
                {
                    this.m_Collection.Delete(this.SelectedItem.Original.Id);
                    db.Transact();
                }

                this.ItemList.Remove(this.SelectedItem.Original);

                this.JS.InvokeVoidAsync("alertify.success", $"{this.DataName.ToCapital()} törölve :(");
            }
        }

        protected override bool ShouldRender()
        {
            bool result = !Environment.StackTrace.Contains("OnValueChangedOnly");
            return result;
        }
        #endregion

        #region Kötelezően felülírandó dolgok
        protected virtual string PopupTitle
        {
            get
            {
                string result = "";
                if(this.PopupDisplay == "add")
                {
                    result = $"{this.DataName.ToCapital()} hozzáadása";
                }
                else
                {
                    result = $"'{this.SelectedItem.Temporal.DisplayValue}' szerkesztése";
                }
                return result;
            }
        }


        protected virtual void Init()
        {

        }

        protected virtual bool BeforeAdd()
        {
            return true;
        }

        protected virtual bool BeforeEdit()
        {
            return true;
        }

        protected virtual bool BeforeDelete()
        {
            return true;
        }

        #endregion

        #region Segédmetódusok
        protected void DeleteUploadedFile(string contentPath)
        {
            try
            {
                string fullPath = GanzUtils.ProperPathCombine('\\', this.WebHost.WebRootPath, contentPath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
